# ExcelToJsonConverter

[![NuGet version](https://img.shields.io/nuget/v/ExcelToJsonConverter.svg)](https://www.nuget.org/packages/ExcelToJsonConverter/)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![.NET Framework](https://img.shields.io/badge/.NET%20Framework-4.5-blue.svg)](https://dotnet.microsoft.com/en-us/download/dotnet-framework/net45)

Una librería .NET que convierte archivos Excel a JSON estructurado, diseñada específicamente para aplicaciones empresariales que trabajan con .NET Framework 4.5.

## Descripción

Esta librería nació de la necesidad de procesar archivos Excel complejos en aplicaciones empresariales. A diferencia de otras soluciones que generan JSON anidado difícil de manejar, ExcelToJsonConverter produce arrays JSON limpios donde cada hoja del Excel se convierte en un array independiente.

## Características principales

- **Estructura JSON limpia**: Cada hoja de Excel genera un array JSON separado
- **Compatible con .NET Framework 4.5**: Pensado para aplicaciones legacy empresariales
- **Integración ASP.NET MVC**: Funciona directamente con HttpPostedFileBase
- **Manejo de nombres con espacios**: Soporte completo para columnas como "Fecha de Albarán" o "Nº Pedido"
- **Configuración flexible**: Control sobre qué hojas procesar, rangos de celdas, y mapeo de columnas
- **Uso comercial permitido**: Licencia MIT completamente libre

## Instalación

### Desde Visual Studio (Recomendado)

1. **Clic derecho** en tu proyecto → **Administrar paquetes NuGet**
2. Ir a la pestaña **Examinar**
3. Buscar **"ExcelToJsonConverter"**
4. Hacer clic en **Instalar**

### Desde Package Manager Console

```powershell
Install-Package ExcelToJsonConverter
```

### Desde .NET CLI

```bash
dotnet add package ExcelToJsonConverter
```

### Instalación manual (si compilas desde código fuente)

1. **Compilar la librería:**
   ```bash
   cd ExcelToJsonConverter/src/ExcelToJsonConverter
   dotnet build --configuration Release
   ```

2. **Opción A - Usar la DLL directamente:**
   - Copiar `ExcelToJsonConverter.dll` desde `bin/Release/net45/`
   - Agregar referencia en tu proyecto: Clic derecho → **Agregar referencia** → **Examinar**

3. **Opción B - Instalar el paquete local:**
   - El archivo .nupkg se genera en `bin/Release/`
   - En Visual Studio: **Herramientas** → **Opciones** → **Administrador de paquetes NuGet** → **Orígenes de paquetes**
   - Agregar una fuente local apuntando a la carpeta `bin/Release/`

## Estructura del JSON generado

La librería convierte este tipo de Excel:

**Hoja "Clientes":**
| Nombre | Edad | Ciudad |
|--------|------|--------|
| Juan Pérez | 30 | Madrid |
| María García | 25 | Barcelona |

**Hoja "Pedidos":**
| Producto | Precio | Stock |
|----------|--------|-------|
| Laptop | 899.99 | 15 |
| Ratón | 25.50 | 100 |

En este JSON:

```json
{
  "Clientes": [
    {"Nombre": "Juan Pérez", "Edad": 30, "Ciudad": "Madrid"},
    {"Nombre": "María García", "Edad": 25, "Ciudad": "Barcelona"}
  ],
  "Pedidos": [
    {"Producto": "Laptop", "Precio": 899.99, "Stock": 15},
    {"Producto": "Ratón", "Precio": 25.50, "Stock": 100}
  ]
}
```

## Uso básico

### Conversión simple

```csharp
using ExcelToJsonConverter;

// Desde archivo
string json = ExcelConverter.ConvertToJson(@"C:\datos\archivo.xlsx");

// Desde stream (útil para archivos subidos)
using (FileStream stream = new FileStream(@"C:\datos\archivo.xlsx", FileMode.Open))
{
    string json = ExcelConverter.ConvertToJson(stream);
}
```

### Con opciones avanzadas

```csharp
var options = new ExcelConverterOptions
{
    IncludeHeaders = true,
    SkipEmptyRows = true,
    IncludedSheets = new[] { "Clientes", "Pedidos" },
    ColumnMapping = new Dictionary<string, string>
    {
        { "Nombre Completo", "NombreCompleto" },
        { "Fecha Nacimiento", "FechaNacimiento" }
    }
};

string json = ExcelConverter.ConvertToJson(@"C:\datos\archivo.xlsx", options);
```

## Trabajando con datos reales

Para archivos Excel con nombres de columnas complejos (muy común en entornos empresariales):

```csharp
// Datos típicos de sistemas ERP con espacios y caracteres especiales
public class DatosERP
{
    [JsonProperty("Fecha de Albarán")]
    public DateTime FechaAlbaran { get; set; }

    [JsonProperty("Organización ventas")]
    public string OrganizacionVentas { get; set; }

    [JsonProperty("Nombre Solicitante")]
    public string NombreSolicitante { get; set; }

    [JsonProperty("Cantidad de pedido")]
    public double CantidadPedido { get; set; }

    [JsonProperty("Nº Pedido de Venta")]
    public string NumeroPedidoVenta { get; set; }

    [JsonProperty("Importe del Transporte")]
    public double ImporteTransporte { get; set; }
}
```

### Deserialización con la clase helper

```csharp
// Obtener los datos como objetos tipados
var datos = ExcelDeserializer.DeserializeSheet<DatosERP>(jsonResult, "Datos ERP");

// Procesar los datos
foreach(var item in datos)
{
    Console.WriteLine($"Cliente: {item.NombreSolicitante}");
    Console.WriteLine($"Pedido: {item.NumeroPedidoVenta}");
    Console.WriteLine($"Importe: {item.ImporteTransporte:C}");
}
```

## Integración en ASP.NET MVC

```csharp
public class UploadController : Controller
{
    [HttpPost]
    public ActionResult ProcesarExcel(HttpPostedFileBase archivo)
    {
        if (archivo != null && archivo.ContentLength > 0)
        {
            try
            {
                // Convertir a JSON
                string json = ExcelConverter.ConvertToJson(archivo.InputStream);
                
                // Obtener datos tipados
                var datosERP = ExcelDeserializer.DeserializeSheet<DatosERP>(json, "Datos ERP");
                
                // Procesar los datos según la lógica de negocio
                ProcesarDatosEmpresariales(datosERP);
                
                ViewBag.Mensaje = $"Procesados {datosERP.Count} registros correctamente";
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Error procesando el archivo: {ex.Message}";
            }
        }
        
        return View();
    }
}
```

## Estructura del proyecto

```
ExcelToJsonConverter/
├── ExcelToJsonConverter/              # Librería principal
│   ├── ExcelConverter.cs              # Clase principal de conversión
│   ├── ExcelConverterOptions.cs       # Opciones de configuración
│   └── Extensions/                    # Utilidades adicionales
├── FrameworkApiExampleV2/             # Ejemplo con .NET Framework 4.5
│   └── Models/
│       ├── FicheroCarga.cs            # Modelo de datos real
│       └── ExcelDeserializer.cs       # Helper para deserialización
└── README.md
```

## Comandos para desarrollo

### Compilar la librería
```bash
cd ExcelToJsonConverter/src/ExcelToJsonConverter
dotnet build --configuration Release
```

### Ejecutar los ejemplos
```bash
# Ejemplo legacy (.NET Framework 4.5)
# Requiere Visual Studio o IIS Express
```

### Generar paquete NuGet
```bash
dotnet pack ExcelToJsonConverter/src/ExcelToJsonConverter/ExcelToJsonConverter.csproj
```

## Licencias y uso comercial

**Puedes usar esta librería en proyectos comerciales sin restricciones.**

### Dependencias y sus licencias:
- **ExcelToJsonConverter**: MIT (libre para uso comercial)
- **Newtonsoft.Json**: MIT (libre para uso comercial)
- **EPPlus 4.5.3.3**: LGPL v3 (permitido para uso comercial vía NuGet)

La combinación de estas licencias permite el uso comercial completo. EPPlus 4.5.3.3 es la última versión con licencia LGPL, las versiones posteriores requieren licencia comercial.

## Changelog

### Versión 1.1.0 (2025-01-31)
- Nueva estructura JSON con arrays separados por hoja
- Clases helper para facilitar la deserialización
- Modelo de ejemplo para datos con nombres de columnas complejos
- Documentación mejorada con ejemplos reales
- Corrección en el manejo de headers vacíos

### Versión 1.0.0 (2025-01-30)
- Release inicial
- Conversión básica de Excel a JSON
- Soporte para .NET Framework 4.5
- Integración con ASP.NET MVC

## Contribuir

Si encuentras algún problema o tienes sugerencias:

1. Revisa los issues existentes en GitHub
2. Crea un nuevo issue describiendo el problema
3. Si tienes una solución, envía un Pull Request

## Autor

Desarrollado por Javier Ludeña para resolver necesidades reales de procesamiento de Excel en .net framework 4.5.