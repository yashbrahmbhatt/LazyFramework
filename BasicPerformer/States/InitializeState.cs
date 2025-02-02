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
        public virtual void InitializeState()
        {
            Log("Entering Initialize...");
            Data.SysEx = null;

            try
            {
                Log("Trying to close applications");
                Data = Workflows.CloseApplications(Data);
                Log("Applications closed successfully");

            }
            catch
            {
                Log("Failed to close applications, killing processes instead");
                KillProcesses(Data.Config?.ProcessesToKill ?? new List<string>());
            }
            if (IsMaintenanceTime())
            {
                Log("Within maintenance window");
            }
            else
            {

                SharedHelpers.Retry<bool>(() =>
                {
                    Log("Initializing applications...");
                    Data = Workflows.InitializeApplications(Data);
                    Log("Applications initialized");
                });
                Data.Stack.Push(States.GetTransaction);
            }
        }
    }
}