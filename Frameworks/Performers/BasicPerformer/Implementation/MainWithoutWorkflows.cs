using System;
using System.Collections.Generic;
using System.Data;
using UiPath.CodedWorkflows;
using UiPath.Core;
using UiPath.Core.Activities.Storage;
using UiPath.Excel;
using UiPath.Excel.Activities;
using UiPath.Excel.Activities.API;
using UiPath.Excel.Activities.API.Models;
using UiPath.Orchestrator.Client.Models;

namespace LazyFramework.DX.Shared.Frameworks.Performers.BasicPerformer.Implementation
{
    
    public class MainWithoutWorkflows : BaseMain<Config, StateData, Workflow>
    {
        [Workflow]
        public override void Execute(string configPath, List<string> ignored)
        {
            
            base.Execute(configPath, ignored);
            // To start using services, use IntelliSense (CTRL + Space) to discover the available services:
            // e.g. system.GetAsset(...)

            // For accessing UI Elements from Object Repository, you can use the Descriptors class e.g:
            // var screen = uiAutomation.Open(Descriptors.MyApp.FirstScreen);
            // screen.Click(Descriptors.MyApp.FirstScreen.SettingsButton);
        }

        public override void SendEmail(List<string> to, List<string> cc, List<string> attachments, string body, string subject)
        {
            Log($"Sending email with subject '{subject}'");
        }
    }
}