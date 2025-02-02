using System;
using System.Collections.Generic;
using System.Data;
using LazyFramework.DX.Shared.Models;
using LazyFramework.DX.Shared.Models.StateMachine;
using UiPath.CodedWorkflows;
using UiPath.CodedWorkflows.Interfaces;
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

namespace LazyFramework.DX.Shared.BasicPerformer2.States.Initialize
{
    public class InitializeSettings : Workflow<BaseStateData<BaseConfig>>
    {
        public string ConfigPath {get; set;}
        public List<string> Ignored {get; set;}
        
        public InitializeSettings(){}
        public InitializeSettings(ICodedWorkflowServices _services, string configPath, List<string> ignored) {
            services = _services;
            ConfigPath = configPath;
            Ignored = ignored;
        }
        
        [Workflow]
        public override BaseStateData<BaseConfig> Execute(BaseStateData<BaseConfig> state)
        {
            Log($"Initialize settings!");
            // To start using services, use IntelliSense (CTRL + Space) to discover the available services:
            // e.g. system.GetAsset(...)

            // For accessing UI Elements from Object Repository, you can use the Descriptors class e.g:
            // var screen = uiAutomation.Open(Descriptors.MyApp.FirstScreen);
            // screen.Click(Descriptors.MyApp.FirstScreen.SettingsButton);
            return state;
        }
    }
}