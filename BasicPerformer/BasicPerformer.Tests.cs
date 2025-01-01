using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
    public class BasicPerformerTests<TConfig, TStateData, TWorkflow> : BasicPerformer.BaseMain<TConfig, TStateData, TWorkflow> where TConfig : BaseConfig, new() where TStateData : BaseStateData<TConfig>, new() where TWorkflow : BaseWorkflow<TStateData, TConfig>
    {
        public FixedSizeQueue<string> StackHistory = new FixedSizeQueue<string>(10);
        public TStateData InitialData;
        // Tests
        public virtual void RunTests(string configPath, List<string> ignored)
        {
            InitializeFramework(configPath, ignored);
            RunTest("InitializeState - Maintenance Time", TestId.InitializeMaintenanceTime, new List<VerifyTest>() {
                (ex, data, history) => ("No test Exception", ex == null),
                (ex, data, history) => ("Only 2 states", (history as FixedSizeQueue<string>).Count == 2),
                (ex, data, history) => ("First state was initialize", (history as FixedSizeQueue<string>).ToArray().First() == "InitializeState"),
                (ex, data, history) => ("Last state was End", (history as FixedSizeQueue<string>).ToArray().Last() == "EndState")
            });
            RunTest("InitializeState - State Error", TestId.InitializeStateError, new List<VerifyTest>() {
                (ex, data, history) => ("Exception raised", ex != null),
                (ex, data, history) => ("Framework exception not null", Data.FrameEx != null),
                (ex, data, history) => ("Only 2 states", (history as FixedSizeQueue<string>).Count == 2),
                (ex, data, history) => ("First state was initialize", (history as FixedSizeQueue<string>).ToArray().First() == "InitializeState"),
                (ex, data, history) => ("Last state was End", (history as FixedSizeQueue<string>).ToArray().Last() == "EndState")
            });
            RunTest("GetTransaction - Maintenance Time", TestId.GetTransactionMaintenanceTime, new List<VerifyTest>() {
                (ex, data, history) => ("No test Exception", ex == null),
                (ex, data, history) => ("Only 3 states", (history as FixedSizeQueue<string>).Count == 3),
                (ex, data, history) => ("First state was initialize", (history as FixedSizeQueue<string>).ToArray().First() == "InitializeState"),
                (ex, data, history) => ("Second state was GetTransaction", (history as FixedSizeQueue<string>).ToArray()[1] == "GetTransactionState"),
                (ex, data, history) => ("Last state was End", (history as FixedSizeQueue<string>).ToArray().Last() == "EndState")
            });
            RunTest("GetTransaction - State Error", TestId.GetTransactionStateError, new List<VerifyTest>() {
                (ex, data, history) => ("Exception raised", ex != null),
                (ex, data, history) => ("Framework exception not null", Data.FrameEx != null),
                (ex, data, history) => ("Only 3 states", (history as FixedSizeQueue<string>).Count == 3),
                (ex, data, history) => ("First state was initialize", (history as FixedSizeQueue<string>).ToArray().First() == "InitializeState"),
                (ex, data, history) => ("Second state was GetTransaction", (history as FixedSizeQueue<string>).ToArray()[1] == "GetTransactionState"),
                (ex, data, history) => ("Last state was End", (history as FixedSizeQueue<string>).ToArray().Last() == "EndState")
            });
            system.AddQueueItem(Data.Config?.QueueName, Data.Config?.QueueFolder, default, new Dictionary<string, object>(), default, default, "ProcessStateError", default);
            RunTest("Process - State Error", TestId.ProcessStateError, new List<VerifyTest>() {
                (ex, data, history) => ("Exception raised", ex != null),
                (ex, data, history) => ("Framework exception not null", Data.FrameEx != null),
                (ex, data, history) => ("Only 4 states", (history as FixedSizeQueue<string>).Count == 4),
                (ex, data, history) => ("First state was initialize", (history as FixedSizeQueue<string>).ToArray().First() == "InitializeState"),
                (ex, data, history) => ("Second state was GetTransaction", (history as FixedSizeQueue<string>).ToArray()[1] == "GetTransactionState"),
                (ex, data, history) => ("Third state was Process", (history as FixedSizeQueue<string>).ToArray()[2] == "ProcessState"),
                (ex, data, history) => ("Last state was End", (history as FixedSizeQueue<string>).ToArray().Last() == "EndState")
            });
            system.AddQueueItem(Data.Config?.QueueName, Data.Config?.QueueFolder, default, new Dictionary<string, object>(), default, default, "TransactionSystemException", default);
            var timestamp = DateTime.Now;
            RunTest("Process - Transaction System Exception", TestId.TransactionSystemException, new List<VerifyTest>() {
                (ex, data, history) => ("Exception not raised", ex == null),
                (ex, data, history) => ("Framework exception is null", Data.FrameEx == null),
                (ex, data, history) => ("System exception is not null", Data.SysEx != null),
                (ex, data, history) => ("6 states", (history as FixedSizeQueue<string>).Count == 6),
                (ex, data, history) => ("First state was initialize", (history as FixedSizeQueue<string>).ToArray().First() == "InitializeState"),
                (ex, data, history) => ("Second state was GetTransaction", (history as FixedSizeQueue<string>).ToArray()[1] == "GetTransactionState"),
                (ex, data, history) => ("Third state was Process", (history as FixedSizeQueue<string>).ToArray()[2] == "ProcessState"),
                (ex, data, history) => ("Last state was End", (history as FixedSizeQueue<string>).ToArray().Last() == "EndState"),
                (ex, data, history) => {
                    var item = system.GetQueueItems(Data.Config?.QueueName, Data.Config?.QueueFolder, default, timestamp, default, QueueItemStates.Failed, default, ReferenceFilterStrategy.StartsWith, "TransactionSystemException", default, 1, default).First();
                    return ("Item has system exception",  item != null && item.ProcessingException.Type == ProcessingExceptionType.ApplicationException);
                }
            });
            system.AddQueueItem(Data.Config?.QueueName, Data.Config?.QueueFolder, default, new Dictionary<string, object>(), default, default, "TransactionBusinessException", default);
            timestamp = DateTime.Now;
            RunTest("Process - Transaction System Exception", TestId.TransactionBusinessException, new List<VerifyTest>() {
                (ex, data, history) => ("Exception not raised", ex == null),
                (ex, data, history) => ("Framework exception is null", Data.FrameEx == null),
                (ex, data, history) => ("Business exception is not null", Data.BusEx != null),
                (ex, data, history) => ("6 states", (history as FixedSizeQueue<string>).Count == 6),
                (ex, data, history) => ("First state was initialize", (history as FixedSizeQueue<string>).ToArray().First() == "InitializeState"),
                (ex, data, history) => ("Second state was GetTransaction", (history as FixedSizeQueue<string>).ToArray()[1] == "GetTransactionState"),
                (ex, data, history) => ("Third state was Process", (history as FixedSizeQueue<string>).ToArray()[2] == "ProcessState"),
                (ex, data, history) => ("Last state was End", (history as FixedSizeQueue<string>).ToArray().Last() == "EndState"),
                (ex, data, history) => {
                    var item = system.GetQueueItems(Data.Config?.QueueName, Data.Config?.QueueFolder, default, timestamp, default, QueueItemStates.Failed, default, ReferenceFilterStrategy.StartsWith, "TransactionBusinessException", default, 1, default).First();
                    return ("Transaction has business exception", item != null && item.ProcessingException.Type == ProcessingExceptionType.ApplicationException);
                }
            });
        }
        public virtual void InitializeFramework(string configPath, List<string> ignored) {
            base.InitializeFramework(configPath, ignored);
            InitialData = JsonConvert.DeserializeObject<TStateData>(JsonConvert.SerializeObject(Data));
        }
        public delegate (string verificationName, bool result) VerifyTest(Exception testException, TStateData data, FixedSizeQueue<string> history);
        public void RunTest(string name, TestId testId, List<VerifyTest> verifications)
        {
            Log($"Starting test '{name}'");
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
            Log($"Starting {verifications.Count} verifications");
            foreach (VerifyTest verification in verifications)
            {
                var result = verification(ex, Data, StackHistory);
                testing.VerifyExpression(result.result, $"Verification '{result.verificationName}' has result {{Result}}", true, result.verificationName, false, false);
            }
            Log($"Test '{name}' complete");
        }

        // Resets everything except config
        public void ResetState()
        {
            Data = JsonConvert.DeserializeObject<TStateData>(JsonConvert.SerializeObject(InitialData));
        }
        public void ResetHistory()
        {
            StackHistory.Clear();
        }

        public override void SendEmail(List<string> to, List<string> cc, List<string> attachments, string body, string subject)
        {
            throw new NotImplementedException();
        }
    }
}