using System;
using System.Collections.Generic;
using Newtonsoft.Json;

// NOTICE: The Coded Automations feature is available as a preview feature and APIs may be subject to change. 
//         No warranty or technical support is provided for this preview feature.
//         Missing features or encountering bugs? Please click the feedback button in the top-right corner and let us know!
// Please delete these comments after you've read and acknowledged them. For more information, please visit the documentation over at https://docs.uipath.com/studio/lang-en/v2023.4/docs/coded-automations.
namespace LazyFramework
{
    namespace Performer
    {
        public class Config : BaseConfig
        {
            #region Setting

            public string ProcessName { get; set; }
            public string QueueName { get; set; }
            public string QueueFolder { get; set; }
            public string CredentialName_Email { get; set; }
            public string CredentialFolder_Email { get; set; }
            public string Folder_ExScreenshots { get; set; }
            public string ProcessesToKill { get; set; }
            public string Maintenance_Start { get; set; }
            public string Maintenance_End { get; set; }

            #endregion

            #region Constants

            public int MaxConsecutiveExceptions { get; set; }
            public int MaxQueueRetries { get; set; }

            #endregion

            #region Assets

            public string FrameEx_To { get; set; }
            public string FrameEx_CC { get; set; }
            public string TransBusEx_To { get; set; }
            public string TransBusEx_CC { get; set; }
            public string TransSysEx_To { get; set; }
            public string TransSysEx_CC { get; set; }
            public int SMTP_Port { get; set; }
            public string SMTP_Server { get; set; }

            #endregion

        }

        public class TextFiles : BaseConfig
        {
            public string FrameEx_Subject { get; set; }
            public string FrameEx_Body { get; set; }
            
            public string TransSysEx_Subject { get; set; }
            public string TransSysEx_Body { get; set; }
            public string TransBusEx_Subject { get; set; }
            public string TransBusEx_Body { get; set; }

        }
    }

    namespace Dispatcher
    {

        public class Config : BaseConfig
        {
            #region Setting

            public string ProcessName { get; set; }
            public string QueueName { get; set; }
            public string QueueFolder { get; set; }
            public string CredentialName_Email { get; set; }
            public string CredentialFolder_Email { get; set; }
            public string Folder_ExScreenshots { get; set; }

            #endregion

            #region Constants


            #endregion

            #region Assets

            public string FrameEx_To { get; set; }
            public string FrameEx_CC { get; set; }
            public int SMTP_Port { get; set; }
            public string SMTP_Server { get; set; }

            #endregion

        }
        public class TextFiles : BaseConfig
        {
            public string FrameEx_Subject { get; set; }
            public string FrameEx_Body { get; set; }

        }
    }

    namespace Common
    {
        public class QueueData : BaseConfig
        {
            public string Reference { get; set; }
            
            #region Inputs
            
            
            
            #endregion
            
            #region Outputs
            
            
            #endregion
        }
    }


    public class BaseConfig
    {

        #region Base

        object this[string key]
        {
            get => this[key];
            set => this[key] = value;
        }

        #endregion

        public T Load<T>(Dictionary<string, string> dictionary)
        {
            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(dictionary));
        }
        
        public T Load<T>(Dictionary<string, object> dictionary)
        {
            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(dictionary));
        }
    }
}