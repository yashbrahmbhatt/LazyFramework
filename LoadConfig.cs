using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using LazyFramework.DX.Shared;
using UiPath.CodedWorkflows;
using UiPath.Excel;
using UiPath.Platform.ResourceHandling;

namespace LazyFramework.DX.Shared
{
    public class LoadConfig : CodedWorkflow
    {
        [Workflow]
        public Dictionary<string, object> Execute(string file, List<string> ignored)
        {
            Log($"Loading config from path '{file}' and ignoring sheets '{ignored}'");
            var dict = new Dictionary<string, object>();
            file = file.ToLower();
            if (file.EndsWith(".xlsx") || file.EndsWith(".xls"))
            {
                var configDataSet = workflows.ReadExcelFile(file);
                foreach (DataTable table in configDataSet.Tables)
                {
                    if (ignored.Contains(table.TableName))
                    {
                        Log($"Ignoring sheet {table.TableName}");
                        continue;
                    }
                    Log($"Loading sheet {table.TableName}");

                    foreach (DataRow row in table.Rows)
                    {
                        var name = row["Name"].ToString();
                        if (string.IsNullOrEmpty(name)) continue;
                        switch (table.TableName)
                        {
                            case "Assets":
                                var assetValue = SharedHelpers.Retry<object>(() => system.GetAsset(row["Value"].ToString()));
                                dict[name] = assetValue;
                                break;
                            case "Files":
                                var type = row["Type"].ToString();
                                var folder = row["Folder"].ToString();
                                var path = row["Path"].ToString();
                                var bucket = row["Bucket"].ToString();
                                switch (type.ToLower().Trim())
                                {
                                    case "excel":
                                        var filePath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".xlsx");
                                        // Storage
                                        if (!string.IsNullOrEmpty(folder)) SharedHelpers.Retry<ILocalResource>(() => system.DownloadStorageFile(path, bucket, folder, filePath, 60));
                                        // Local
                                        else filePath = path;
                                        dict[name] = SharedHelpers.Retry<DataSet>(() => workflows.ReadExcelFile(filePath));
                                        break;
                                    case "text":
                                        // Local File
                                        if (string.IsNullOrEmpty(folder)) dict[name] = SharedHelpers.Retry<string>(() => File.ReadAllText(path));
                                        // Storage Bucket File
                                        else dict[name] = SharedHelpers.Retry<string>(() => system.ReadStorageText(path, bucket, folder));
                                        break;
                                    default:
                                        throw new ArgumentException("File type is not supported");
                                }
                                break;
                            default:
                                dict[name] = row["Value"].ToString();
                                break;
                        }
                    }
                }
            }
            else
            {
                throw new NotImplementedException("Files other than excel files are not currently supported for Configs");
            }

            return dict;
        }

    }
}