; ============================================================================
;  Instalador de IngSoftware_90DI (Ingeniería de Software - UAI)
;  Compilar con Inno Setup Compiler (https://jrsoftware.org/isdl.php)
;
;  Antes de compilar, en esta carpeta (Installer\) tienen que estar:
;    - publish\                     (salida de dotnet publish, ver README)
;    - IngSoftware90DI.bacpac       (export de la base, ver README)
;    - provision-db.ps1             (incluido en el repo)
; ============================================================================
#define AppName      "IngSoftware_90DI"
#define AppVersion   "1.0"
#define Publisher    "UAI - Ingenieria de Software"
#define ExeName      "UI_90DI.exe"

[Setup]
AppName={#AppName}
AppVersion={#AppVersion}
AppPublisher={#Publisher}
DefaultDirName={autopf}\IngSoftware_90DI
DefaultGroupName={#AppName}
OutputBaseFilename=IngSoftware_90DI
Compression=lzma2
SolidCompression=yes
ArchitecturesInstallIn64BitMode=x64compatible
PrivilegesRequired=admin
WizardStyle=modern

[Files]
; Aplicación publicada (self-contained: incluye el runtime .NET)
Source: "publish\*"; DestDir: "{app}"; Flags: recursesubdirs createallsubdirs ignoreversion
; Base de datos inicial (esquema + datos + SP) y script de aprovisionamiento
Source: "IngSoftware90DI.bacpac"; DestDir: "{app}\db"; Flags: ignoreversion
Source: "provision-db.ps1";       DestDir: "{app}\db"; Flags: ignoreversion

[Dirs]
Name: "{commonappdata}\IngSoftware_90DI\Backups"

[Icons]
Name: "{group}\{#AppName}";         Filename: "{app}\{#ExeName}"
Name: "{commondesktop}\{#AppName}"; Filename: "{app}\{#ExeName}"

[Run]
Filename: "{app}\{#ExeName}"; Description: "Ejecutar {#AppName}"; Flags: nowait postinstall skipifsilent

[Code]
var
  ServerPage: TInputQueryWizardPage;

procedure InitializeWizard;
begin
  ServerPage := CreateInputQueryPage(wpSelectDir,
    'Base de datos', 'Servidor SQL Server',
    'Ingrese el nombre/instancia del SQL Server donde se creará la base de datos.');
  ServerPage.Add('Servidor (ej: .\SQLEXPRESS):', False);
  ServerPage.Values[0] := '.\SQLEXPRESS';
end;

procedure EscribirAppSettings;
var
  Cfg, Srv: string;
begin
  Srv := ServerPage.Values[0];
  StringChangeEx(Srv, '\', '\\', True);
  Cfg :=
    '{'#13#10 +
    '  "ConnectionStrings": {'#13#10 +
    '    "IngSoftware90DI": "Server=' + Srv + ';Database=IngSoftware90DI;Trusted_Connection=True;TrustServerCertificate=True;"'#13#10 +
    '  }'#13#10 +
    '}';
  SaveStringToFile(ExpandConstant('{app}\dal.settings.json'), Cfg, False);
end;

procedure CurStepChanged(CurStep: TSetupStep);
var
  ResultCode: Integer;
begin
  if CurStep = ssPostInstall then
  begin
    EscribirAppSettings;
    Exec('powershell.exe',
      '-ExecutionPolicy Bypass -NoProfile -File "' + ExpandConstant('{app}\db\provision-db.ps1') +
      '" -Server "' + ServerPage.Values[0] + '"',
      ExpandConstant('{app}\db'),
      SW_SHOW, ewWaitUntilTerminated, ResultCode);
  end;
end;