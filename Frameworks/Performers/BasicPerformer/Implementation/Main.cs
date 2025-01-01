using System;
using System.Collections.Generic;
using System.Data;
using Newtonsoft.Json;
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
    public class Config : BaseConfig {
        
    }
    
    public class StateData : BaseStateData<Config>{
        
    }
    
    public abstract class Workflow : BaseWorkflow<StateData, Config> {
        
    }
    
    public class Main : BaseMain<Config, StateData, Workflow>
    {
        public Main() {
            InitializeApplications = workflows.InitializeApplications;
            CloseApplications = workflows.CloseApplications;
            Process = workflows.Process;
            GetTransactionData = workflows.GetTransactionData;
        }
        
        [Workflow]
        public void Execute(string configPath, List<string> ignored)
        {
            // Log(JsonConvert.SerializeObject(CloseApplications, Formatting.Indented, new JsonSerializerSettings(){ReferenceLoopHandling=ReferenceLoopHandling.Ignore}));
            workflows.CloseApplications(new StateData());
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