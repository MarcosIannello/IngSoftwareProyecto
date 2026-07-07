# ============================================================================
#  Aprovisiona la base IngSoftware90DI en la máquina destino.
#  - Si la base NO existe: la crea importando el .bacpac (esquema + datos + SP).
#  - Siempre recalcula los dígitos verificadores como red de seguridad.
#  Lo invoca el instalador (IngSoftware_90DI.iss) en el paso post-install.
#
#  AUTOSUFICIENTE: usa el SqlPackage EMPAQUETADO junto a la app (..\SqlPackage\)
#  y consulta SQL con .NET (System.Data.SqlClient, incluido en Windows PowerShell),
#  así NO depende de que estén instalados sqlcmd ni SqlPackage en la máquina.
#  Único prerrequisito: SQL Server (y una login con permisos para crear la base).
#
#  La ventana NO se cierra sola (espera ENTER) y deja un log para diagnóstico.
# ============================================================================
param(
    [string]$Server = ".\SQLEXPRESS"
)

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

function Fin([int]$code) {
    Write-Host ""
    Write-Host "Log guardado en: $log"
    Stop-Transcript | Out-Null
    Write-Host "Presione ENTER para cerrar..." -ForegroundColor Yellow
    Read-Host | Out-Null
    exit $code
}

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

Write-Host "== Aprovisionamiento de base ($db en $Server) =="

# 1) SqlPackage disponible (empaquetado o en PATH)
if (-not (Test-Path $sqlPackage)) {
    Write-Host "ERROR: no se encontró SqlPackage (ni empaquetado en ..\SqlPackage\ ni en el PATH)." -ForegroundColor Red
    Fin 1
}

# 2) ¿Se puede conectar? ¿Existe la base? (con .NET, sin sqlcmd)
try {
    $existe = Invoke-Sql "SELECT COUNT(*) FROM sys.databases WHERE name = '$db'" "master" $true
}
catch {
    Write-Host "ERROR: no se pudo conectar a '$Server'. Verifique que SQL Server esté corriendo" -ForegroundColor Red
    Write-Host "       y que su usuario tenga permisos en esa instancia."
    Write-Host $_.Exception.Message
    Fin 1
}

if ([int]$existe -eq 0) {
    Write-Host "La base no existe. Importando desde el .bacpac ..."
    & $sqlPackage /Action:Import /SourceFile:"$bacpac" /TargetServerName:"$Server" /TargetDatabaseName:"$db" /TargetTrustServerCertificate:True
    if ($LASTEXITCODE -ne 0) {
        Write-Host "ERROR: falló el import del .bacpac." -ForegroundColor Red
        Fin 1
    }
    Write-Host "Base creada correctamente." -ForegroundColor Green
}
else {
    Write-Host "La base '$db' ya existe. Se omite el import."
}

# 3) Red de seguridad: recalcular dígitos verificadores (con .NET, sin sqlcmd)
Write-Host "Recalculando integridad..."
try {
    Invoke-Sql "EXEC sp_RecalcularTodo_90DI;" $db | Out-Null
}
catch {
    Write-Host "ADVERTENCIA: no se pudo recalcular integridad: $($_.Exception.Message)" -ForegroundColor Yellow
}

Write-Host "== Listo ==" -ForegroundColor Green
Fin 0
