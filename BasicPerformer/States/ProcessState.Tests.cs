using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using LazyFramework.DX.Shared.Models;
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
    public partial class BaseTests<TConfig, TStateData> : BaseMain<TConfig, TStateData> where TConfig : BaseConfig, new() where TStateData : BaseStateData<TConfig>, new()
    {
        public void ProcessState(TestId testId)
        {
            switch (testId)
            {
                case TestId.ProcessStateError:
                    throw new Exception(nameof(testId));
                default:
                    break;
            }
            base.ProcessState();
        }

        public virtual TStateData ProcessSlot(TStateData state)
        {
            var reference = state.Transaction?.Reference ?? throw new Exception("Transaction must not be null when reaching the process slot.");
            if (reference == nameof(TestId.TransactionSystemException))
                throw new Exception(reference);
            else if (reference == nameof(TestId.TransactionBusinessException))
                throw new BusinessRuleException(reference);
            return state;
        }
        public void RunProcessStateTests()
        {
            Workflows.Process = ProcessSlot;
            system.AddQueueItem(Data.Config?.QueueName, Data.Config?.QueueFolder, default, new Dictionary<string, object>(), default, default, "ProcessStateError", default);
            RunTest("Process - State Error", TestId.ProcessStateError, new List<VerifyMethod>() {
                (ex) => ("Exception raised", ex != null),
                (ex) => ("Framework exception not null", Data.FrameEx != null),
                (ex) => ("Only 4 states", StackHistory.Count == 4),
                (ex) => ("First state was initialize", StackHistory.ToArray().First() == States.Initialize.Method.Name),
                (ex) => ("Second state was GetTransaction", StackHistory.ToArray()[1] == States.GetTransaction.Method.Name),
                (ex) => ("Third state was Process", StackHistory.ToArray()[2] == States.Process.Method.Name),
                (ex) => ("Last state was End", StackHistory.ToArray().Last() == States.End.Method.Name)
            });
            system.AddQueueItem(Data.Config?.QueueName, Data.Config?.QueueFolder, default, new Dictionary<string, object>(), default, default, "TransactionSystemException", default);
            var timestamp = DateTime.Now;
            RunTest("Process - Transaction System Exception", TestId.TransactionSystemException, new List<VerifyMethod>() {
                (ex) => ("Exception not raised", ex == null),
                (ex) => ("Framework exception is null", Data.FrameEx == null),
                (ex) => ("System exception is null", Data.SysEx == null),
                (ex) => ("6 states", StackHistory.Count == 3 + 3 * Data.Config?.MaxQueueRetries),
                (ex) => ("First state was initialize", StackHistory.ToArray().First() == States.Initialize.Method.Name),
                (ex) => ("Second state was GetTransaction", StackHistory.ToArray()[1] == States.GetTransaction.Method.Name),
                (ex) => ("Third state was Process", StackHistory.ToArray()[2] == States.Process.Method.Name),
                (ex) => ("Last state was End", StackHistory.ToArray().Last() == States.End.Method.Name),
                (ex) => {
                    var item = system.GetQueueItems(Data.Config?.QueueName, Data.Config?.QueueFolder, default, timestamp, default, QueueItemStates.Failed, default, ReferenceFilterStrategy.StartsWith, "TransactionSystemException", default, 100, default).FirstOrDefault(q=>true, null);
                    return ("Item has system exception",  item != null && item.ProcessingException.Type == ProcessingExceptionType.ApplicationException);
                }
            });
            system.AddQueueItem(Data.Config?.QueueName, Data.Config?.QueueFolder, default, new Dictionary<string, object>(), default, default, "TransactionBusinessException", default);
            timestamp = DateTime.Now;
            RunTest("Process - Transaction Business Exception", TestId.TransactionBusinessException, new List<VerifyMethod>() {
                (ex) => ("Exception not raised", ex == null),
                (ex) => ("Framework exception is null", Data.FrameEx == null),
                (ex) => ("Business exception is not null", Data.BusEx != null),
                (ex) => ("6 states", StackHistory.Count == 6),
                (ex) => ("First state was initialize", StackHistory.ToArray().First() == States.Initialize.Method.Name),
                (ex) => ("Second state was GetTransaction", StackHistory.ToArray()[1] == States.GetTransaction.Method.Name),
                (ex) => ("Third state was Process", StackHistory.ToArray()[2] == States.Process.Method.Name),
                (ex) => ("Last state was End", StackHistory.ToArray().Last() == States.End.Method.Name),
                (ex) => {
                    var item = system.GetQueueItems(Data.Config?.QueueName, Data.Config?.QueueFolder, default, timestamp, default, QueueItemStates.Failed, default, ReferenceFilterStrategy.StartsWith, "TransactionBusinessException", default, 1, default).FirstOrDefault(q=>true, null);
                    return ("Transaction has business exception", item != null && item.ProcessingException.Type == ProcessingExceptionType.ApplicationException);
                }
            });
        }
    }
}