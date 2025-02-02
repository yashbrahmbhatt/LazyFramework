using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Newtonsoft.Json;
using UiPath.CodedWorkflows;
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

namespace LazyFramework.DX.Shared.BasicPerformer.Implementation
{
    public class TestFramework : BaseTests<Config, StateData>
    {
        [TestCase]
        public void RunTests()
        {
            var configPath = @"BasicPerformer\Implementation\Data\Config.xlsx";
            var ignored = new List<string>();
            
            Workflows.InitializeApplications = workflows.InitializeApplications;
            Workflows.Process = workflows.Process;
            Workflows.CloseApplications = workflows.CloseApplications;
            Workflows.GetTransactionData = workflows.GetTransactionData;

            States.Initialize = (TestId testId) => InitializeState(testId);
            States.GetTransaction = (TestId testId) => GetTransactionState(testId);
            States.Process = (TestId testId) => ProcessState(testId);
            States.End = (TestId testId) => EndState();
            Log("Starting tests!");
            Log(States.ToString());
            base.RunTests(configPath, ignored);
        }

    }
}