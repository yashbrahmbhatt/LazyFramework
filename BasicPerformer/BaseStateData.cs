using System;
using System.Collections.Generic;
using System.Data;
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
    public abstract class BaseStateData<TConfig> where TConfig : BaseConfig
    {
        public TConfig? Config = null;
        public Exception? SysEx = null;
        public Exception? FrameEx = null;
        public BusinessRuleException? BusEx = null;
        public QueueItem? Transaction = null;
        public int ConsecutiveSystemExceptions = 0;
    }
}