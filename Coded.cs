using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using LazyFramework.DX.Shared.BasicPerformer.Implementation;
using LazyFramework.DX.Shared.BasicPerformer.Implementation.Framework;
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

namespace LazyFramework.DX.Shared
{
    public class Coded : CodedWorkflow
    {
        public void InitializeServices(CodedWorkflow wf)
        {

        }
        public void InspectMethod(Delegate methodDelegate)
        {
            // Get the MethodInfo for the delegate
            MethodInfo methodInfo = methodDelegate.Method;

            // Get the declaring type
            Log($"Declaring Type: {methodInfo.DeclaringType}");

            // Get the method name
            Log($"Method Name: {methodInfo.Name}");

            // Get the file location (if available via Debug information)
            var location = methodInfo.DeclaringType?.Assembly.Location;
            Log($"File Location: {location}");
            

            // Get the return type
            Log($"Return Type: {methodInfo.ReturnType}");

            // Get the parameters
            var parameters = methodInfo.GetParameters();
            Log("Parameters:");
            foreach (var param in parameters)
            {
                Log($" - Name: {param.Name}, Type: {param.ParameterType}");
            }
        }

        [Workflow]
        public void Execute()
        {
            InitializeApplications wf = new InitializeApplications();
            InspectMethod(wf.Execute);
            var output = RunWorkflow(@"BasicPerformer\Implementation\Framework\InitializeApplications.cs", new Dictionary<string, object> { { "state", new StateData() } }, default, default, default);


            wf.Execute(new StateData());



            // To start using services, use IntelliSense (CTRL + Space) to discover the available services:
            // e.g. system.GetAsset(...)

            // For accessing UI Elements from Object Repository, you can use the Descriptors class e.g:
            // var screen = uiAutomation.Open(Descriptors.MyApp.FirstScreen);
            // screen.Click(Descriptors.MyApp.FirstScreen.SettingsButton);
        }
    }
}