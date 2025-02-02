using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using LazyFramework.DX.Shared.Models;
using UiPath.Core;
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
            base.GetTransactionState();
        }

        public void RunGetTransactionStateTests()
        {
            RunTest("GetTransaction - Maintenance Time", TestId.GetTransactionMaintenanceTime, new List<VerifyMethod>() {
                (ex) => ("No test Exception", ex == null),
                (ex) => ("Only 3 states",StackHistory.Count == 3),
                (ex) => ("First state was initialize",StackHistory.ToArray().First() == States.Initialize.Method.Name),
                (ex) => ("Second state was GetTransaction",StackHistory.ToArray()[1] == States.GetTransaction.Method.Name),
                (ex) => ("Last state was End",StackHistory.ToArray().Last() == States.End.Method.Name)
            });
            RunTest("GetTransaction - State Error", TestId.GetTransactionStateError, new List<VerifyMethod>() {
                (ex) => ("Exception raised", ex != null),
                (ex) => ("Framework exception not null", Data.FrameEx != null),
                (ex) => ("Only 3 states",StackHistory.Count == 3),
                (ex) => ("First state was initialize",StackHistory.ToArray().First() == States.Initialize.Method.Name),
                (ex) => ("Second state was GetTransaction",StackHistory.ToArray()[1] == States.GetTransaction.Method.Name),
                (ex) => ("Last state was End",StackHistory.ToArray().Last() == States.End.Method.Name)
            });
        }
    }
}