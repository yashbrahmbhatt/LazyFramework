using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Interop;
using LazyFramework.DX.Shared.Models;
using Newtonsoft.Json;
using UiPath.CodedWorkflows;
using UiPath.CodedWorkflows.Interfaces;
using UiPath.Core;
using UiPath.Core.Activities;
#nullable enable
namespace LazyFramework.DX.Shared.BasicPerformer
{

    
    public abstract class BaseMain<TConfig, TStateData/*, TWorkflow*/> : CodedWorkflow where TConfig : BaseConfig, new() where TStateData : BaseStateData<TConfig>, new() /* where TWorkflow : BaseWorkflow<TStateData, TConfig>*/
    {
        // Workflow Slots
        public WorkflowSlots Slots = new();
        public class WorkflowSlots
        {
            public delegate TStateData ExecuteDelegate(TStateData state);

            public ExecuteDelegate? InitializeSettings = null;
            public ExecuteDelegate InitializeApplications = DefaultExecuteDelegate("InitializeApplications");
            public ExecuteDelegate Process = DefaultExecuteDelegate("Process");
            public ExecuteDelegate GetTransactionData = DefaultExecuteDelegate("GetTransactionData");
            public ExecuteDelegate? HandleBusinessException;
            public ExecuteDelegate? HandleSystemException;
            public ExecuteDelegate? HandleSuccess;
            public ExecuteDelegate? End;
            public ExecuteDelegate CloseApplications = DefaultExecuteDelegate("CloseApplications");

            public WorkflowSlots() { }

            public static ExecuteDelegate DefaultExecuteDelegate(string name)
            {
                return (TStateData state) =>
                {
                    return state;
                };
            }
        }

        // Constructors
        public BaseMain() : base() { }

        // Fields
        public TStateData Data = new();
        
        
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
            Data.Stack.Push(InitializeState);
            while (Data.Stack.Count > 0)
            {
                try
                {
                    var currentState = Data.Stack.Pop();
                    currentState.Invoke(testId);
                }
                catch (Exception e)
                {
                    Data.FrameEx = e;                    
                }
            }
            EndState(testId);
        }
        public virtual void InitializeFramework(string configPath, List<string> ignored)
        {
            // System.Windows.MessageBox.Show("The primary screen resolution is: " + SystemParameters.PrimaryScreenWidth.ToString() + " x " + SystemParameters.PrimaryScreenHeight.ToString());
            // File.WriteAllText("text.json", JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
            Log("The primary screen resolution is: " + SystemParameters.PrimaryScreenWidth.ToString() + " x " + SystemParameters.PrimaryScreenHeight.ToString());
            if (Slots.InitializeSettings != null) Data = Slots.InitializeSettings(Data);
            else Data = new TStateData() { Config = DictionaryObjectFactory.FromDictionary<TConfig>(workflows.LoadConfig(configPath, ignored)) };
        }
        public void ValidateWorkflows()
        {
            if (Slots.InitializeApplications == null) throw new FrameworkWorkflowNotInitialized(nameof(Slots.InitializeApplications));
            if (Slots.Process == null) throw new FrameworkWorkflowNotInitialized(nameof(Slots.Process));
            if (Slots.CloseApplications == null) throw new FrameworkWorkflowNotInitialized(nameof(Slots.CloseApplications));
            if (Slots.GetTransactionData == null) throw new FrameworkWorkflowNotInitialized(nameof(Slots.GetTransactionData));
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
        public virtual void InitializeState(TestId testId)
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
                Data = Slots.CloseApplications(Data);
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
                    Data = Slots.InitializeApplications(Data);
                    Log("Applications initialized");
                });
                Data.Stack.Push(GetTransactionState);
            }
        }
        public virtual void GetTransactionState(TestId testId)
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
            Data = Slots.GetTransactionData(Data);
            if (Data.Transaction == null)
            {
                Log("No more queue items");
                return;
            }
            Log("Transaction found");
            Data.Stack.Push(ProcessState);
        }
        public virtual void ProcessState(TestId testId)
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
                Data = Slots.Process(Data);
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
                    Data = Slots.HandleSystemException != null ? Slots.HandleSystemException(Data) : Data;
                    Data.ConsecutiveSystemExceptions += 1;
                    if (Data.ConsecutiveSystemExceptions > Data.Config?.MaxConsecutiveExceptions)
                    {
                        Log("Reached maximum consecutive system exception");
                    }
                    else
                    {
                        Log($"Consecutive system exceptions: {Data.ConsecutiveSystemExceptions.ToString()} less than max, reinitializing");
                        Data.Stack.Push(InitializeState);
                    }
                }
                else if (Data.BusEx != null)
                {
                    SendErrorEmail();
                    Data = Slots.HandleBusinessException != null ? Slots.HandleBusinessException(Data) : Data;
                    Data.Stack.Push(GetTransactionState);
                }
                else
                {
                    Data = Slots.HandleSuccess != null ? Slots.HandleSuccess(Data) : Data;
                    Data.Stack.Push(GetTransactionState);
                }
            }
        }
        public virtual void EndState(TestId testId)
        {
            Data = Slots.End != null ? Slots.End(Data) : Data;
            if (Data.FrameEx != null) SendErrorEmail();

            try
            {
                Data = Slots.CloseApplications(Data);
            }
            catch
            {
                KillProcesses(Data.Config?.ProcessesToKill ?? new List<string>());
            }

            if (Data.FrameEx != null) throw Data.FrameEx;
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
