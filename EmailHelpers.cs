using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
#nullable enable
namespace LazyFramework.DX.Shared
{
    public static class EmailHelpers
    {
        public static string DataTableToHTML(DataTable? input)
        {
            if(input == null) throw new ArgumentNullException(nameof(input));
            var sb = new StringBuilder();
            sb.Append("<table><tr>");
            foreach (DataColumn col in input.Columns) sb.Append("<th>" + col.ColumnName + "</th>");
            sb.Append("</tr>");
            foreach (DataRow row in input.Rows)
            {
                sb.Append("<tr>");
                foreach (DataColumn col in input.Columns) sb.Append("<td>" + row[col].ToString() + "</td>");
                sb.Append("</tr>");
            }
            sb.Append("</table");
            return sb.ToString();
        }
        public static (string body, string subject) UpdateEmailTemplate(string? body, string? subject, Dictionary<string, object> templateData)
        {
            if(body == null) throw new ArgumentNullException(nameof(body));
            if(subject == null) throw new ArgumentNullException(nameof(subject));
            foreach (var key in templateData.Keys)
            {
                if (templateData[key] != null && templateData[key].GetType() == typeof(DataTable))
                {
                    templateData[key] = DataTableToHTML(templateData[key] as DataTable);
                }
                body.Replace($"{{key}}", templateData[key]?.ToString() ?? string.Empty);
                subject.Replace($"{{key}}", templateData[key]?.ToString() ?? string.Empty);
            }
            return (body, subject);
        }
    }

}