using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
#if NET48
using System.Threading.Tasks;
#endif

namespace ExcelToJsonConverter
{
    public static class ExcelConverter
    {
        public static string ConvertToJson(string filePath)
        {
            return ConvertToJson(filePath, new ExcelConverterOptions());
        }

        public static string ConvertToJson(Stream stream)
        {
            return ConvertToJson(stream, new ExcelConverterOptions());
        }

        public static string ConvertToJson(string filePath, ExcelConverterOptions options)
        {
            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                return ConvertToJson(stream, options);
            }
        }

        public static string ConvertToJson(Stream stream, ExcelConverterOptions options)
        {
            using (var package = new ExcelPackage(stream))
            {
                var workbook = package.Workbook;
                var result = new Dictionary<string, object>();

                var sheetsToProcess = options.IncludedSheets != null && options.IncludedSheets.Any()
                    ? workbook.Worksheets.Where(ws => options.IncludedSheets.Contains(ws.Name))
                    : workbook.Worksheets;

                foreach (var worksheet in sheetsToProcess)
                {
                    var range = string.IsNullOrEmpty(options.CellRange) ? worksheet.Dimension : worksheet.Cells[options.CellRange];

                    if (range == null) continue;

                    var headers = new List<string>();
                    var startRow = range.Start.Row;

                    // Get headers
                    if (options.IncludeHeaders)
                    {
                        for (var col = range.Start.Column; col <= range.End.Column; col++)
                        {
                            var header = worksheet.Cells[range.Start.Row, col].Text;
                            var columnName = options.ColumnMapping != null && options.ColumnMapping.ContainsKey(header)
                                ? options.ColumnMapping[header]
                                : (!string.IsNullOrEmpty(header) ? header : $"Column{col}");
                            headers.Add(columnName);
                        }
                        startRow++;
                    }
                    else
                    {
                        // Generate default column names
                        for (var col = range.Start.Column; col <= range.End.Column; col++)
                        {
                            headers.Add($"Column{col}");
                        }
                    }

                    // Convert data to array of objects
                    var sheetData = new List<Dictionary<string, object>>();
                    
                    for (var rowNum = startRow; rowNum <= range.End.Row; rowNum++)
                    {
                        var wsRow = worksheet.Cells[rowNum, range.Start.Column, rowNum, range.End.Column];
                        if (options.SkipEmptyRows && wsRow.All(c => c.Value == null || string.IsNullOrWhiteSpace(c.Text)))
                        {
                            continue;
                        }

                        var rowObject = new Dictionary<string, object>();
                        for (var col = range.Start.Column; col <= range.End.Column; col++)
                        {
                            var headerIndex = col - range.Start.Column;
                            var cellValue = worksheet.Cells[rowNum, col].Value;
                            rowObject[headers[headerIndex]] = cellValue;
                        }
                        sheetData.Add(rowObject);
                    }

                    result.Add(worksheet.Name, sheetData);
                }

                return JsonConvert.SerializeObject(result, Formatting.Indented);
            }
        }

#if NET48
        public static async Task<string> ConvertToJsonAsync(Stream stream)
        {
            return await ConvertToJsonAsync(stream, new ExcelConverterOptions());
        }

        public static async Task<string> ConvertToJsonAsync(Stream stream, ExcelConverterOptions options)
        {
            using (var package = new ExcelPackage(stream))
            {
                return await Task.Run(() =>
                {
                    var workbook = package.Workbook;
                    var result = new Dictionary<string, object>();

                    var sheetsToProcess = options.IncludedSheets != null && options.IncludedSheets.Any()
                        ? workbook.Worksheets.Where(ws => options.IncludedSheets.Contains(ws.Name))
                        : workbook.Worksheets;

                    foreach (var worksheet in sheetsToProcess)
                    {
                        var range = string.IsNullOrEmpty(options.CellRange) ? worksheet.Dimension : worksheet.Cells[options.CellRange];

                        if (range == null) continue;

                        var headers = new List<string>();
                        var startRow = range.Start.Row;

                        // Get headers
                        if (options.IncludeHeaders)
                        {
                            for (var col = range.Start.Column; col <= range.End.Column; col++)
                            {
                                var header = worksheet.Cells[range.Start.Row, col].Text;
                                var columnName = options.ColumnMapping != null && options.ColumnMapping.ContainsKey(header)
                                    ? options.ColumnMapping[header]
                                    : (!string.IsNullOrEmpty(header) ? header : $"Column{col}");
                                headers.Add(columnName);
                            }
                            startRow++;
                        }
                        else
                        {
                            // Generate default column names
                            for (var col = range.Start.Column; col <= range.End.Column; col++)
                            {
                                headers.Add($"Column{col}");
                            }
                        }

                        // Convert data to array of objects
                        var sheetData = new List<Dictionary<string, object>>();
                        
                        for (var rowNum = startRow; rowNum <= range.End.Row; rowNum++)
                        {
                            var wsRow = worksheet.Cells[rowNum, range.Start.Column, rowNum, range.End.Column];
                            if (options.SkipEmptyRows && wsRow.All(c => c.Value == null || string.IsNullOrWhiteSpace(c.Text)))
                            {
                                continue;
                            }

                            var rowObject = new Dictionary<string, object>();
                            for (var col = range.Start.Column; col <= range.End.Column; col++)
                            {
                                var headerIndex = col - range.Start.Column;
                                var cellValue = worksheet.Cells[rowNum, col].Value;
                                rowObject[headers[headerIndex]] = cellValue;
                            }
                            sheetData.Add(rowObject);
                        }

                        result.Add(worksheet.Name, sheetData);
                    }

                    return JsonConvert.SerializeObject(result, Formatting.Indented);
                });
            }
        }
#endif
    }
}
