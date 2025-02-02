using System;
using System.Collections.Generic;
using System.Data;
using LazyFramework.DX.Shared.Models;
using Newtonsoft.Json;
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

namespace LazyFramework.DX.Shared.BasicPerformer
{
    public abstract class BaseWorkflow<TConfig, TStateData> : CodedWorkflow where TConfig : BaseConfig, new() where TStateData : BaseStateData<TConfig>, new()
    {
        public ExecuteDelegate Delegate;
        public BaseWorkflow() { }
        public BaseWorkflow(ExecuteDelegate del)
        {
            Delegate = del;
        }
        public delegate TStateData ExecuteDelegate(TStateData state);
        public virtual TStateData Execute(TStateData state)
        {
            if (Delegate != null) state = (TStateData)Delegate.DynamicInvoke(state);
            return state;
        }
    }


    public abstract class BaseTestCase<TConfig, TStateData> : CodedWorkflow where TConfig : BaseConfig, new() where TStateData : BaseStateData<TConfig>, new()
    {

        public ExecuteDelegate Delegate = null;
        public BaseTestCase() { }
        public BaseTestCase(ExecuteDelegate del)
        {
            Delegate = del;
        }
        public delegate void ExecuteDelegate(TestId testId);
        public virtual void Execute(TestId testId)
        {
            if (Delegate != null) Delegate.DynamicInvoke(testId);
        }
    }
}