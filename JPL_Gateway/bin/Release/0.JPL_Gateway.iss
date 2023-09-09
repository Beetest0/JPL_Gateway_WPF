; -- Example1.iss --
; Demonstrates copying 3 files and creating an icon.

; SEE THE DOCUMENTATION FOR DETAILS ON CREATING .ISS SCRIPT FILES!

[Setup]
AppName=JPL_Gateway_TEST
AppVersion=1.0.2
DefaultDirName={pf}\JPL_Gateway_TEST
DisableDirPage=Yes
DefaultGroupName=JPL_Gateway_TEST
UninstallDisplayIcon={app}\JPL_Gateway.exe
Compression=lzma2
OutputDir=userdocs:Inno Setup Examples Output
OutputBaseFilename=JPL_Gateway_TEST_SETUP
AppID=JPL_Gateway_TEST
PrivilegesRequired=admin
LicenseFile=license.txt
SetupIconFile=JPL_icon.ico

[Files]
Source: "en\*"; DestDir: "{app}\en";
Source: "kr\*"; DestDir: "{app}\kr";
Source: "JPL_Gateway.exe"; DestDir: "{app}";
Source: "JPL_icon.ico"; DestDir: "{app}";
Source: "AxInterop.WMPLib.dll"; DestDir: "{app}";
Source: "BTDDFUSoftlink.exe"; DestDir: "{app}";
Source: "BTUpdaterSoftlink.exe"; DestDir: "{app}";
Source: "CMVC.sys"; DestDir: "{app}";
Source: "CMVC64.sys"; DestDir: "{app}";
Source: "CMVC65XX.sys"; DestDir: "{app}";
Source: "CMVC65XX_64.sys"; DestDir: "{app}";
Source: "EASendMail.dll"; DestDir: "{app}";
Source: "engineframeworkcpp.dll"; DestDir: "{app}";
Source: "FreeMateUsb.dll"; DestDir: "{app}";
Source: "hidapi.dll"; DestDir: "{app}";
Source: "HidDfu.dll"; DestDir: "{app}";
Source: "HidLibrary.dll"; DestDir: "{app}";
Source: "Interop.WMPLib.dll"; DestDir: "{app}";
Source: "Microsoft.Expression.Interactions.dll"; DestDir: "{app}";
Source: "msvcp140d.dll"; DestDir: "{app}";
Source: "NAudio.dll"; DestDir: "{app}";
Source: "Newtonsoft.Json.dll"; DestDir: "{app}";
Source: "PresentationCore.dll"; DestDir: "{app}";
Source: "RestSharp.dll"; DestDir: "{app}";
Source: "SoftlinkDownloader.exe"; DestDir: "{app}";
Source: "SoftlinkDownloaderXX.exe"; DestDir: "{app}";
Source: "Speaker_test.wav"; DestDir: "{app}";
Source: "SuperSocket.ClientEngine.dll"; DestDir: "{app}";
Source: "ucrtbased.dll"; DestDir: "{app}";
Source: "vcruntime140d.dll"; DestDir: "{app}";
Source: "WebSocket4Net.dll"; DestDir: "{app}";


[Icons]
Name: "{commonprograms}\JPL_Gateway_TEST"; Filename: "{app}\JPL_Gateway.exe"
Name: "{commondesktop}\JPL_Gateway_TEST"; Filename: "{app}\JPL_Gateway.exe"

[UninstallRun]
Filename: {sys}\taskkill.exe; Parameters: "/f /im JPL_Gateway.exe"; Flags: skipifdoesntexist runhidden

[Registry]
Root: HKCU; Subkey: "SOFTWARE\SoftLink_Jabber"; ValueType: string; ValueName: ""; ValueData: ""
Root: HKCU; Subkey: "SOFTWARE\SoftLink_Jabber"; ValueType: string; ValueName: "clientId"; ValueData: ""
Root: HKCU; Subkey: "SOFTWARE\SoftLink_Jabber"; ValueType: string; ValueName: "callstatus"; ValueData: "idle"
Root: HKCU; Subkey: "SOFTWARE\SoftLink_Jabber"; ValueType: string; ValueName: "ismute"; ValueData: "unumte"
Root: HKCU; Subkey: "SOFTWARE\SoftLink_Jabber"; ValueType: string; ValueName: "custom"; ValueData: "softlink"
Root: HKCU; Subkey: "SOFTWARE\SoftLink_Jabber"; ValueType: string; ValueName: "lync"; ValueData: "0"


[Code]
/////////////////////////////////////////////////////////////////////
function GetUninstallString(): String;
var
  sUnInstPath: String;
  sUnInstallString: String;
  sXXXString : String;
begin

  sUnInstPath := ExpandConstant('Software\Microsoft\Windows\CurrentVersion\Uninstall\{#emit SetupSetting("AppId")}_is1');
  sUnInstallString := '';
  if not RegQueryStringValue(HKLM, sUnInstPath, 'UninstallString', sUnInstallString) then
    RegQueryStringValue(HKCU, sUnInstPath, 'UninstallString', sUnInstallString);
  Result := sUnInstallString;
end;

/////////////////////////////////////////////////////////////////////
function IsUpgrade(): Boolean;
begin
  Result := (GetUninstallString() <> '');
end;


/////////////////////////////////////////////////////////////////////
function UnInstallOldVersion(): Integer;
var
  sUnInstallString: String;
  iResultCode: Integer;
begin
// Return Values:
// 1 - uninstall string is empty
// 2 - error executing the UnInstallString
// 3 - successfully executed the UnInstallString

  // default return value
  Result := 0;

  // get the uninstall string of the old app
  sUnInstallString := GetUninstallString();
  if sUnInstallString <> '' then begin
    sUnInstallString := RemoveQuotes(sUnInstallString);
    if Exec(sUnInstallString, '/SILENT /NORESTART /SUPPRESSMSGBOXES','', SW_HIDE, ewWaitUntilTerminated, iResultCode) then
      Result := 3
    else
      Result := 2;
  end else
    Result := 1;
end;

/////////////////////////////////////////////////////////////////////
procedure CurStepChanged(CurStep: TSetupStep);
begin
  if (CurStep=ssInstall) then
  begin
    if (IsUpgrade()) then
    begin
      UnInstallOldVersion();
    end;
  end;
end;