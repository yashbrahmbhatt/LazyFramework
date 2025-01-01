using System;

namespace LazyFramework.DX.Shared.Models
{
    public class DiagnosticDictionary : DictionaryObject
    {
        public string Env_MachineName { get; set; } = Environment.MachineName;
        public string Env_User { get; set; } = Environment.UserName;
        public string Env_Date { get; set; } = DateTime.Now.ToString("yyyy-MM-dd");
        public string Env_Time { get; set; } = DateTime.Now.ToString("HH:mm:ss");
        public string Ex_Message { get; set; }
        public string Ex_Stack { get; set; }
        public string Ex_Source { get; set; }
        public DiagnosticDictionary(Exception e)
        {
            Ex_Message = e.Message;
            Ex_Stack = e.StackTrace;
            Ex_Source = e.Source;
        }
    }
}