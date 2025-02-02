using UiPath.CodedWorkflows.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using LazyFramework.DX.Shared.BasicPerformer2.States.Initialize;
using LazyFramework.DX.Shared.Models;
using LazyFramework.DX.Shared.Models.StateMachine;
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

namespace LazyFramework.DX.Shared.BasicPerformer2.States
{
    public class InitializeStateSlots : WorkflowSlots<BaseStateData<BaseConfig>>
    {
        public Workflow<BaseStateData<BaseConfig>> InitializeSettings { get; set; } = null;
        public Workflow<BaseStateData<BaseConfig>> InitializeApplications { get; set; } = null;
        public Workflow<BaseStateData<BaseConfig>> CloseApplications { get; set; } = null;
    }
    public partial class BaseStates
    {
        public InitializeState Initialize { get; set; }
    }

    public class InitializeState : State<BaseStateData<BaseConfig>, InitializeStateSlots>
    {
        public override string Name { get; set; } = "InitializeState";
        public string ConfigPath { get; set; } = "";
        public List<string> Ignored { get; set; } = new List<string>();

        public InitializeState() : base()
        {
            Slots = new();
            //Slots.InitializeSettings = new InitializeSettings();
        }
        public InitializeState(string configPath, List<string> ignored) : base()
        {
            Slots = new();
            ConfigPath = configPath;
            Ignored = ignored;
            Slots.InitializeSettings = new InitializeSettings(services, ConfigPath, Ignored);
            Log("State Constructor!");
        }

        [Workflow]
        public override void Execute(BaseStateData<BaseConfig> state)
        {
            Log("Entering Initialize...");
            Slots.InitializeSettings = new InitializeSettings(services, ConfigPath, Ignored);
            Slots.InitializeSettings.Execute(state);
        }
    }
}