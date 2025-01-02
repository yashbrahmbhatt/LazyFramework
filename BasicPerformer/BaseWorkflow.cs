using System;
using System.Collections.Generic;
using System.Data;
using LazyFramework.DX.Shared.BasicPerformer.Test;
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
        public abstract TStateData Execute(TStateData state);
    }


    public abstract class BaseTestCase<TConfig, TStateData> : CodedWorkflow where TConfig : BaseConfig, new() where TStateData : BaseStateData<TConfig>, new()
    {
        
        public TStateData State;
        public static TStateData ResetState(TStateData fromObject)
        {
            return JsonConvert.DeserializeObject<TStateData>(JsonConvert.SerializeObject(fromObject));
        }
        public static void ResetHistory(Stack<State> stack)
        {
            stack.Clear();
        }
        
        public virtual void Setup() {
        }
        
        public virtual void Teardown() {
            
        }


        public abstract TStateData Execute(TStateData state);
    }
}