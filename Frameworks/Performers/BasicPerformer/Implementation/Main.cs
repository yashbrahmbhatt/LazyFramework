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
    public class Config : BaseConfig
    {

    }

    public class StateData : BaseStateData<Config>
    {

    }

    public abstract class Workflow : BaseWorkflow<StateData, Config>
    {

    }
    public class Main : BaseMain<Config, StateData, Workflow>
    {

        public Main() { }
        public override void SendEmail(List<string> to, List<string> cc, List<string> attachments, string body, string subject)
        {
            Log($"Sending email with subject '{subject}'");
        }

        [Workflow]
        public void Execute(string configPath, List<string> ignored)
        {
            WorkflowSlots.InitializeApplications = workflows.InitializeApplications;
            WorkflowSlots.CloseApplications = workflows.CloseApplications;
            WorkflowSlots.Process = workflows.Process;
            WorkflowSlots.GetTransactionData = workflows.GetTransactionData;
            
            RunFramework(configPath, ignored);
        }
    }
}