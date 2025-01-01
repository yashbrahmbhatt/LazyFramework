using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Interop;
using LazyFramework.DX.Shared.Models;
using Newtonsoft.Json;
using UiPath.CodedWorkflows;
using UiPath.CodedWorkflows.Interfaces;
using UiPath.Core;
using UiPath.Core.Activities;
#nullable enable
namespace LazyFramework.DX.Shared.Frameworks.Performers.BasicPerformer
{

    public abstract class BaseConfig : DictionaryObject
    {
        public string ProcessName = "BasicPerformer";
        public string QueueName = "BasicPerformer";
        public string QueueFolder = "LazyFramework";
        public string Folder_ExScreenshot = "Data\\Exception Screenshots";
        public List<string> ProcessesToKill = new List<string>();
        public TimeSpan Maintenance_Start = new TimeSpan(0, 0, 0);
        public TimeSpan Maintenance_End = new TimeSpan(0, 0, 0);

        public int MaxConsecutiveExceptions = 5;
        public int MaxQueueRetries = 3;

        public List<string> FrameEx_To = new List<string>();
        public List<string> FrameEx_CC = new List<string>();
        public List<string> TransSysEx_To = new List<string>();
        public List<string> TransSysEx_CC = new List<string>();
        public List<string> TransBusEx_To = new List<string>();
        public List<string> TransBusEx_CC = new List<string>();

        public string FrameEx_Subject = "";
        public string FrameEx_Body = "";
        public string TransSysEx_Subject = "";
        public string TransSysEx_Body = "";
        public string TransBusEx_Subject = "";
        public string TransBusEx_Body = "";
    }

    public abstract class BaseStateData<TConfig> where TConfig : BaseConfig
    {
        public TConfig? Config = null;
        public Exception? SysEx = null;
        public Exception? FrameEx = null;
        public BusinessRuleException? BusEx = null;
        public QueueItem? Transaction = null;
        public int ConsecutiveSystemExceptions = 0;
    }

    public abstract class BaseWorkflow<TState, TConfig> : CodedWorkflow where TState : BaseStateData<TConfig> where TConfig : BaseConfig
    {
        public abstract TState Execute(TState state);
    }




    public abstract partial class BaseMain<TConfig, TStateData, TWorkflow> : CodedWorkflow where TConfig : BaseConfig, new() where TStateData : BaseStateData<TConfig>, new() where TWorkflow : BaseWorkflow<TStateData, TConfig>
    {
        // Workflow Slots
        public delegate TStateData ExecuteDelegate(TStateData state);
        public static class WorkflowSlots
        {
            public static ExecuteDelegate? InitializeSettings;
            public static ExecuteDelegate InitializeApplications;
            public static ExecuteDelegate Process;
            public static ExecuteDelegate GetTransactionData;
            public static ExecuteDelegate? HandleBusinessException;
            public static ExecuteDelegate? HandleSystemException;
            public static ExecuteDelegate? HandleSuccess;
            public static ExecuteDelegate? End;
            public static ExecuteDelegate CloseApplications;
        }

        // Constructors
        public BaseMain() { }

        // Fields
        public TStateData Data = new();
        public Stack<State> Stack = new();
        public FixedSizeQueue<string> StackHistory = new FixedSizeQueue<string>(10);
        public TStateData InitialData;


        // Entry
        public void RunFramework(string configPath, List<string> ignored)
        {
            ValidateWorkflows();
            InitializeFramework(configPath, ignored);
            RunStateMachine(TestId.None);
        }

        // Framework Methods
        public abstract void SendEmail(List<string>? to, List<string>? cc, List<string> attachments, string body, string subject);
        public void SendErrorEmail()
        {
            if (Data.FrameEx != null)
            {
                var template = new DiagnosticDictionary(Data.FrameEx);
                template["ProcessName"] = Data.Config?.ProcessName;
                var attachments = new List<string>() { SharedHelpers.TakeScreenshot(Data.Config?.Folder_ExScreenshot) };
                var (body, subject) = EmailHelpers.UpdateEmailTemplate(
                    Data.Config?.FrameEx_Body, Data.Config?.FrameEx_Subject, template.Data
                );

                SendEmail(Data.Config?.FrameEx_To, Data.Config?.FrameEx_CC, attachments, body, subject);
            }
            else if (Data.SysEx != null)
            {
                var template = new DiagnosticDictionary(Data.SysEx);
                template["ProcessName"] = Data.Config?.ProcessName;
                var attachments = new List<string>() { SharedHelpers.TakeScreenshot(Data.Config?.Folder_ExScreenshot) };
                var (body, subject) = EmailHelpers.UpdateEmailTemplate(
                    Data.Config?.TransSysEx_Body, Data.Config?.TransSysEx_Subject, template.Data
                );

                SendEmail(Data.Config?.TransSysEx_To, Data.Config?.TransSysEx_CC, attachments, body, subject);
            }
            else if (Data.BusEx != null)
            {
                var template = new DiagnosticDictionary(Data.BusEx);
                template["ProcessName"] = Data.Config?.ProcessName;
                var attachments = new List<string>() { SharedHelpers.TakeScreenshot(Data.Config?.Folder_ExScreenshot) };
                var (body, subject) = EmailHelpers.UpdateEmailTemplate(
                    Data.Config?.TransBusEx_Body, Data.Config?.TransBusEx_Subject, template.Data
                );

                SendEmail(Data.Config?.TransBusEx_To, Data.Config?.TransBusEx_CC, attachments, body, subject);
            }
            else
            {
                throw new NotImplementedException("Unknown state for sending error email");
            }
        }
        public bool IsMaintenanceTime()
        {
            Log($"Checking if currently ({DateTime.Now.ToString()} between maintenance times ({Data.Config?.Maintenance_Start} {Data.Config?.Maintenance_End})");
            return SharedHelpers.CurrentlyBetweenTimes(Data.Config?.Maintenance_Start, Data.Config?.Maintenance_End);
        }
        public void RunStateMachine(TestId testId)
        {
            Stack.Push(InitializeState);
            while (Stack.Count > 0)
            {
                try
                {
                    var currentState = Stack.Pop();
                    StackHistory.Enqueue(currentState.Method.Name);
                    currentState.Invoke(testId);
                }
                catch (Exception e)
                {
                    Data.FrameEx = e;
                    Stack.Clear();
                }
            }
            StackHistory.Enqueue("EndState");
            EndState(testId);
        }
        public void InitializeFramework(string configPath, List<string> ignored)
        {
            // System.Windows.MessageBox.Show("The primary screen resolution is: " + SystemParameters.PrimaryScreenWidth.ToString() + " x " + SystemParameters.PrimaryScreenHeight.ToString());
            // File.WriteAllText("text.json", JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
            Log("The primary screen resolution is: " + SystemParameters.PrimaryScreenWidth.ToString() + " x " + SystemParameters.PrimaryScreenHeight.ToString());
            if (WorkflowSlots.InitializeSettings != null) Data = WorkflowSlots.InitializeSettings(Data);
            else Data = new TStateData() { Config = DictionaryObjectFactory.FromDictionary<TConfig>(workflows.LoadConfig(configPath, ignored)) };
            InitialData = JsonConvert.DeserializeObject<TStateData>(JsonConvert.SerializeObject(Data));
        }
        public void ValidateWorkflows()
        {
            if (WorkflowSlots.InitializeApplications == null) throw new FrameworkWorkflowNotInitialized(nameof(WorkflowSlots.InitializeApplications));
            if (WorkflowSlots.Process == null) throw new FrameworkWorkflowNotInitialized(nameof(WorkflowSlots.Process));
            if (WorkflowSlots.CloseApplications == null) throw new FrameworkWorkflowNotInitialized(nameof(WorkflowSlots.CloseApplications));
            if (WorkflowSlots.GetTransactionData == null) throw new FrameworkWorkflowNotInitialized(nameof(WorkflowSlots.GetTransactionData));
        }
        public void KillProcesses(List<string> processNames)
        {
            foreach (var processName in processNames)
            {
                try
                {
                    var processes = Process.GetProcessesByName(processName);
                    foreach (var process in processes)
                    {
                        try
                        {
                            process.Kill();
                            process.WaitForExit(); // Optional: Ensure the process has exited
                            Log($"Successfully killed process: {process.ProcessName} (ID: {process.Id})");
                        }
                        catch (Exception ex)
                        {
                            Log($"Failed to kill process: {process.ProcessName} (ID: {process.Id}). Error: {ex.Message}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log($"Error retrieving processes for '{processName}': {ex.Message}");
                }
            }
        }
        // States
        public delegate void State(TestId testId);
        public void InitializeState(TestId testId)
        {
            switch (testId)
            {
                case TestId.InitializeMaintenanceTime:
                    Data.Config.Maintenance_Start = new TimeSpan(0, 0, 0);
                    Data.Config.Maintenance_End = new TimeSpan(23, 59, 59);
                    break;
                case TestId.InitializeStateError:
                    throw new Exception(nameof(testId));
                default:
                    break;
            }
            Log("Entering Initialize...");
            Data.SysEx = null;

            try
            {
                Log("Trying to close applications");
                Data = WorkflowSlots.CloseApplications(Data);
                Log("Applications closed successfully");
                
            }
            catch
            {
                Log("Failed to close applications, killing processes instead");
                KillProcesses(Data.Config?.ProcessesToKill ?? new List<string>());
            }
            if (IsMaintenanceTime())
            {
                Log("Within maintenance window");
            }
            else
            {

                SharedHelpers.Retry<bool>(() =>
                {
                    Log("Initializing applications...");
                    Data = WorkflowSlots.InitializeApplications(Data);
                    Log("Applications initialized");
                });
                Stack.Push(GetTransactionState);
            }
        }
        public void GetTransactionState(TestId testId)
        {
            switch (testId)
            {
                case TestId.GetTransactionMaintenanceTime:
                    Data.Config.Maintenance_Start = new TimeSpan(0, 0, 0);
                    Data.Config.Maintenance_End = new TimeSpan(23, 59, 59);
                    break;
                case TestId.GetTransactionStateError:
                    throw new Exception(nameof(testId));
                default:
                    break;
            }
            Log("Getting transaction");
            if (IsMaintenanceTime())
            {
                Log("Currently within maintenance window");
                return;
            }
            Data = WorkflowSlots.GetTransactionData(Data);
            if (Data.Transaction == null)
            {
                Log("No more queue items");
                return;
            }
            Log("Transaction found");
            Stack.Push(ProcessState);
        }
        public void ProcessState(TestId testId)
        {
            switch (testId)
            {
                case TestId.ProcessStateError:
                    throw new Exception(nameof(testId));
                default:
                    break;
            }
            ProcessingStatus outcome = ProcessingStatus.Successful;
            Dictionary<string, object> analytics = new();
            string details = "";
            string reason = "";
            ErrorType errorType = ErrorType.Application;
            Data.BusEx = null;
            try
            {
                switch (testId)
                {
                    case TestId.TransactionSystemException:
                        throw new Exception(nameof(testId));
                    case TestId.TransactionBusinessException:
                        throw new Exception(nameof(testId));
                    default:
                        break;
                }
                Data = WorkflowSlots.Process(Data);
                Data.ConsecutiveSystemExceptions = 0;
            }
            catch (BusinessRuleException bre)
            {
                outcome = ProcessingStatus.Failed;
                errorType = ErrorType.Business;
                details = bre.Message;
                reason = bre.StackTrace ?? "";
                Data.BusEx = bre;
            }
            catch (Exception se)
            {
                outcome = ProcessingStatus.Failed;
                errorType = ErrorType.Application;
                reason = se.Message;
                details = se.StackTrace ?? "";
                Data.SysEx = se;
            }
            finally
            {
                system.SetTransactionStatus(
                    Data.Transaction,
                    outcome,
                    Data.Config?.QueueFolder,
                    analytics,
                    Data.Transaction?.Output,
                    details,
                    errorType,
                    reason,
                    15000
                );
                if (Data.SysEx != null)
                {
                    SendErrorEmail();
                    Data = WorkflowSlots.HandleSystemException != null ? WorkflowSlots.HandleSystemException(Data) : Data;
                    Data.ConsecutiveSystemExceptions += 1;
                    if (Data.ConsecutiveSystemExceptions > Data.Config?.MaxConsecutiveExceptions)
                    {
                        Log("Reached maximum consecutive system exception");
                    }
                    else
                    {
                        Log($"Consecutive system exceptions: {Data.ConsecutiveSystemExceptions.ToString()} less than max, reinitializing");
                        Stack.Push(InitializeState);
                    }
                }
                else if (Data.BusEx != null)
                {
                    SendErrorEmail();
                    Data = WorkflowSlots.HandleBusinessException != null ? WorkflowSlots.HandleBusinessException(Data) : Data;
                    Stack.Push(GetTransactionState);
                }
                else
                {
                    Data = WorkflowSlots.HandleSuccess != null ? WorkflowSlots.HandleSuccess(Data) : Data;
                    Stack.Push(GetTransactionState);
                }
            }
        }
        public void EndState(TestId testId)
        {
            Data = WorkflowSlots.End != null ? WorkflowSlots.End(Data) : Data;
            if (Data.FrameEx != null) SendErrorEmail();

            try
            {
                Data = WorkflowSlots.CloseApplications(Data);
            }
            catch
            {
                KillProcesses(Data.Config?.ProcessesToKill ?? new List<string>());
            }

            if (Data.FrameEx != null) throw Data.FrameEx;
        }

        // State Tests
        public virtual void RunTests(string configPath, List<string> ignored)
        {
            InitializeFramework(configPath, ignored);
            RunTest("InitializeState - Maintenance Time", TestId.InitializeMaintenanceTime, new List<VerifyTest>() {
                (ex, data, history) => ("No test Exception", testing.VerifyExpression(ex == null)),
                (ex, data, history) => ("Only 2 states", testing.VerifyExpression((history as FixedSizeQueue<string>).Count == 2)),
                (ex, data, history) => ("First state was initialize", testing.VerifyExpression((history as FixedSizeQueue<string>).ToArray().First() == "InitializeState")),
                (ex, data, history) => ("Last state was End", testing.VerifyExpression((history as FixedSizeQueue<string>).ToArray().Last() == "EndState"))
            });
            RunTest("InitializeState - State Error", TestId.InitializeStateError, new List<VerifyTest>() {
                (ex, data, history) => ("Exception raised", testing.VerifyExpression(ex != null)),
                (ex, data, history) => ("Framework exception not null", testing.VerifyExpression(Data.FrameEx != null)),
                (ex, data, history) => ("Only 2 states", testing.VerifyExpression((history as FixedSizeQueue<string>).Count == 2)),
                (ex, data, history) => ("First state was initialize", testing.VerifyExpression((history as FixedSizeQueue<string>).ToArray().First() == "InitializeState")),
                (ex, data, history) => ("Last state was End", testing.VerifyExpression((history as FixedSizeQueue<string>).ToArray().Last() == "EndState"))
            });
            RunTest("GetTransaction - Maintenance Time", TestId.GetTransactionMaintenanceTime, new List<VerifyTest>() {
                (ex, data, history) => ("No test Exception", testing.VerifyExpression(ex == null)),
                (ex, data, history) => ("Only 3 states", testing.VerifyExpression((history as FixedSizeQueue<string>).Count == 3)),
                (ex, data, history) => ("First state was initialize", testing.VerifyExpression((history as FixedSizeQueue<string>).ToArray().First() == "InitializeState")),
                (ex, data, history) => ("Second state was GetTransaction", testing.VerifyExpression((history as FixedSizeQueue<string>).ToArray()[1] == "GetTransactionState")),
                (ex, data, history) => ("Last state was End", testing.VerifyExpression((history as FixedSizeQueue<string>).ToArray().Last() == "EndState"))
            });
            RunTest("InitializeState - State Error", TestId.InitializeStateError, new List<VerifyTest>() {
                (ex, data, history) => ("Exception raised", testing.VerifyExpression(ex != null)),
                (ex, data, history) => ("Framework exception not null", testing.VerifyExpression(Data.FrameEx != null)),
                (ex, data, history) => ("Only 3 states", testing.VerifyExpression((history as FixedSizeQueue<string>).Count == 3)),
                (ex, data, history) => ("First state was initialize", testing.VerifyExpression((history as FixedSizeQueue<string>).ToArray().First() == "InitializeState")),
                (ex, data, history) => ("Second state was GetTransaction", testing.VerifyExpression((history as FixedSizeQueue<string>).ToArray()[1] == "GetTransactionState")),
                (ex, data, history) => ("Last state was End", testing.VerifyExpression((history as FixedSizeQueue<string>).ToArray().Last() == "EndState"))
            });
        }
        public delegate (string verificationName, bool result) VerifyTest(Exception testException, TStateData data, FixedSizeQueue<string> history);
        public void RunTest(string name, TestId testId, List<VerifyTest> verifications)
        {
            ResetState();
            ResetHistory();
            Exception ex = null;
            try
            {
                RunStateMachine(testId);
            }
            catch (Exception e)
            {
                ex = e;
            }
            foreach (VerifyTest verification in verifications)
            {
                var result = verification(ex, Data, StackHistory);
                Log($"[Test {name}] {result.verificationName}:  {result.result.ToString()}");
            }
        }

        // Resets everything except config
        public void ResetState() {
            Data = JsonConvert.DeserializeObject<TStateData>(JsonConvert.SerializeObject(InitialData));
        }
        public void ResetHistory(){
            StackHistory.Clear();
        }

        // Helper Structs/Classes/Enums
        public class FrameworkWorkflowNotInitialized : Exception
        {
            public FrameworkWorkflowNotInitialized(string name) : base($"Workflow with name '{name}' must be set in the constructor.")
            {
            }

            public FrameworkWorkflowNotInitialized(string message, Exception innerException) : base(message, innerException)
            {
            }
        }
    }
    public enum TestId
    {
        // InitializeState
        InitializeStateError,
        InitializeMaintenanceTime,
        //GetTransactionDataState
        GetTransactionStateError,
        GetTransactionMaintenanceTime,
        //Process
        ProcessStateError,
        TransactionSystemException,
        TransactionBusinessException,
        //None
        None
    }
}
