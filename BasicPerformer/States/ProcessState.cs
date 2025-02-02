using System;
using System.Collections.Generic;
using System.Data;
using UiPath.Core;
using UiPath.Core.Activities;
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
        public virtual void ProcessState()
        {

            ProcessingStatus outcome = ProcessingStatus.Successful;
            Dictionary<string, object> analytics = new();
            string details = "";
            string reason = "";
            ErrorType errorType = ErrorType.Application;
            Data.BusEx = null;
            try
            {
                Data = Workflows.Process(Data);
                Data.ConsecutiveSystemExceptions = 0;
            }
            catch (BusinessRuleException bre)
            {
                outcome = ProcessingStatus.Failed;
                errorType = ErrorType.Business;
                details = bre.Message;
                reason = bre.StackTrace ?? "";
                Data.BusEx = bre;
            }
            catch (Exception se)
            {
                outcome = ProcessingStatus.Failed;
                errorType = ErrorType.Application;
                reason = se.Message;
                details = se.StackTrace ?? "";
                Data.SysEx = se;
            }
            finally
            {
                system.SetTransactionStatus(
                    Data.Transaction,
                    outcome,
                    Data.Config?.QueueFolder,
                    analytics ?? new Dictionary<string, object>(),
                    Data.Transaction?.Output ?? new Dictionary<string, object>(),
                    details,
                    errorType,
                    reason,
                    15000
                );
                if (Data.SysEx != null)
                {
                    SendErrorEmail();
                    Data = Workflows.HandleSystemException != null ? Workflows.HandleSystemException(Data) : Data;
                    Data.ConsecutiveSystemExceptions += 1;
                    if (Data.ConsecutiveSystemExceptions > Data.Config?.MaxConsecutiveExceptions)
                    {
                        Log("Reached maximum consecutive system exception");
                    }
                    else
                    {
                        Log($"Consecutive system exceptions: {Data.ConsecutiveSystemExceptions.ToString()} less than max, reinitializing");
                        Data.Stack.Push(States.Initialize);
                    }
                }
                else if (Data.BusEx != null)
                {
                    SendErrorEmail();
                    Data = Workflows.HandleBusinessException != null ? Workflows.HandleBusinessException(Data) : Data;
                    Data.Stack.Push(States.GetTransaction);
                }
                else
                {
                    Data = Workflows.HandleSuccess != null ? Workflows.HandleSuccess(Data) : Data;
                    Data.Stack.Push(States.GetTransaction);
                }
            }
        }
    }
}