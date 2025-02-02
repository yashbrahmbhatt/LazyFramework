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
    public abstract partial class BaseMain<TConfig, TStateData/*, TWorkflow*/> : CodedWorkflow where TConfig : BaseConfig, new() where TStateData : BaseStateData<TConfig>, new() /* where TWorkflow : BaseWorkflow<TStateData, TConfig>*/
    {
        public virtual void EndState()
        {
            Data = Workflows.End != null ? Workflows.End(Data) : Data;
            if (Data.FrameEx != null) SendErrorEmail();

            try
            {
                Data = Workflows.CloseApplications(Data);
            }
            catch
            {
                KillProcesses(Data.Config?.ProcessesToKill ?? new List<string>());
            }

            if (Data.FrameEx != null) throw Data.FrameEx;
        }
    }
}