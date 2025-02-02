using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using LazyFramework.DX.Shared.BasicPerformer;
using LazyFramework.DX.Shared.Models;
using Newtonsoft.Json;
using UiPath.Core;
using UiPath.Core.Activities;
using UiPath.Core.Activities.Storage;
using UiPath.Excel;
using UiPath.Excel.Activities;
using UiPath.Excel.Activities.API;
using UiPath.Excel.Activities.API.Models;
using UiPath.Orchestrator.Client.Models;
using UiPath.Testing;
using UiPath.Testing.Activities.Api.Models;
using UiPath.Testing.Activities.Models;
using UiPath.Testing.Activities.TestData;
using UiPath.Testing.Activities.TestDataQueues.Enums;
using UiPath.Testing.Enums;

namespace LazyFramework.DX.Shared.BasicPerformer
{
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
    public partial class BaseTests<TConfig, TStateData> : BaseMain<TConfig, TStateData> where TConfig : BaseConfig, new() where TStateData : BaseStateData<TConfig>, new()
    {
        // Fields
        public override BaseStateSlots States { get; set; } = new();
        public FixedSizeQueue<string> StackHistory = new FixedSizeQueue<string>(10);
        public TStateData InitialData = null;

        //Constructors
        public BaseTests() : base() { }

        // Entry
        public virtual void RunTests(string configPath, List<string> ignored)
        {
            InitializeSettings(configPath, ignored);
            InitialData = DesSer(Data);
            RunInitializeStateTests();
            RunGetTransactionStateTests();
            RunProcessStateTests();
        }

        // Overrides
        public new void RunStateMachine(params object[] param)
        {
            Data.Stack.Push(States.Initialize);
            while (Data.Stack.Count > 0)
            {
                try
                {
                    var currentState = Data.Stack.Pop();
                    StackHistory.Enqueue(currentState.Method.Name);
                    currentState.DynamicInvoke(param);
                }
                catch (Exception e)
                {
                    Data.FrameEx = e;
                }
            }
            StackHistory.Enqueue(States.End.Method.Name);
            States.End.DynamicInvoke(param);
        }

        // Helpers
        public delegate (string verificationName, bool result) VerifyMethod(Exception testException);

        public virtual void RunTest(string name, TestId testId, List<VerifyMethod> verifications)
        {
            Data = DesSer<TStateData>(InitialData as TStateData);
            ResetHistory();
            Exception ex = null;
            Log($"Starting test '{name}'");
            Log($"States: {States.ToString()}");
            Log($"Workflows: {Workflows.ToString()}");
            try
            {
                RunStateMachine(testId);
            }
            catch (Exception e)
            {
                Log($"Test Exception raised: {e.Message}\n{e.StackTrace}", UiPath.CodedWorkflows.LogLevel.Error);
                ex = e;
            }
            Log($"Test completed. StackHistory: {StackHistory.ToString()}");
            Log($"Starting {verifications.Count} verifications");
            foreach (VerifyMethod verification in verifications)
            {
                var result = verification(ex);
                testing.VerifyExpression(result.result, $"Verification '{result.verificationName}' has result {{Result}}", true, result.verificationName, false, false);
            }
            Log($"Test '{name}' complete");
        }
        public static T DesSer<T>(T fromObject)
        {
            return (T)JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(fromObject));
        }
        public virtual void ResetHistory()
        {
            StackHistory.Clear();
        }

        public override void SendEmail(List<string> to, List<string> cc, List<string> attachments, string body, string subject)
        {
            Log($"Email would be sent here!");
        }
    }
}