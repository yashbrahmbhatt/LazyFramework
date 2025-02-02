using System;
using System.Collections.Generic;
using System.Data;
using LazyFramework.DX.Shared.Models;
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

namespace LazyFramework.DX.Shared.BasicPerformer2
{
    public class BaseStateData<TConfig> : DictionaryObject where TConfig : DictionaryObject, new()
    {
        public Exception SysEx { get; set; } = null;
        public TConfig Config {get; set;} = new();
        
        public BaseStateData(){}
    }
}