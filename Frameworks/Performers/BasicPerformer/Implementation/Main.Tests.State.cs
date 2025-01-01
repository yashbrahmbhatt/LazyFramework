using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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

namespace LazyFramework.DX.Shared.Frameworks.Performers.BasicPerformer.Implementation
{
    public class Main_Tests_State : BaseMain<Config, StateData, Workflow>
    {
        public Main_Tests_State()
        {
            InitializeApplications = workflows.InitializeApplications;
            CloseApplications = workflows.CloseApplications;
            Process = workflows.Process;
            GetTransactionData = workflows.GetTransactionData;
        }

        [TestCase]
        public new void RunTests()
        {
            var configPath = Path.Combine(Directory.GetCurrentDirectory(), "Frameworks\\Performers\\BasicPerformer\\Implementation\\Data\\Config.xlsx");
            var ignored = new List<string>();
            Log(workflows.ToString());
            base.RunTests(configPath, ignored);
            // Arrange
            Log("Test run started for Main_Tests_State.");

            // Act
            // For accessing UI Elements from Object Repository, you can use the Descriptors class e.g:
            // var screen = uiAutomation.Open(Descriptors.MyApp.FirstScreen);
            // screen.Click(Descriptors.MyApp.FirstScreen.SettingsButton);

            // Assert
            // To start using activities, use IntelliSense (CTRL + Space) to discover the available services, e.g. testing.VerifyExpression(...).
        }

        public override void SendEmail(List<string> to, List<string> cc, List<string> attachments, string body, string subject)
        {
            throw new NotImplementedException();
        }
    }
}