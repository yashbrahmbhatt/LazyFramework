using System.Data;
using LazyFramework.DX.Shared;
using UiPath.CodedWorkflows;
using UiPath.Excel;
using UiPath.Excel.Activities.API;
using UiPath.Excel.Activities.API.Models;

namespace LazyFramework.DX.Shared
{
    public class ReadExcelFile : CodedWorkflow
    {
        [Workflow]
        public DataSet Execute(string file)
        {
            Log($"Reading excel file at '{file}'");
            DataSet ds = new();

            var process = excel.ExcelProcessScope(new ScopeOptions()
            {
                ShowExcelWindow = false,
                DisplayAllerts = false,
                ExistingProcessAction = ExistingExcelProcessAction.ForceKill,
                FileConflictResolution = ExcelFileConflictResolution.None,
                LaunchMethod = ExcelStartMethod.Automation,
                ProcessMode = ExcelProcessMode.ReuseIfExists,
            });
            SharedHelpers.Retry<bool>(() =>
            {
                var workbook = excel.UseExcelFile(new UseOptions()
                {
                    ReadOnly = true,
                    KeepExcelOpen = false,
                    SaveChanges = false,
                    CreateIfNotExist = false,
                    Path = file,
                    ExcelProcess = process
                });

                workbook.ForEachSheet((ISheetRef sheet) =>
                {
                    var dt = sheet.ReadAsDataTable(true, false, false, false, ReadFormattingOptions.RawValue);
                    dt.TableName = sheet.Name;
                    ds.Tables.Add(dt);
                    return true;
                });
            });
            Log($"File read with {ds.Tables.Count.ToString()} tables");
            return ds;
        }
    }
}