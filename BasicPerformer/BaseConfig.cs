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
    public abstract class BaseConfig : DictionaryObject
    {
        public string ProcessName = "BasicPerformer";
        public string QueueName = "BasicPerformer";
        public string QueueFolder = "LazyFramework";
        public string Folder_ExScreenshot = "Data\\Exception Screenshots";
        public List<string> ProcessesToKill = new List<string>();
        public TimeSpan Maintenance_Start = new TimeSpan(0, 0, 0);
        public TimeSpan Maintenance_End = new TimeSpan(0, 0, 0);

        public int MaxConsecutiveExceptions = 5;
        public int MaxQueueRetries = 3;

        public List<string> FrameEx_To = new List<string>();
        public List<string> FrameEx_CC = new List<string>();
        public List<string> TransSysEx_To = new List<string>();
        public List<string> TransSysEx_CC = new List<string>();
        public List<string> TransBusEx_To = new List<string>();
        public List<string> TransBusEx_CC = new List<string>();

        public string FrameEx_Subject = "";
        public string FrameEx_Body = "";
        public string TransSysEx_Subject = "";
        public string TransSysEx_Body = "";
        public string TransBusEx_Subject = "";
        public string TransBusEx_Body = "";
    }
}