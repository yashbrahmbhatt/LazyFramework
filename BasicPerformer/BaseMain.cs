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

    public class BaseStateSlots
    {
        public Delegate Initialize { get; set; }
        public Delegate GetTransaction { get; set; }
        public Delegate Process { get; set; }
        public Delegate End { get; set; }

        public override string ToString()
        {
            var list = new List<Delegate>() { Initialize, GetTransaction, Process, End };
            var str = string.Join(", ", list.Select(d => d == null ? "null" : $"{d.Method.Name} ({d.Method.DeclaringType})"));
            return $"[{str}]";
        }
    }

    public class BaseWorkflowSlots<TConfig, TStateData/*, TWorkflow*/> where TConfig : BaseConfig, new() where TStateData : BaseStateData<TConfig>, new() /* where TWorkflow : BaseWorkflow<TStateData, TConfig>*/
    {
        public delegate TStateData ExecuteDelegate(TStateData state);

        public ExecuteDelegate? InitializeSettings { get; set; } = null;
        public ExecuteDelegate InitializeApplications { get; set; } = DefaultExecuteDelegate("InitializeApplications");
        public ExecuteDelegate Process { get; set; } = DefaultExecuteDelegate("Process");
        public ExecuteDelegate GetTransactionData { get; set; } = DefaultExecuteDelegate("GetTransactionData");
        public ExecuteDelegate? HandleBusinessException { get; set; } = null;
        public ExecuteDelegate? HandleSystemException { get; set; } = null;
        public ExecuteDelegate? HandleSuccess { get; set; } = null;
        public ExecuteDelegate? End { get; set; } = null;
        public ExecuteDelegate CloseApplications { get; set; } = DefaultExecuteDelegate("CloseApplications");

        public BaseWorkflowSlots() { }

        public static ExecuteDelegate DefaultExecuteDelegate(string name)
        {
            return (TStateData state) =>
            {
                return state;
            };
        }

        public override string ToString()
        {
            var list = new List<ExecuteDelegate>(){
                InitializeSettings,
                InitializeApplications,
                Process,
                GetTransactionData,
                HandleBusinessException,
                HandleSystemException,
                HandleSuccess,
                End
            };
            var str = string.Join(", ", list.Select(d => d == null ? "null" : $"{d.Method.Name} ({d.Method.DeclaringType})"));
            return $"[{str}]";
        }
    }
    public abstract partial class BaseMain<TConfig, TStateData/*, TWorkflow*/> : CodedWorkflow where TConfig : BaseConfig, new() where TStateData : BaseStateData<TConfig>, new() /* where TWorkflow : BaseWorkflow<TStateData, TConfig>*/
    {
        // Properties
        public virtual BaseStateSlots States { get; set; } = new();
        public virtual BaseWorkflowSlots<TConfig, TStateData> Workflows { get; set; } = new();
        public virtual TStateData Data { get; set; } = new();


        // Constructors
        public BaseMain() : base() { }

        // Entry
        public virtual void RunFramework(string configPath, List<string> ignored)
        {
            ValidateWorkflows();
            InitializeSettings(configPath, ignored);
            RunStateMachine();
        }

        // Framework Methods
        public virtual bool IsMaintenanceTime()
        {
            Log($"Checking if currently ({DateTime.Now.ToString()} between maintenance times ({Data.Config?.Maintenance_Start} {Data.Config?.Maintenance_End})");
            return SharedHelpers.CurrentlyBetweenTimes(Data.Config?.Maintenance_Start, Data.Config?.Maintenance_End);
        }
        public virtual void RunStateMachine(params object[] param)
        {
            Log("The primary screen resolution is: " + SystemParameters.PrimaryScreenWidth.ToString() + " x " + SystemParameters.PrimaryScreenHeight.ToString());
            Data.Stack.Push(States.Initialize);
            while (Data.Stack.Count > 0)
            {
                try
                {
                    var currentState = Data.Stack.Pop();
                    currentState.DynamicInvoke(param);
                }
                catch (Exception e)
                {
                    Data.FrameEx = e;
                }
            }
            States.End.DynamicInvoke(param);
        }
        public virtual void InitializeSettings(string configPath, List<string> ignored)
        {
            // System.Windows.MessageBox.Show("The primary screen resolution is: " + SystemParameters.PrimaryScreenWidth.ToString() + " x " + SystemParameters.PrimaryScreenHeight.ToString());
            // File.WriteAllText("text.json", JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
            if (Workflows.InitializeSettings != null) Data = Workflows.InitializeSettings(Data);
            else Data = new TStateData() { Config = DictionaryObjectFactory.FromDictionary<TConfig>(workflows.LoadConfig(configPath, ignored)) };
        }
        public virtual void ValidateWorkflows()
        {
            if (Workflows.InitializeApplications == null) throw new FrameworkWorkflowNotInitialized(nameof(Workflows.InitializeApplications));
            if (Workflows.Process == null) throw new FrameworkWorkflowNotInitialized(nameof(Workflows.Process));
            if (Workflows.CloseApplications == null) throw new FrameworkWorkflowNotInitialized(nameof(Workflows.CloseApplications));
            if (Workflows.GetTransactionData == null) throw new FrameworkWorkflowNotInitialized(nameof(Workflows.GetTransactionData));
        }
        public virtual void KillProcesses(List<string> processNames)
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

        public class InvalidFrameworkStateException : Exception
        {
            public InvalidFrameworkStateException() : base() { }

            public InvalidFrameworkStateException(string message) : base(message) { }

            public InvalidFrameworkStateException(string message, Exception innerException) : base(message, innerException) { }


        }
    }

}

