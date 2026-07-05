# ============================================================================
#  Aprovisiona la base IngSoftware90DI en la máquina destino.
#  - Si la base NO existe: la crea importando el .bacpac (esquema + datos + SP).
#  - Siempre recalcula los dígitos verificadores como red de seguridad.
#  Lo invoca el instalador (IngSoftware_90DI.iss) en el paso post-install.
#
#  Requisitos en la máquina destino:
#    - SQL Server (Express) instalado y corriendo.
#    - sqlcmd  -> "SQL Server Command Line Utilities" o SSMS.
#    - SqlPackage -> dotnet tool install --global microsoft.sqlpackage
#                    (o el SqlPackage standalone de Microsoft, sin .NET SDK)
#
#  La ventana NO se cierra sola (espera ENTER) y deja un log para diagnóstico.
# ============================================================================
param(
    [string]$Server = ".\SQLEXPRESS"
)

$db     = "IngSoftware90DI"
$bacpac = Join-Path $PSScriptRoot "IngSoftware90DI.bacpac"
$log    = Join-Path $env:TEMP "IngSoftware_90DI_provision.log"

Start-Transcript -Path $log -Force | Out-Null

function Fin([int]$code) {
    Write-Host ""
    Write-Host "Log guardado en: $log"
    Stop-Transcript | Out-Null
    Write-Host "Presione ENTER para cerrar..." -ForegroundColor Yellow
    Read-Host | Out-Null
    exit $code
}

Write-Host "== Aprovisionamiento de base ($db en $Server) =="

# 1) Verificar prerrequisitos con mensajes claros
if (-not (Get-Command sqlcmd -ErrorAction SilentlyContinue)) {
    Write-Host "ERROR: 'sqlcmd' no esta instalado o no esta en el PATH." -ForegroundColor Red
    Write-Host "       Instale 'SQL Server Command Line Utilities' o SSMS."
    Fin 1
}
if (-not (Get-Command sqlpackage -ErrorAction SilentlyContinue)) {
    Write-Host "ERROR: 'SqlPackage' no esta instalado o no esta en el PATH." -ForegroundColor Red
    Write-Host "       Instale con: dotnet tool install --global microsoft.sqlpackage"
    Write-Host "       (o descargue el SqlPackage standalone de Microsoft)."
    Fin 1
}

# 2) ¿Se puede conectar? ¿Existe la base?
$exists = (sqlcmd -S $Server -E -h -1 -W -Q "SET NOCOUNT ON; SELECT COUNT(*) FROM sys.databases WHERE name='$db'" 2>&1)
if ($LASTEXITCODE -ne 0) {
    Write-Host "ERROR: no se pudo conectar a '$Server'." -ForegroundColor Red
    Write-Host "       Verifique que SQL Server Express este instalado y corriendo."
    Write-Host $exists
    Fin 1
}

if ("$exists".Trim() -eq '0') {
    Write-Host "La base no existe. Importando desde el .bacpac ..."
    sqlpackage /Action:Import /SourceFile:"$bacpac" /TargetServerName:"$Server" /TargetDatabaseName:"$db" /TargetTrustServerCertificate:True
    if ($LASTEXITCODE -ne 0) {
        Write-Host "ERROR: fallo el import del .bacpac." -ForegroundColor Red
        Fin 1
    }
    Write-Host "Base creada correctamente." -ForegroundColor Green
} else {
    Write-Host "La base '$db' ya existe. Se omite el import."
}

# 3) Red de seguridad: recalcular digitos verificadores
Write-Host "Recalculando integridad..."
sqlcmd -S $Server -E -d $db -Q "EXEC sp_RecalcularTodo_90DI;"

Write-Host "== Listo ==" -ForegroundColor Green
Fin 0
