# ============================================================================
#  Aprovisiona la base IngSoftware90DI en la máquina destino.
#  - Si la base NO existe: la crea importando el .bacpac (esquema + datos + SP).
#  - Siempre recalcula los dígitos verificadores como red de seguridad.
#  Lo invoca el instalador (IngSoftware_90DI.iss) en el paso post-install.
#
#  SILENCIOSO: corre en ventana OCULTA, sin mensajes ni pausas (no muestra nada
#  al usuario). Todo el detalle queda en un LOG por si hay que diagnosticar.
#  Sale con código 0 si todo salió bien, o 1 si hubo un error → el instalador
#  decide qué mostrar.
#
#  AUTOSUFICIENTE: usa el SqlPackage EMPAQUETADO junto a la app (..\SqlPackage\)
#  y consulta SQL con .NET (System.Data.SqlClient, incluido en Windows PowerShell),
#  así NO depende de que estén instalados sqlcmd ni SqlPackage en la máquina.
#  Único prerrequisito: SQL Server (y una login con permisos para crear la base).
# ============================================================================
param(
    [string]$Server = ".\SQLEXPRESS"
)

$ErrorActionPreference = "Stop"

$db     = "IngSoftware90DI"
$bacpac = Join-Path $PSScriptRoot "IngSoftware90DI.bacpac"
$log    = Join-Path $env:TEMP "IngSoftware_90DI_provision.log"

# SqlPackage empaquetado en ..\SqlPackage\ (junto a la app). Fallback: el del PATH.
$sqlPackage = Join-Path (Split-Path $PSScriptRoot -Parent) "SqlPackage\sqlpackage.exe"
if (-not (Test-Path $sqlPackage)) {
    $enPath = Get-Command sqlpackage -ErrorAction SilentlyContinue
    if ($enPath) { $sqlPackage = $enPath.Source }
}

Start-Transcript -Path $log -Force | Out-Null

# Ejecuta SQL con .NET (sin sqlcmd). Devuelve el escalar si $scalar, o $null.
function Invoke-Sql([string]$sql, [string]$database = "master", [bool]$scalar = $false) {
    $cs  = "Server=$Server;Database=$database;Integrated Security=True;TrustServerCertificate=True;"
    $con = New-Object System.Data.SqlClient.SqlConnection $cs
    try {
        $con.Open()
        $cmd = $con.CreateCommand()
        $cmd.CommandText = $sql
        $cmd.CommandTimeout = 0
        if ($scalar) { return $cmd.ExecuteScalar() }
        $cmd.ExecuteNonQuery() | Out-Null
        return $null
    }
    finally { $con.Close() }
}

try {
    Write-Output "== Aprovisionamiento de base ($db en $Server) =="

    if (-not (Test-Path $sqlPackage)) {
        throw "No se encontró SqlPackage (ni empaquetado en ..\SqlPackage\ ni en el PATH)."
    }

    # ¿Existe la base? (con .NET, sin sqlcmd)
    $existe = Invoke-Sql "SELECT COUNT(*) FROM sys.databases WHERE name = '$db'" "master" $true

    if ([int]$existe -eq 0) {
        Write-Output "La base no existe. Importando desde el .bacpac ..."
        & $sqlPackage /Action:Import /SourceFile:"$bacpac" /TargetServerName:"$Server" /TargetDatabaseName:"$db" /TargetTrustServerCertificate:True
        if ($LASTEXITCODE -ne 0) { throw "Falló el import del .bacpac (código $LASTEXITCODE)." }
        Write-Output "Base creada correctamente."
    }
    else {
        Write-Output "La base '$db' ya existe. Se omite el import."
    }

    # Red de seguridad: recalcular dígitos verificadores (con .NET, sin sqlcmd)
    try {
        Invoke-Sql "EXEC sp_RecalcularTodo_90DI;" $db | Out-Null
    }
    catch {
        Write-Output "ADVERTENCIA: no se pudo recalcular integridad: $($_.Exception.Message)"
    }

    Write-Output "== Listo =="
    Stop-Transcript | Out-Null
    exit 0
}
catch {
    Write-Output "ERROR: $($_.Exception.Message)"
    Stop-Transcript | Out-Null
    exit 1
}
