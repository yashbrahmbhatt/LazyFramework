using System;
using System.Collections.Generic;

// NOTICE: The Coded Automations feature is available as a preview feature and APIs may be subject to change. 
//         No warranty or technical support is provided for this preview feature.
//         Missing features or encountering bugs? Please click the feedback button in the top-right corner and let us know!
// Please delete these comments after you've read and acknowledged them. For more information, please visit the documentation over at https://docs.uipath.com/studio/lang-en/v2023.4/docs/coded-automations.
namespace LazyFramework
{
    public class DispatcherConfig : Config
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

        public string logM_Start { get; set; }
        public string logM_Complete { get; set; }

        #endregion

        #region Assets

        public string SysEx_To { get; set; }
        public string SysEx_CC { get; set; }
        public string SMTP_Port { get; set; }
        public string SMTP_Server { get; set; }

        #endregion

        public override DispatcherConfig Load(Dictionary<string, string> dictionary)
        {
            return (DispatcherConfig)base.Load(dictionary);
        }

    }

    public class TextFiles : Config
    {
        public string SysEx_Subject { get; set; }
        public string SysEx_Body { get; set; }

        public override TextFiles Load(Dictionary<string, string> dictionary)
        {
            return (TextFiles)base.Load(dictionary);
        }
    }

    public abstract class Config
    {

        #region Base

        object this[string key]
        {
            get => this[key];
            set => this[key] = value;
        }

        #endregion

        public virtual Config Load(Dictionary<string, string> dictionary)
        {
            foreach (string key in dictionary.Keys)
            {
                this[key] = dictionary[key];
            }
            return this;
        }
    }
}