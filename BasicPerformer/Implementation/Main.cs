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

namespace LazyFramework.DX.Shared.BasicPerformer.Implementation
{
    public class Config : BaseConfig
    {

    }

    public class StateData : BaseStateData<Config>
    {

    }

    public abstract class FrameworkWorkflow : BaseWorkflow<StateData, Config>
    {
        
    }
    public class Main : BaseMain<Config, StateData, FrameworkWorkflow>
    {

        public Main() { }
        
        public override void SendEmail(List<string> to, List<string> cc, List<string> attachments, string body, string subject)
        {
            Log($"Sending email with subject '{subject}'");
        }

        [Workflow]
        public void Execute(string configPath, List<string> ignored)
        {
            
            Slots.InitializeApplications = workflows.InitializeApplications;
            Slots.CloseApplications = workflows.CloseApplications;
            Slots.Process = workflows.Process;
            Slots.GetTransactionData = workflows.GetTransactionData;
            
            RunFramework(configPath, ignored);
        }
        
        
    }
}