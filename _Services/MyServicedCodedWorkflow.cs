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
using UiPath.Testing;
using UiPath.Testing.Activities.Api.Models;
using UiPath.Testing.Activities.Models;
using UiPath.Testing.Activities.TestData;
using UiPath.Testing.Activities.TestDataQueues.Enums;
using UiPath.Testing.Enums;

namespace LazyFramework.DX.Shared._Services
{
    public partial class CodedWorkflow : CodedWorkflowBase
    {
        public IMyService myService { get => serviceContainer.Resolve<IMyService>(); }

        protected override void RegisterServices(ICodedWorkflowsServiceLocator serviceLocator)
        {
            // Implementation using 'RegisterType'
            serviceLocator.RegisterType<IMyService, MyService>();
            // Implementation using 'RegisterInstance'
        }
    }
}