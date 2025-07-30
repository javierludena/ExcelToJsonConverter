# Configuración Git para ExcelToJsonConverter

## Pasos para subir el proyecto a GitHub

### 1. Inicializar el repositorio local
```bash
cd "C:\Users\javier.ludena\Desktop\libreria-nuget-excel-json"
git init
```

### 2. Añadir todos los archivos
```bash
git add .
```

### 3. Crear el primer commit
```bash
git commit -m "feat: ExcelToJsonConverter v1.1.0 - Librería para convertir Excel a JSON con arrays estructurados

- Conversión de Excel a JSON con arrays limpios por hoja
- Soporte completo para .NET Framework 4.5
- Clases helper para deserialización fácil
- Ejemplos funcionales para ASP.NET MVC
- Documentación completa en español
- Licencia MIT para uso comercial"
```

### 4. Crear repositorio en GitHub
1. Ve a https://github.com y crea un nuevo repositorio
2. Nómbralo: `ExcelToJsonConverter`
3. Descripción: "Librería .NET para convertir Excel a JSON estructurado - Compatible con .NET Framework 4.5"
4. Márcalo como **Público**
5. **NO** inicialices con README (ya tienes uno)

### 5. Conectar con el repositorio remoto
```bash
# Cambiar 'tu-usuario' por tu nombre de usuario de GitHub
git remote add origin https://github.com/tu-usuario/ExcelToJsonConverter.git
git branch -M main
```

### 6. Subir al repositorio
```bash
git push -u origin main
```

### 7. Crear tag para la versión
```bash
git tag -a v1.1.0 -m "v1.1.0 - Arrays JSON estructurados por hoja

Mejoras principales:
- Nueva estructura JSON con arrays separados por hoja
- Clases helper ExcelDeserializer y FicheroCarga
- Soporte mejorado para nombres con espacios y caracteres especiales
- Documentación completa en español
- Ejemplos prácticos con datos reales"

git push origin v1.1.0
```

### 8. Crear release en GitHub (opcional)
1. Ve a tu repositorio en GitHub
2. Haz clic en "Releases" → "Create a new release"
3. Selecciona el tag v1.1.0
4. Título: "ExcelToJsonConverter v1.1.0"
5. Descripción: Copia el contenido del CHANGELOG para v1.1.0
6. Adjunta el archivo .nupkg si quieres
7. Haz clic en "Publish release"

## Comandos útiles para mantenimiento

### Actualizar después de cambios
```bash
git add .
git commit -m "feat: descripción del cambio"
git push
```

### Crear nueva versión
```bash
# Actualizar versión en .csproj primero
git add .
git commit -m "bump: versión 1.2.0"
git tag -a v1.2.0 -m "v1.2.0 - Descripción de cambios"
git push origin main
git push origin v1.2.0
```

### Ver estado del repositorio
```bash
git status
git log --oneline -10
```

## Estructura final del repositorio

```
ExcelToJsonConverter/
├── .gitignore                         # Archivos a ignorar
├── README.md                          # Documentación principal
├── CHANGELOG.md                       # Historial de cambios
├── CLAUDE.md                          # Instrucciones para Claude Code
├── GIT_SETUP.md                       # Este archivo
├── ExcelToJsonConverter/              # Librería principal
├── ExcelApiExample/                   # Ejemplo .NET 9.0
├── FrameworkApiExampleV2/             # Ejemplo .NET Framework 4.5
└── BBDD carga toneladas ERP Ventas.XLSX  # (ignorado por .gitignore)
```

## URL del repositorio
Una vez creado, tu repositorio estará disponible en:
https://github.com/tu-usuario/ExcelToJsonConverter

## Instrucciones de instalación para usuarios
Los usuarios podrán instalar la librería con:
```bash
Install-Package ExcelToJsonConverter
```

O clonar el repositorio para contribuir:
```bash
git clone https://github.com/tu-usuario/ExcelToJsonConverter.git
```