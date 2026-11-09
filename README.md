# Laboratorio 7 - CRUD de Personas

Aplicación de escritorio WPF con arquitectura por capas, ADO.NET y SQL Server.

## Requisitos
- Windows 10/11
- Visual Studio 2022 con la carga de trabajo **Desarrollo de escritorio de .NET**
- .NET 8 SDK
- SQL Server LocalDB (incluido normalmente con Visual Studio) o SQL Server Express

## Capas
- **Presentacion:** interfaz WPF y eventos de usuario.
- **Negocio:** validaciones y reglas de negocio.
- **Datos:** acceso SQL mediante ADO.NET (`Microsoft.Data.SqlClient`).
- **Entidades:** clase `Persona` compartida entre capas.

## Base de datos
1. Abre SQL Server Management Studio o el explorador de objetos de Visual Studio.
2. Ejecuta todo el contenido de `Database/script.sql`.
3. Si tu instancia no es LocalDB, modifica la cadena de conexión en `Datos/Conexion.cs`.

La tabla contiene cinco columnas: `IdPersona`, `NombreCompleto`, `DNI`, `Telefono` y `Activo`. La columna `Activo` implementa eliminación lógica. El listado y el buscador muestran únicamente registros activos.

## Ejecutar
Abre `Lab7Persona.sln` en Visual Studio 2022, restaura NuGet y establece `Presentacion` como proyecto de inicio. Ejecuta con F5.

## Funcionalidades
- Registrar personas.
- Listar personas activas.
- Buscar por nombre completo o DNI.
- Seleccionar un registro y actualizarlo.
- Eliminar lógicamente (actualiza `Activo = 0`, no borra físicamente).

## Evidencias
La carpeta `Evidencias` contiene referencias visuales de la interfaz diseñada. Para obtener capturas reales de ejecución, inicia la aplicación en Windows después de configurar SQL Server.
