# Instalador de IngSoftware_90DI — Paso a paso

Genera `IngSoftware_90DI.exe`, que instala la app y prepara la base de datos en cualquier
máquina Windows con SQL Server. El instalador es **autosuficiente** (lleva su propio
SqlPackage) y **por-usuario** (no requiere permisos de administrador).

## Requisitos para ARMAR el instalador (tu máquina de dev)
- .NET 8 SDK
- [Inno Setup](https://jrsoftware.org/isdl.php)
- SSMS (para exportar el `.bacpac`)
- **SqlPackage standalone** (versión **.NET Framework**), descomprimido en `Installer\SqlPackage\`.
  Se empaqueta dentro del instalador. Descarga: https://learn.microsoft.com/sql/tools/sqlpackage/sqlpackage-download

## Requisitos en la máquina DESTINO

**El único prerrequisito es un motor SQL Server.** El instalador es autosuficiente: lleva su
propio SqlPackage y consulta SQL con .NET, por lo que **NO requiere sqlcmd, SqlPackage ni
permisos de administrador**.

| Requisito | Detalle |
|---|---|
| **SQL Server Express** (o superior) | Motor de base de datos al que se conecta la app |
| **Login con permisos** | El usuario de Windows debe tener una login en ese SQL con derecho a crear la base. En un SQL Express local que instalaste vos, ya sos sysadmin. |

> ✅ **No requiere administrador de Windows.** La instalación es por-usuario (se instala en la
> carpeta del usuario, sin UAC).

### Cómo averiguar el nombre de la instancia SQL (para el asistente)

> ⚠️ Ejecutar en **PowerShell**, no en CMD.

```powershell
Get-Service | Where-Object { $_.Name -like 'MSSQL*' } | Select-Object Name, Status
```

| Servicio que aparece | Instancia | Qué escribir en el asistente |
|---|---|---|
| `MSSQL$SQLEXPRESS` | SQLEXPRESS | `.\SQLEXPRESS` |
| `MSSQLSERVER` | (por defecto) | `.` o `localhost` |
| `MSSQL$OTRONOMBRE` | OTRONOMBRE | `.\OTRONOMBRE` |

> El asistente propone `.\SQLEXPRESS` por defecto. Si la instancia se llama distinto, escribí
> el nombre correcto o no conectará.

---

## Paso 1 — Publicar la app (self-contained)
Desde la raíz del repo (`IngSoftware90DI/`):

```bash
dotnet publish "IngSoftware90DI/UI_90DI.csproj" -c Release -r win-x64 --self-contained true -o Installer/publish
```

## Paso 2 — Exportar la base a un .bacpac
En SSMS, conectado a la base **ya INTEGRO**:
1. Clic derecho en `IngSoftware90DI` → **Tasks → Export Data-tier Application…**
2. Guardar como `Installer/IngSoftware90DI.bacpac`.

El `.bacpac` lleva esquema + datos + los **SP optimizados** + datos semilla (admin, patentes incl. `BACKUP_BD`).

## Paso 3 — Colocar SqlPackage
Descomprimir el **SqlPackage standalone (.NET Framework)** en `Installer\SqlPackage\`,
de modo que quede `Installer\SqlPackage\sqlpackage.exe` (+ sus DLLs).

## Paso 4 — Compilar el instalador
1. Abrir `Installer/IngSoftware_90DI.iss` en **Inno Setup Compiler**.
2. Verificar que en `Installer/` estén: `publish\`, `IngSoftware90DI.bacpac`, `provision-db.ps1`, `SqlPackage\`.
3. Menú **Build → Compile**.
4. Se genera `Installer/Output/IngSoftware_90DI.exe`.

## Paso 5 — Probar el instalador
Idealmente en una VM/usuario **sin admin** con solo SQL Server instalado:
1. Ejecutar `IngSoftware_90DI.exe` (no pide administrador).
2. Ingresar el servidor SQL detectado (ej. `.\SQLEXPRESS`).
3. Terminar la instalación → el instalador:
   - copia la app,
   - escribe `dal.settings.json` (config de la capa DAL) con el servidor elegido,
   - crea la base con el **SqlPackage empaquetado** y recalcula integridad.
4. Abrir la app → login admin → verificar menú, y probar **Admin → Backup BD**.

## Qué hace cada archivo
| Archivo | Rol |
|---|---|
| `IngSoftware_90DI.iss` | Script de Inno Setup: copia, pide servidor, escribe `dal.settings.json`, dispara el aprovisionamiento |
| `provision-db.ps1` | Crea la base con el SqlPackage empaquetado (si no existe) + `sp_RecalcularTodo_90DI`. Usa .NET (no depende de sqlcmd). |
| `IngSoftware90DI.bacpac` | Base inicial (esquema + SP + datos); se genera en el paso 2 |
| `SqlPackage\` | SqlPackage empaquetado (se coloca en el paso 3) |
| `publish/` | App publicada (se genera en el paso 1) |
