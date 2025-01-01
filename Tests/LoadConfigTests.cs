using System;
using System.Collections.Generic;
using System.Data;
using LazyFramework.DX.Shared.Models;
using UiPath.CodedWorkflows;
using Newtonsoft.Json;

namespace LazyFramework.DX.Shared.Tests
{
    public class LoadConfigTests : CodedWorkflow
    {

        public class TestConfig : DictionaryObject
        {
            public string ShouldLoad {get; set;}
            public bool ShouldLoadAsset;
            public string LocalTextFile;
            public DataSet LocalExcelFile;

            public List<string> StringListValue;
            public List<int> IntListValue;
            public List<double> DoubleListValue;
            public double DoubleValue;
            public bool BoolValue;
            public DateTime DateTimeValue;
            public TimeSpan TimespanValue;
        }
        [TestCase]
        public void Execute(string filePath, List<string> ignored)
        {
            // Arrange
            Log("Test run started for LoadConfig_Tests.");
            filePath = @"Tests\\Data\\LoadConfigTest.xlsx";
            ignored = new List<string>() { "Ignore" };
            Dictionary<string, object> dict = null;
            Exception ex = null;
            try
            {
                dict = workflows.LoadConfig(filePath, ignored);
            }
            catch (Exception e)
            {
                ex = e;
            }
            testing.VerifyExpression(ex == null);
            testing.VerifyExpression(dict != null);
            testing.VerifyExpression(dict.Keys.Contains("ShouldLoad"));
            testing.VerifyExpression(!dict.Keys.Contains("ShouldNotLoad"));
            testing.VerifyExpression(dict.Keys.Contains("ShouldLoadAsset"));
            testing.VerifyExpression(dict.Keys.Contains("LocalTextFile") && dict["LocalTextFile"] is string);
            testing.VerifyExpression(dict.Keys.Contains("LocalExcelFile") && dict["LocalExcelFile"] is DataSet);

            TestConfig config = null;
            try
            {
                config = DictionaryObjectFactory.FromDictionary<TestConfig>(dict);
            }
            catch (Exception e)
            {
                ex = e;
            }
            testing.VerifyExpression(ex == null);
            testing.VerifyExpression(config.StringListValue != null);
            testing.VerifyExpression(config.IntListValue != null);
            testing.VerifyExpression(config.DoubleListValue != null);
            testing.VerifyExpression(config.DoubleValue != null);
            testing.VerifyExpression(config.BoolValue != null);
            testing.VerifyExpression(config.DateTimeValue != null);
            testing.VerifyExpression(config.TimespanValue != null);
            Log("Completed testing of LoadConfig");
        }
    }
}