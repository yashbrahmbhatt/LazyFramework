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

namespace LazyFramework.DX.Shared.BasicPerformer
{
    public abstract partial class BaseMain<TConfig, TStateData/*, TWorkflow*/> : CodedWorkflow where TConfig : BaseConfig, new() where TStateData : BaseStateData<TConfig>, new() /* where TWorkflow : BaseWorkflow<TStateData, TConfig>*/
    {
        public abstract void SendEmail(List<string>? to, List<string>? cc, List<string> attachments, string body, string subject);
        public virtual void SendErrorEmail()
        {
            if (Data.FrameEx != null)
            {
                var template = new DiagnosticDictionary(Data.FrameEx);
                template["ProcessName"] = Data.Config?.ProcessName;
                var attachments = new List<string>() { SharedHelpers.TakeScreenshot(Data.Config?.Folder_ExScreenshot) };
                var (body, subject) = EmailHelpers.UpdateEmailTemplate(
                    Data.Config?.FrameEx_Body, Data.Config?.FrameEx_Subject, template.Data
                );

                SendEmail(Data.Config?.FrameEx_To, Data.Config?.FrameEx_CC, attachments, body, subject);
            }
            else if (Data.SysEx != null)
            {
                if (Data.Transaction.RetryNo >= Data.Config.MaxQueueRetries)
                {
                    var template = new DiagnosticDictionary(Data.SysEx);
                    template["ProcessName"] = Data.Config?.ProcessName;
                    var attachments = new List<string>() { SharedHelpers.TakeScreenshot(Data.Config?.Folder_ExScreenshot) };
                    var (body, subject) = EmailHelpers.UpdateEmailTemplate(
                        Data.Config?.TransSysEx_Body, Data.Config?.TransSysEx_Subject, template.Data
                    );

                    SendEmail(Data.Config?.TransSysEx_To, Data.Config?.TransSysEx_CC, attachments, body, subject);
                }
            }
            else if (Data.BusEx != null)
            {
                var template = new DiagnosticDictionary(Data.BusEx);
                template["ProcessName"] = Data.Config?.ProcessName;
                var attachments = new List<string>() { SharedHelpers.TakeScreenshot(Data.Config?.Folder_ExScreenshot) };
                var (body, subject) = EmailHelpers.UpdateEmailTemplate(
                    Data.Config?.TransBusEx_Body, Data.Config?.TransBusEx_Subject, template.Data
                );

                SendEmail(Data.Config?.TransBusEx_To, Data.Config?.TransBusEx_CC, attachments, body, subject);
            }
            else
            {
                throw new NotImplementedException("Unknown state for sending error email");
            }
        }
    }
}