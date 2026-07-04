# Instalador de IngSoftware_90DI — Paso a paso

Genera `IngSoftware_90DI.exe`, que instala la app y prepara la base de datos en cualquier
máquina Windows con SQL Server (Express) instalado.

## Requisitos para ARMAR el instalador (tu máquina de dev)
- .NET 8 SDK
- [Inno Setup](https://jrsoftware.org/isdl.php)
- SSMS (para exportar el `.bacpac`)

## ⚠️ Requisitos OBLIGATORIOS en la máquina DESTINO

La app es un **cliente**: necesita un SQL Server con la base. **Si estos requisitos no están,
la app se instala y muestra el login, pero NO permite iniciar sesión** (no hay base a la cual
conectarse). Instalar ESTO en la máquina destino **antes** de correr `IngSoftware_90DI.exe`:

| Requisito | Para qué | Cómo obtenerlo |
|---|---|---|
| **SQL Server Express** | Motor de base de datos (obligatorio) | https://www.microsoft.com/sql-server/sql-server-downloads → *Express* |
| **sqlcmd** | El instalador consulta/recalcula la base | Viene con SSMS o con *SQL Server Command Line Utilities* |
| **SqlPackage** | El instalador importa el `.bacpac` (crea la base) | `dotnet tool install --global microsoft.sqlpackage` |

> **Nota de arquitectura (Opción 1 — prerrequisito):** el instalador **prepara la base**
> (crea tablas, SP y datos desde el `.bacpac`), pero **no instala el motor SQL**. Se asume
> SQL Server Express como prerrequisito de la máquina destino. Es un modelo de despliegue
> estándar y válido para un sistema cliente-servidor.

### Cómo verificar los prerrequisitos en la máquina destino
```powershell
Get-Service 'MSSQL$SQLEXPRESS'   # el motor debe existir y estar Running
sqlcmd -?                        # debe existir el comando
sqlpackage /version              # debe existir el comando
```
Si los tres responden, el instalador va a crear la base y la app **va a loguear**.

---

## Paso 1 — Publicar la app (self-contained)
Desde la raíz del repo (`IngSoftware90DI/`):

```bash
dotnet publish "IngSoftware90DI/UI_90DI.csproj" -c Release -r win-x64 --self-contained true -o Installer/publish
```

Queda todo en `Installer/publish/` (incluye el runtime .NET, así el destino no necesita instalarlo).

## Paso 2 — Exportar la base a un .bacpac
En SSMS, conectado a tu base **ya INTEGRO**:
1. Clic derecho en `IngSoftware90DI` → **Tasks → Export Data-tier Application…**
2. Guardar como `Installer/IngSoftware90DI.bacpac`.

El `.bacpac` lleva esquema + datos + los **SP optimizados** + datos semilla (admin, patentes incl. `BACKUP_BD`).

## Paso 3 — Compilar el instalador
1. Abrir `Installer/IngSoftware_90DI.iss` en **Inno Setup Compiler**.
2. Verificar que en `Installer/` estén: `publish\`, `IngSoftware90DI.bacpac`, `provision-db.ps1`.
3. Menú **Build → Compile**.
4. Se genera `Installer/Output/IngSoftware_90DI.exe`.

## Paso 4 — Probar el instalador (entorno limpio)
Idealmente en otra VM Windows (o usuario nuevo) con SQL Express + sqlcmd + SqlPackage:
1. Ejecutar `IngSoftware_90DI.exe` (como admin).
2. En el asistente, ingresar el servidor SQL (ej. `.\SQLEXPRESS`).
3. Terminar la instalación → el instalador:
   - copia la app,
   - escribe `dal.settings.json` (config de la capa DAL) con el servidor elegido,
   - importa el `.bacpac` (crea la base) y recalcula integridad.
4. Abrir IngSoftware_90DI → login admin → verificar menú, y probar **Admin → Backup BD**.

## Qué hace cada archivo
| Archivo | Rol |
|---|---|
| `IngSoftware_90DI.iss` | Script de Inno Setup: copia, pide servidor, escribe `dal.settings.json`, dispara el aprovisionamiento |
| `provision-db.ps1` | Importa el `.bacpac` si la base no existe + `sp_RecalcularTodo_90DI` |
| `IngSoftware90DI.bacpac` | Base inicial (lo generás vos en el paso 2) |
| `publish/` | App publicada (lo generás vos en el paso 1) |

