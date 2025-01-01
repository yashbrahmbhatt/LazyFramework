using System;
using System.Collections.Generic;
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




    public abstract class BaseMain<TConfig, TStateData, TWorkflow> : CodedWorkflow where TConfig : BaseConfig, new() where TStateData : BaseStateData<TConfig>, new() where TWorkflow : BaseWorkflow<TStateData, TConfig>
    {
        // Workflow Slots
        public delegate TStateData ExecuteDelegate(TStateData state);
        public ExecuteDelegate? InitializeSettings;
        public ExecuteDelegate InitializeApplications;
        public ExecuteDelegate Process;
        public ExecuteDelegate GetTransactionData;
        public ExecuteDelegate? HandleBusinessException;
        public ExecuteDelegate? HandleSystemException;
        public ExecuteDelegate? HandleSuccess;
        public ExecuteDelegate? End;
        public ExecuteDelegate CloseApplications;

        // Constructors
        public BaseMain() { }

        // Fields
        public TStateData Data = new();
        public Stack<State> Stack = new();


        // Entry
        public virtual void Execute(string configPath, List<string> ignored)
        {
            ValidateWorkflows();
            InitializeFramework(configPath, ignored);
            RunStateMachine();
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
            return SharedHelpers.CurrentlyBetweenTimes(Data.Config?.Maintenance_Start, Data.Config?.Maintenance_End);
        }
        public void RunStateMachine()
        {
            Stack.Push(InitializeState);
            while (Stack.Count > 0)
            {
                try
                {
                    var currentState = Stack.Pop();
                    currentState.Invoke(TestId.None);
                }
                catch (Exception e)
                {
                    Data.FrameEx = e;
                    Stack.Push(EndState);
                }
            }
        }
        public void InitializeFramework(string configPath, List<string> ignored)
        {
            // System.Windows.MessageBox.Show("The primary screen resolution is: " + SystemParameters.PrimaryScreenWidth.ToString() + " x " + SystemParameters.PrimaryScreenHeight.ToString());
            // File.WriteAllText("text.json", JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
            Log("The primary screen resolution is: " + SystemParameters.PrimaryScreenWidth.ToString() + " x " + SystemParameters.PrimaryScreenHeight.ToString());
            if (InitializeSettings != null) Data = InitializeSettings(Data);
            else Data = new TStateData() { Config = DictionaryObjectFactory.FromDictionary<TConfig>(workflows.LoadConfig(configPath, ignored)) };
        }
        private void ValidateWorkflows()
        {
            if (InitializeApplications == null) throw new FrameworkWorkflowNotInitialized(nameof(InitializeApplications));
            if (Process == null) throw new FrameworkWorkflowNotInitialized(nameof(Process));
            if (CloseApplications == null) throw new FrameworkWorkflowNotInitialized(nameof(CloseApplications));
            if (GetTransactionData == null) throw new FrameworkWorkflowNotInitialized(nameof(GetTransactionData));
        }

        // States
        public delegate void State(TestId testId);
        public void InitializeState(TestId testId)
        {
            switch (testId)
            {
                case TestId.MaintenanceTime:
                    Data.Config.Maintenance_Start = new TimeSpan(0, 0, 0);
                    Data.Config.Maintenance_End = new TimeSpan(23, 59, 59);
                    break;
                case TestId.StateError:
                    throw new Exception(nameof(testId));
                default:
                    break;
            }
            Log("Entering Initialize...");
            Data.SysEx = null;

            try
            {
                Log("Trying to close applications");
                Data = CloseApplications(Data);
            }
            catch
            {
                Log("Failed to close applications, killing processes instead");
                SharedHelpers.KillProcesses(Data.Config?.ProcessesToKill ?? new List<string>());
            }
            if (IsMaintenanceTime())
            {
                Log("Within maintenance window");
                Stack.Push(EndState);
            }
            {

                SharedHelpers.Retry<bool>(() =>
                {
                    Log("Initializing applications...");
                    Data = InitializeApplications(Data);
                    Log("Applications initialized");
                });
                Stack.Push(GetTransactionState);
            }
        }
        public void GetTransactionState(TestId testId)
        {
            switch (testId)
            {
                case TestId.MaintenanceTime:
                    Data.Config.Maintenance_Start = new TimeSpan(0, 0, 0);
                    Data.Config.Maintenance_End = new TimeSpan(23, 59, 59);
                    break;
                case TestId.StateError:
                    throw new Exception(nameof(testId));
                default:
                    break;
            }
            Log("Getting transaction");
            if (IsMaintenanceTime())
            {
                Log("Currently within maintenance window");
                Stack.Push(EndState);
                return;
            }
            Data = GetTransactionData(Data);
            if (Data.Transaction == null)
            {
                Log("No more queue items");
                Stack.Push(EndState);
                return;
            }
            Log("Transaction found");
            Stack.Push(ProcessState);
        }
        public void ProcessState(TestId testId)
        {
            switch (testId)
            {
                case TestId.StateError:
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

                    default:
                        break;
                }
                Data = Process(Data);
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
                    Data = HandleSystemException != null ? HandleSystemException(Data) : Data;
                    Stack.Push(InitializeState);
                }
                else if (Data.BusEx != null)
                {
                    SendErrorEmail();
                    Data = HandleBusinessException != null ? HandleBusinessException(Data) : Data;
                    Stack.Push(GetTransactionState);
                }
                else
                {
                    Data = HandleSuccess != null ? HandleSuccess(Data) : Data;
                    Stack.Push(GetTransactionState);
                }
            }
        }
        public void EndState(TestId testId)
        {
            Data = End != null ? End(Data) : Data;
            if (Data.FrameEx != null) SendErrorEmail();

            try
            {
                Data = CloseApplications(Data);
            }
            catch
            {
                SharedHelpers.KillProcesses(Data.Config?.ProcessesToKill ?? new List<string>());
            }

            if (Data.FrameEx != null) throw Data.FrameEx;
        }

        // State Tests

        public virtual void RunTests(string configPath, List<string> ignored)
        {
            InitializeFramework(configPath, ignored);
            InitializeStateTestMaintenanceTime();
        }
        public void InitializeStateTestMaintenanceTime()
        {
            ResetStateExceptConfig();
            Exception ex = null;
            try
            {
                InitializeState(TestId.MaintenanceTime);
            }
            catch (Exception e)
            {
                ex = e;
            }

            testing.VerifyExpression(ex == null);
            testing.VerifyExpression(Stack.Count == 0 && Stack.First() == EndState);
        }

        public void InitializeStateTestStateError()
        {
            ResetStateExceptConfig();
            Exception ex = null;
            try
            {
                InitializeState(TestId.StateError);
            }
            catch (Exception e)
            {
                ex = e;
            }

            testing.VerifyExpression(ex != null);
        }
        // Resets everything except config
        public void ResetStateExceptConfig()
        {
            Data.SysEx = null;
            Data.FrameEx = null;
            Data.BusEx = null;
            Data.Transaction = null;
            Data.ConsecutiveSystemExceptions = 0;
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
        StateError,
        TransactionSystemException,
        TransactionBusinessException,
        MaintenanceTime,
        None
    }
}
