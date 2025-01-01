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

namespace LazyFramework.DX.Shared.BasicPerformer.Implementation
{
    public class Main_Test : Main
    {
        [TestCase]
        public void RunTests()
        {
            WorkflowSlots.InitializeApplications = workflows.InitializeApplications;
            WorkflowSlots.CloseApplications = workflows.CloseApplications;
            WorkflowSlots.Process = workflows.Process;
            WorkflowSlots.GetTransactionData = workflows.GetTransactionData;
            var configPath = Path.Combine(Directory.GetCurrentDirectory(), "Frameworks\\Performers\\BasicPerformer\\Implementation\\Data\\Config.xlsx");
            var ignored = new List<string>();
            base.RunTests(configPath, ignored);
            // Arrange

            // Act
            // For accessing UI Elements from Object Repository, you can use the Descriptors class e.g:
            // var screen = uiAutomation.Open(Descriptors.MyApp.FirstScreen);
            // screen.Click(Descriptors.MyApp.FirstScreen.SettingsButton);

            // Assert
            // To start using activities, use IntelliSense (CTRL + Space) to discover the available services, e.g. testing.VerifyExpression(...).
        }
    }
}