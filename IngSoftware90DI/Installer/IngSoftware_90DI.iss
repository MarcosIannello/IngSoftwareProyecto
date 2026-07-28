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
; Instalación POR USUARIO: no requiere administrador (para máquinas de facultad sin admin).
; Con "lowest", los constantes {autopf}, {autodesktop}, {autoappdata}, {group} resuelven
; automáticamente a ubicaciones del usuario (no Program Files / no ProgramData).
PrivilegesRequired=lowest
WizardStyle=modern
; Ícono del propio instalador (.exe de setup). Ruta relativa a este .iss.
SetupIconFile=..\IngSoftware90DI\Resources\favicon.ico
; Ícono que se muestra en "Agregar o quitar programas"
UninstallDisplayIcon={app}\{#ExeName}

[Files]
; Aplicación publicada (self-contained: incluye el runtime .NET)
Source: "publish\*"; DestDir: "{app}"; Flags: recursesubdirs createallsubdirs ignoreversion
; Base de datos inicial (esquema + datos + SP) y script de aprovisionamiento
Source: "IngSoftware90DI.bacpac"; DestDir: "{app}\db"; Flags: ignoreversion
Source: "provision-db.ps1";       DestDir: "{app}\db"; Flags: ignoreversion
; SqlPackage EMPAQUETADO (para no depender de que esté instalado en la máquina).
; Requiere que en Installer\SqlPackage\ estén los archivos del SqlPackage standalone.
Source: "SqlPackage\*"; DestDir: "{app}\SqlPackage"; Flags: recursesubdirs createallsubdirs ignoreversion

[Dirs]
Name: "{autoappdata}\IngSoftware_90DI\Backups"

[Icons]
Name: "{group}\{#AppName}";     Filename: "{app}\{#ExeName}"; IconFilename: "{app}\{#ExeName}"
Name: "{autodesktop}\{#AppName}"; Filename: "{app}\{#ExeName}"; IconFilename: "{app}\{#ExeName}"

[Run]
Filename: "{app}\{#ExeName}"; Description: "Ejecutar {#AppName}"; Flags: nowait postinstall skipifsilent

[Code]
const
  RUTA_INSTANCIAS = 'SOFTWARE\Microsoft\Microsoft SQL Server\Instance Names\SQL';

var
  ServerPage: TWizardPage;
  ServerCombo: TNewComboBox;
  DirPage: TWizardPage;
  DirEdit: TNewEdit;
  DirBrowseBtn: TNewButton;
  SelectedDir: string;

// Agrega val al array solo si no está ya (case-insensitive).
procedure AgregarUnico(var arr: TArrayOfString; const val: string);
var
  i, n: Integer;
begin
  for i := 0 to GetArrayLength(arr) - 1 do
    if CompareText(arr[i], val) = 0 then
      Exit;
  n := GetArrayLength(arr);
  SetArrayLength(arr, n + 1);
  arr[n] := val;
end;

// Lee las instancias SQL registradas en una vista del registro (64 o 32 bits).
// El nombre de cada VALOR bajo Instance Names\SQL es el nombre de la instancia:
//   MSSQLSERVER -> instancia por defecto (se conecta como ".")
//   <otro>      -> instancia con nombre  (se conecta como ".\<otro>")
procedure LeerVistaInstancias(RootKey: Integer; var arr: TArrayOfString);
var
  Names: TArrayOfString;
  i: Integer;
  inst: string;
begin
  if RegGetValueNames(RootKey, RUTA_INSTANCIAS, Names) then
  begin
    for i := 0 to GetArrayLength(Names) - 1 do
    begin
      inst := Names[i];
      if CompareText(inst, 'MSSQLSERVER') = 0 then
        AgregarUnico(arr, '.')
      else
        AgregarUnico(arr, '.\' + inst);
    end;
  end;
end;

// Devuelve todas las instancias SQL LOCALES detectadas en la máquina.
// Lee la vista de 64 bits (donde SQL suele registrar) y la de 32 bits por las dudas.
function EnumerarInstanciasSql(): TArrayOfString;
var
  arr: TArrayOfString;
begin
  SetArrayLength(arr, 0);
  LeerVistaInstancias(HKLM64, arr);
  LeerVistaInstancias(HKLM32, arr);
  Result := arr;
end;

// El servidor que el usuario tiene seleccionado/escrito en el combo (con fallback).
function ServidorElegido(): string;
begin
  Result := Trim(ServerCombo.Text);
  if Result = '' then
    Result := '.\SQLEXPRESS';
end;

// Evento: click en "Examinar...". BrowseForFolder es una función nativa de
// Inno Setup (abre el diálogo de carpeta de Windows); el tercer parámetro
// habilita el botón "Crear nueva carpeta".
procedure DirBrowseBtnClick(Sender: TObject);
var
  Dir: string;
begin
  Dir := DirEdit.Text;
  if BrowseForFolder('Seleccionar carpeta de instalación:', Dir, True) then
    DirEdit.Text := Dir;
end;

procedure InitializeWizard;
var
  instancias: TArrayOfString;
  i: Integer;
  lbl, lblDir: TNewStaticText;
begin
  // ===== PÁGINA 1: Selección de carpeta de instalación =====
  DirPage := CreateCustomPage(wpWelcome,
    'Carpeta de instalación', 'Dónde deseas instalar IngSoftware_90DI');

  lblDir := TNewStaticText.Create(DirPage);
  lblDir.Parent := DirPage.Surface;
  lblDir.Left := 0;
  lblDir.Top := ScaleY(8);
  lblDir.AutoSize := True;
  lblDir.Caption := 'Carpeta destino:';

  DirEdit := TNewEdit.Create(DirPage);
  DirEdit.Parent := DirPage.Surface;
  DirEdit.Left := 0;
  DirEdit.Top := lblDir.Top + lblDir.Height + ScaleY(6);
  DirEdit.Width := DirPage.SurfaceWidth - ScaleX(80);
  DirEdit.Text := ExpandConstant('{autopf}\IngSoftware_90DI');

  DirBrowseBtn := TNewButton.Create(DirPage);
  DirBrowseBtn.Parent := DirPage.Surface;
  DirBrowseBtn.Left := DirEdit.Left + DirEdit.Width + ScaleX(8);
  DirBrowseBtn.Top := DirEdit.Top;
  DirBrowseBtn.Width := ScaleX(70);
  DirBrowseBtn.Height := DirEdit.Height;
  DirBrowseBtn.Caption := 'Examinar...';
  DirBrowseBtn.OnClick := @DirBrowseBtnClick;

  // ===== PÁGINA 2: Base de datos (instancia SQL) =====
  ServerPage := CreateCustomPage(DirPage.ID,
    'Base de datos', 'Instancia de SQL Server');

  lbl := TNewStaticText.Create(ServerPage);
  lbl.Parent := ServerPage.Surface;
  lbl.Left := 0;
  lbl.Top := ScaleY(8);
  lbl.AutoSize := True;
  lbl.Caption := 'Instancia SQL:';

  ServerCombo := TNewComboBox.Create(ServerPage);
  ServerCombo.Parent := ServerPage.Surface;
  ServerCombo.Left := 0;
  ServerCombo.Top := lbl.Top + lbl.Height + ScaleY(6);
  ServerCombo.Width := ServerPage.SurfaceWidth;
  ServerCombo.Style := csDropDown; // editable: permite elegir o tipear

  instancias := EnumerarInstanciasSql();
  for i := 0 to GetArrayLength(instancias) - 1 do
    ServerCombo.Items.Add(instancias[i]);

  // Preselección: primera instancia detectada; si no hubo ninguna, el default clásico.
  if ServerCombo.Items.Count > 0 then
    ServerCombo.ItemIndex := 0
  else
    ServerCombo.Text := '.\SQLEXPRESS';
end;

procedure EscribirAppSettings;
var
  Cfg, Srv: string;
begin
  Srv := ServidorElegido();
  StringChangeEx(Srv, '\', '\\', True);
  Cfg :=
    '{'#13#10 +
    '  "ConnectionStrings": {'#13#10 +
    '    "IngSoftware90DI": "Server=' + Srv + ';Database=IngSoftware90DI;Trusted_Connection=True;TrustServerCertificate=True;"'#13#10 +
    '  }'#13#10 +
    '}';
  SaveStringToFile(ExpandConstant('{app}\dal.settings.json'), Cfg, False);
end;

// La página built-in de destino (wpSelectDir) queda reemplazada por DirPage.
function ShouldSkipPage(PageID: Integer): Boolean;
begin
  Result := (PageID = wpSelectDir);
end;

function NextButtonClick(CurPageID: Integer): Boolean;
begin
  Result := True;

  if CurPageID = DirPage.ID then
  begin
    SelectedDir := Trim(DirEdit.Text);

    if SelectedDir = '' then
    begin
      MsgBox('Indicá una carpeta de instalación.', mbError, MB_OK);
      Result := False;
      Exit;
    end;

    // Ruta absoluta con unidad (C:\...); evita rutas relativas o inválidas.
    if (Length(SelectedDir) < 3) or (SelectedDir[2] <> ':') or (SelectedDir[3] <> '\') then
    begin
      MsgBox('La ruta debe ser absoluta, por ejemplo C:\IngSoftware_90DI', mbError, MB_OK);
      Result := False;
      Exit;
    end;

    // {app} se resuelve desde WizardForm.DirEdit: hay que volcarlo ACÁ,
    // antes de instalar (en ssInstall ya sería tarde y se ignoraría).
    WizardForm.DirEdit.Text := SelectedDir;
  end;
end;

procedure CurStepChanged(CurStep: TSetupStep);
var
  ResultCode: Integer;
begin
  if CurStep = ssPostInstall then
  begin
    EscribirAppSettings;
    // Provisión de la base OCULTA y silenciosa: ventana escondida (SW_HIDE +
    // -WindowStyle Hidden), sin mensajes ni pausas. El detalle queda en el log.
    if (not Exec('powershell.exe',
          '-ExecutionPolicy Bypass -NoProfile -WindowStyle Hidden -File "' + ExpandConstant('{app}\db\provision-db.ps1') +
          '" -Server "' + ServidorElegido() + '"',
          ExpandConstant('{app}\db'),
          SW_HIDE, ewWaitUntilTerminated, ResultCode))
       or (ResultCode <> 0) then
    begin
      // Solo ante un fallo real avisamos (una línea), con la ruta del log.
      MsgBox('No se pudo preparar la base de datos en la instancia seleccionada.'#13#10 +
             'Detalle: ' + ExpandConstant('{%TEMP}') + '\IngSoftware_90DI_provision.log',
             mbError, MB_OK);
    end;
  end;
end;