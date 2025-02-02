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
        public void InitializeState(TestId testId) {
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
            base.InitializeState();
        }
        
        public void RunInitializeStateTests() {
            RunTest("InitializeState - Maintenance Time", TestId.InitializeMaintenanceTime, new List<VerifyMethod>() {
                (ex) => ("No test Exception", ex == null),
                (ex) => ("Only 2 states", StackHistory.Count == 2),
                (ex) => ("First state was initialize", StackHistory.ToArray().First() == States.Initialize.Method.Name),
                (ex) => ("Last state was End", StackHistory.ToArray().Last() == States.End.Method.Name)
            });
            RunTest("InitializeState - State Error", TestId.InitializeStateError, new List<VerifyMethod>() {
                (ex) => ("Exception raised", ex != null),
                (ex) => ("Framework exception not null", Data.FrameEx != null),
                (ex) => ("Only 2 states", StackHistory.Count == 2),
                (ex) => ("First state was initialize", StackHistory.ToArray().First() == States.Initialize.Method.Name),
                (ex) => ("Last state was End", StackHistory.ToArray().Last() == States.End.Method.Name)
            });
        }
    }
}