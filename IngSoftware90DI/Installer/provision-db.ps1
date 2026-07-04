# ============================================================================
#  Aprovisiona la base IngSoftware90DI en la máquina destino.
#  - Si la base NO existe: la crea importando el .bacpac (esquema + datos + SP).
#  - Siempre recalcula los dígitos verificadores como red de seguridad.
#  Lo invoca el instalador (IngSoftware_90DI.iss) en el paso post-install.
#
#  Requisitos en la máquina destino:
#    - SQL Server (Express) instalado y accesible.
#    - sqlcmd y SqlPackage en el PATH.
#        sqlcmd  -> viene con las herramientas de SQL Server / "SQL Server Command Line Utilities".
#        SqlPackage -> dotnet tool install --global microsoft.sqlpackage
# ============================================================================
param(
    [string]$Server = ".\SQLEXPRESS"
)

$ErrorActionPreference = "Stop"
$db     = "IngSoftware90DI"
$bacpac = Join-Path $PSScriptRoot "IngSoftware90DI.bacpac"

Write-Host "== Aprovisionamiento de base de datos ($db en $Server) =="

# ¿Existe la base?
$exists = (sqlcmd -S $Server -E -h -1 -W -Q "SET NOCOUNT ON; SELECT COUNT(*) FROM sys.databases WHERE name='$db'").Trim()

if ($exists -eq '0') {
    Write-Host "La base no existe. Importando desde $bacpac ..."
    sqlpackage /Action:Import `
               /SourceFile:"$bacpac" `
               /TargetServerName:"$Server" `
               /TargetDatabaseName:"$db" `
               /TargetTrustServerCertificate:True
    Write-Host "Base creada."
} else {
    Write-Host "La base '$db' ya existe. Se omite el import."
}

# Red de seguridad: dejar los dígitos verificadores consistentes
Write-Host "Recalculando integridad..."
sqlcmd -S $Server -E -d $db -Q "EXEC sp_RecalcularTodo_90DI;"

Write-Host "== Listo =="
