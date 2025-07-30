# ExcelToJsonConverter

[![NuGet version](https://img.shields.io/nuget/v/ExcelToJsonConverter.svg)](https://www.nuget.org/packages/ExcelToJsonConverter/)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![.NET Framework](https://img.shields.io/badge/.NET%20Framework-4.5-blue.svg)](https://dotnet.microsoft.com/en-us/download/dotnet-framework/net45)

Convert Excel files to **clean JSON arrays** in .NET Framework 4.5 applications with ASP.NET MVC support. Each Excel sheet becomes a separate JSON array with typed objects.

## Description

A professional .NET library to seamlessly convert Excel files (.xlsx, .xls) to JSON strings. Built for performance and ease of use, it supports both .NET Framework 4.0 and 4.8, with special features for ASP.NET MVC applications.

## ✨ Features

-   **🎯 Clean JSON Structure**: Each Excel sheet becomes a separate JSON array
-   **🚀 Framework Compatibility**: Targets .NET Framework 4.5
-   **⚡ ASP.NET MVC Ready**: Directly handles `HttpPostedFileBase` and `Stream` objects
-   **📋 Multiple Input Types**: Accepts file paths, `Stream` objects, and uploaded files
-   **🔧 Rich Configuration**:
    -   Select specific sheets to convert
    -   Include or exclude header rows
    -   Skip empty rows automatically
    -   Custom column mapping
    -   Define specific cell ranges
-   **🛡️ Robust Error Handling**: Custom exceptions for clear error diagnostics
-   **📦 Easy Deserialization**: Helper classes for converting JSON to typed objects
-   **✅ Commercial Use**: MIT License - free for commercial projects

## Installation

Install the package via NuGet Package Manager:

```shell
Install-Package ExcelToJsonConverter
```

Or via the .NET CLI:

```shell
dotnet add package ExcelToJsonConverter
```

## Usage

### Basic Conversion

Convert an entire Excel file to a JSON string.

```csharp
using ExcelToJsonConverter;

// From a file path
string json = ExcelConverter.ConvertToJson("C:\path\to\your\file.xlsx");

// From a Stream
using (FileStream stream = new FileStream("C:\path\to\your\file.xlsx", FileMode.Open))
{
    string json = ExcelConverter.ConvertToJson(stream);
}
```

### Advanced Conversion with Options

Customize the conversion process using `ExcelConverterOptions`.

```csharp
var options = new ExcelConverterOptions
{
    IncludeHeaders = true,
    SkipEmptyRows = true,
    IncludedSheets = new[] { "Sheet1", "Sheet3" },
    ColumnMapping = new Dictionary<string, string>
    {
        { "First Name", "firstName" },
        { "Last Name", "lastName" }
    },
    CellRange = "A1:C10" // Process only a specific range
};

string json = ExcelConverter.ConvertToJson("C:\path\to\your\file.xlsx", options);
```

### Asynchronous Conversion (.NET 4.8 only)

For non-blocking operations in modern applications.

```csharp
public async Task<string> ProcessExcelFileAsync(Stream stream)
{
    return await ExcelConverter.ConvertToJsonAsync(stream);
}
```

### ASP.NET MVC Example

Easily handle file uploads in your controllers.

```csharp
using System.Web.Mvc;
using System.Web;
using ExcelToJsonConverter;

public class UploadController : Controller
{
    [HttpPost]
    public ActionResult Upload(HttpPostedFileBase file)
    {
        if (file != null && file.ContentLength > 0)
        {
            try
            {
                // To use IFormFile, you need a wrapper or an extension method
                // For this example, we'll use the stream directly
                string json = ExcelConverter.ConvertToJson(file.InputStream);

                // Or, if you have an IFormFile wrapper
                // string json = ExcelConverter.ConvertToJson(new FormFile(file));

                ViewBag.Json = json;
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error: " + ex.Message;
            }
        }
        else
        {
            ViewBag.Error = "Please select a file.";
        }

        return View("Index");
    }
}
```

## GitHub Repository Setup

To contribute or set up your own version of this repository:

```bash
# Initialize repository
git init
git add .
git commit -m "feat: Initial commit - Excel to JSON converter for .NET Framework 4.0/4.8"

# Connect with GitHub
git remote add origin https://github.com/tu-usuario/ExcelToJsonConverter.git
git branch -M main
git push -u origin main

# Create tag for release
git tag -a v1.0.0 -m "v1.0.0 - First stable release"
git push origin v1.0.0
```

## License

This project is licensed under the [MIT License](LICENSE).
