# Changelog

Todos los cambios importantes de este proyecto serán documentados en este archivo.

El formato está basado en [Keep a Changelog](https://keepachangelog.com/es-ES/1.0.0/),
y este proyecto se adhiere a [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [1.1.0] - 2025-01-31

### Añadido
- Nueva estructura JSON que genera arrays limpios separados por hoja de Excel
- Clase helper `ExcelDeserializer` para facilitar la deserialización del JSON
- Modelo de ejemplo `FicheroCarga` con atributos `JsonProperty` para manejar nombres de columnas con espacios
- Métodos genéricos para deserializar cualquier tipo de hoja
- Método `GetSheetNames()` para obtener los nombres de todas las hojas disponibles
- Documentación completa en español
- Ejemplos de uso con datos reales de sistemas ERP

### Cambiado
- Estructura de salida JSON: de objetos DataTable complejos a arrays simples de objetos
- Descripción del paquete actualizada para reflejar compatibilidad específica con .NET Framework 4.5
- README mejorado con ejemplos prácticos y casos de uso reales

### Corregido
- Manejo de headers vacíos: ahora genera nombres de columna por defecto (Column1, Column2, etc.)
- Referencias de ensamblado en proyectos de ejemplo
- Problemas de dependencias con EPPlus en proyectos .NET Framework 4.5

## [1.0.0] - 2025-01-30

### Añadido
- Release inicial de la librería ExcelToJsonConverter
- Conversión básica de archivos Excel (.xlsx, .xls) a JSON
- Soporte para .NET Framework 4.5
- Clase `ExcelConverter` con métodos síncronos
- Clase `ExcelConverterOptions` para configuración avanzada
- Opciones para:
  - Incluir/excluir headers
  - Omitir filas vacías
  - Seleccionar hojas específicas
  - Mapeo personalizado de columnas
  - Definir rangos de celdas específicos
- Integración con ASP.NET MVC
- Soporte para objetos `Stream` y `HttpPostedFileBase`
- Proyecto de ejemplo con .NET Framework 4.5 (FrameworkApiExampleV2)
- Proyecto de ejemplo con .NET 9.0 (ExcelApiExample)
- Licencia MIT
- Dependencias:
  - EPPlus 4.5.3.3 (LGPL v3)
  - Newtonsoft.Json 13.0.3 (MIT)

### Notas técnicas
- Compatible con .NET Framework 4.5
- Generación automática de paquete NuGet en build
- Ejemplos funcionales para ambos frameworks
- Documentación básica de instalación y uso