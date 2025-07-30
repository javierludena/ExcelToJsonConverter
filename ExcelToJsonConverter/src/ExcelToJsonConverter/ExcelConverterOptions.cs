using System.Collections.Generic;

namespace ExcelToJsonConverter
{
    public class ExcelConverterOptions
    {
        public string[] IncludedSheets { get; set; }
        public bool IncludeHeaders { get; set; } = true;
        public bool SkipEmptyRows { get; set; } = true;
        public Dictionary<string, string> ColumnMapping { get; set; }
        public string CellRange { get; set; }
    }
}
