# Instalador de IngSoftware_90DI

Genera `IngSoftware_90DI.exe`, que instala la aplicación y crea su base de datos en
cualquier PC con Windows y SQL Server. No requiere permisos de administrador.

## Qué hace falta en la PC donde se instala

Solo tener **SQL Server** (Express o superior) instalado y en funcionamiento.

Al instalar, el asistente **detecta solo** las instancias de SQL de la máquina y las ofrece
en una lista para elegir. Si no aparece la que se quiere usar, se puede escribir a mano.
El resto (crear la base, configurar la conexión) lo hace el instalador automáticamente.

## Cómo armar el instalador

1. **Publicar la app.** Desde la carpeta del repo:

   ```bash
   dotnet publish "IngSoftware90DI/UI_90DI.csproj" -c Release -r win-x64 --self-contained true -o Installer/publish
   ```

2. **Exportar la base.** En SSMS, sobre la base `IngSoftware90DI`:
   clic derecho → *Tasks → Export Data-tier Application…* y guardar como
   `Installer/IngSoftware90DI.bacpac`.

3. **Compilar.** Abrir `Installer/IngSoftware_90DI.iss` en Inno Setup y hacer *Build → Compile*.
   Se genera `Installer/Output/IngSoftware_90DI.exe`.

## Cómo probarlo

Ejecutar `IngSoftware_90DI.exe`, elegir la instancia de SQL y terminar la instalación.
Después abrir la app e ingresar con el usuario admin para verificar que todo funcione.
