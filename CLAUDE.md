# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

This is a .NET library ecosystem for converting Excel files to JSON format, targeting multiple .NET Framework versions. The repository contains:

1. **ExcelToJsonConverter** - The main NuGet library (.NET Framework 4.5)
2. **ExcelApiExample** - Modern .NET 9.0 Web API example using the library
3. **FrameworkApiExampleV2** - Legacy .NET Framework 4.5 ASP.NET MVC Web API example

## Architecture

### Core Library Structure
- **Main Library**: `ExcelToJsonConverter/src/ExcelToJsonConverter/`
  - `ExcelConverter.cs` - Main static class with conversion methods
  - `ExcelConverterOptions.cs` - Configuration options for conversion
  - `Extensions/` - Extension methods and utilities

### Key Dependencies
- **EPPlus**: Excel file processing (version 4.5.3.3 for .NET 4.5, version 6.1.1 for .NET 4.8)
- **Newtonsoft.Json**: JSON serialization (version 11.0.1 for compatibility)

### Example Applications
- **ExcelApiExample**: Modern minimal API with Swagger documentation
- **FrameworkApiExampleV2**: Traditional MVC pattern with full Web API controllers

## Common Development Commands

### Building the Solution
```bash
# Build main library
dotnet build ExcelToJsonConverter/src/ExcelToJsonConverter/ExcelToJsonConverter.csproj

# Build .NET 9.0 API example
dotnet build ExcelApiExample/ExcelApiExample.csproj

# Build entire solution
dotnet build ExcelToJsonConverter.sln
```

### Running Examples
```bash
# Run modern .NET 9.0 API (with Swagger at /swagger)
dotnet run --project ExcelApiExample

# Legacy Framework project requires Visual Studio or MSBuild
# Use IIS Express or Visual Studio to run FrameworkApiExampleV2
```

### Package Management
```bash
# Restore NuGet packages
dotnet restore

# Create NuGet package (automatically done on build due to GeneratePackageOnBuild=true)
dotnet pack ExcelToJsonConverter/src/ExcelToJsonConverter/ExcelToJsonConverter.csproj
```

### Testing
The test project exists but currently only contains a placeholder. Tests would be run with:
```bash
dotnet test ExcelToJsonConverter/tests/ExcelToJsonConverter.Tests/
```

## Target Framework Considerations

- **Library**: Targets .NET Framework 4.5 for maximum compatibility
- **Modern Example**: Uses .NET 9.0 with modern C# features
- **Legacy Example**: Uses .NET Framework 4.5 with traditional ASP.NET MVC

When adding features:
- Maintain backward compatibility for .NET Framework 4.5
- Use conditional compilation (`#if NET48`) for framework-specific features like async/await
- Test compatibility across both modern and legacy example projects

## API Endpoints

### ExcelApiExample (.NET 9.0)
- `POST /api/excel/convert` - Convert uploaded Excel file to JSON

### FrameworkApiExampleV2 (.NET Framework 4.5)
- Uses traditional MVC controller structure
- Upload functionality in `ExcelUploadController.cs`

## File Structure Notes

- Main solution file: `ExcelToJsonConverter.sln`
- Each example has its own solution/project structure
- Package configurations in `nuget.config` files
- Legacy project uses `packages.config` for NuGet packages

## Release History

### v1.1.3 (2025-07-31)
- Added troubleshooting documentation for Newtonsoft.Json binding redirect issues
- Updated example project with Cliente model replacing FicheroCarga
- Enhanced documentation with XML configuration examples for web.config/app.config

### v1.1.1 (2025-07-31)
- Downgraded Newtonsoft.Json from 13.0.3 to 11.0.1 for compatibility with legacy projects
- Maintains all functionality while supporting older dependency versions
- Release files available in `release-files/` directory

### v1.1.0 
- Initial structured library release with array support

## Troubleshooting

### Newtonsoft.Json Binding Redirect Issues

If you encounter binding redirect errors when using the library in consumer projects, the solution is to change the Newtonsoft.Json binding redirect in the consumer project's `web.config` or `app.config` from version 13.0.0.0 to 11.0.0.0:

```xml
<runtime>
  <assemblyBinding xmlns="urn:schemas-microsoft-com:asm.v1">
    <dependentAssembly>
      <assemblyIdentity name="Newtonsoft.Json" publicKeyToken="30ad4fe6b2a6aeed" culture="neutral" />
      <bindingRedirect oldVersion="0.0.0.0-11.0.0.0" newVersion="11.0.0.0" />
    </dependentAssembly>
  </assemblyBinding>
</runtime>
```

This ensures compatibility with the library's Newtonsoft.Json 11.0.1 dependency.

## Git Commit Guidelines

- Do not include Claude Code references in commit messages
- Keep commit messages concise and descriptive
- Use conventional commit format (feat:, fix:, chore:, docs:)