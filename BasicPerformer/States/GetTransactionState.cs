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
        public virtual void GetTransactionState()
        {
            Log("Getting transaction");
            if (IsMaintenanceTime())
            {
                Log("Currently within maintenance window");
                return;
            }
            Data = Workflows.GetTransactionData(Data);
            if (Data.Transaction == null)
            {
                Log("No more queue items");
                return;
            }
            Log("Transaction found");
            Data.Stack.Push(States.Process);
        }
    }
}