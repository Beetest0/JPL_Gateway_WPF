; version
; 1. For the versioning to work correctly, vbuild must constantly increase for every release.
; 2. vbuild starts out at 1000 and will increment every build for the life of the program.
; 3. For example, it would go v1.5.1001, then v1.6.1002, then v1.7.1004, etc.
; 4. This behaviour can be altered to suit other versioning schemes by changing the functions below.
#define vmajor 1
#define vminor 0
#define vbuild 4

; select application executable
#define exename "JPL_Gateway.exe"
#define icon "JPL_Gateway"

; automatically generates publisher
#define brandname "JPL"
;#define appname "JPL_Gateway"
#define appname "JPL_Gateway_TEST"
#define url "https://www.jpltele.com/"

[Setup]
AppID={code:GetAppID|''}
UsePreviousLanguage=no
AppName={#appname} {#vmajor}.{#vminor}.{#vbuild}
AppVerName={#appname} {#vmajor}.{#vminor}.{#vbuild}
; ---end autoupdate code---
AppPublisher={#url}
AppPublisherURL={#url}
AppSupportURL={#url}
AppUpdatesURL={#url}
DefaultDirName={pf}\{#appname}
DefaultGroupName={#appname}
AllowNoIcons=yes
OutputDir=.
OutputBaseFilename={#appname}_SETUP_{#vmajor}.{#vminor}.{#vbuild}
Compression=lzma
SolidCompression=yes
VersionInfoVersion={#vmajor}.{#vminor}.{#vbuild}
SetupIconFile=JPL_icon.ico
UninstallDisplayIcon={app}\{#exename}

[Languages]
Name: "eng"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked
Name: "quicklaunchicon"; Description: "{cm:CreateQuickLaunchIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
Source: "{#exename}"; DestDir: "{app}"; Flags: ignoreversion
Source: "de-DE\*"; DestDir: "{app}\de-DE";
Source: "en-GB\*"; DestDir: "{app}\en-GB";
Source: "es-ES\*"; DestDir: "{app}\es-ES";
Source: "fr-FR\*"; DestDir: "{app}\fr-FR";
Source: "JPL_icon.ico"; DestDir: "{app}";
Source: "BTDDFUSoftlink.exe"; DestDir: "{app}";
Source: "BTUpdaterSoftlink.exe"; DestDir: "{app}";
Source: "SoftlinkDownloader.exe"; DestDir: "{app}";
Source: "SoftlinkDownloaderXX.exe"; DestDir: "{app}";
Source: "Speaker_test.wav"; DestDir: "{app}";
Source: "CMVC.sys"; DestDir: "{app}";
Source: "CMVC64.sys"; DestDir: "{app}";
Source: "CMVC65XX.sys"; DestDir: "{app}";
Source: "CMVC65XX_64.sys"; DestDir: "{app}";
Source: "*.dll"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs


; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Registry]
; Root: HKCU; Subkey: "Software\DesignREM\FirmwareShieldGui\Settings"; Flags: deletekey;
; ---start autoupdate code; see http://www.microchipc.com/innosetup/innosetup_auto_versioning_upgrade.php--
Root: HKLM; Subkey: Software\{#appname}; ValueType: string; ValueName: CurrentVersion; ValueData: {code:GetAppCurrentVersion|''}; Flags: uninsdeletekey
; ---end autoupdate code---
Root: HKCU; Subkey: "SOFTWARE\SoftLink_Jabber"; ValueType: string; ValueName: ""; ValueData: ""
Root: HKCU; Subkey: "SOFTWARE\SoftLink_Jabber"; ValueType: string; ValueName: "clientId"; ValueData: ""
Root: HKCU; Subkey: "SOFTWARE\SoftLink_Jabber"; ValueType: string; ValueName: "callstatus"; ValueData: "idle"
Root: HKCU; Subkey: "SOFTWARE\SoftLink_Jabber"; ValueType: string; ValueName: "ismute"; ValueData: "unumte"
Root: HKCU; Subkey: "SOFTWARE\SoftLink_Jabber"; ValueType: string; ValueName: "custom"; ValueData: "softlink"
Root: HKCU; Subkey: "SOFTWARE\SoftLink_Jabber"; ValueType: string; ValueName: "lync"; ValueData: "0"


[INI]
Filename: "{app}\homepage.url"; Section: "InternetShortcut"; Key: "URL"; String: "https://www.jpltele.com/"

[Icons]
Name: "{commonprograms}\{#appname}"; Filename: "{app}\{#exename}"
Name: "{commondesktop}\{#appname}"; Filename: "{app}\{#exename}"
;Name: "{group}\{#icon}"; Filename: "{app}\{#icon}"; WorkingDir: "{app}"; Parameters: "v{#vmajor}.{#vminor}.{#vbuild}";
;Name: "{group}\{cm:UninstallProgram,{#brandname}}"; Filename: "{uninstallexe}"
;Name: "{app}\{cm:UninstallProgram, {#brandname}}"; Filename: "{uninstallexe}"

[Run]
Filename: "{app}\{#exename}"; Description: "{cm:LaunchProgram,{#brandname}}"; Flags: nowait postinstall skipifsilent; Parameters: "v{#vmajor}.{#vminor}.{#vbuild}"

[UninstallDelete]
Type: files; Name: "{app}\homepage.url"

; ---start autoupdate code; see http://www.microchipc.com/innosetup/innosetup_auto_versioning_upgrade.php--
[Code]
function GetAppMajorVersion(param: String): String;
	begin
		Result:='{#vmajor}';
	end;

function GetAppMinorVersion(param: String): String;
	begin
		Result:='{#vminor}';
	end;

function GetAppCurrentVersion(param: String): String;
	begin
		Result:='{#vbuild}';
	end;

function GetAppID(param: String): String;
	begin
		Result := '{#appname}';
	end;

function GetPathInstalled(AppID: String): String;
	var
		PrevPath: String;
	begin
		PrevPath := '';
		if not RegQueryStringValue(HKLM, 'Software\Microsoft\Windows\CurrentVersion\Uninstall\'+AppID+'_is1', 'Inno Setup: App Path', PrevPath) then begin
			RegQueryStringValue(HKCU, 'Software\Microsoft\Windows\CurrentVersion\Uninstall\'+AppID+'_is1', 'Inno Setup: App Path', PrevPath);
		end;
		Result := PrevPath;
	end;

function GetInstalledVersion(): String;
	var
		InstalledVersion: String;
	begin
		InstalledVersion := '';
		RegQueryStringValue(HKLM, 'Software\{#appname}', 'Version', InstalledVersion);
		Result := InstalledVersion;
	end;

function GetInstalledCurrentVersion(): String;
	var
		InstalledCurrentVersion: String;
	begin
		InstalledCurrentVersion := '';
		RegQueryStringValue(HKLM, 'Software\{#appname}', 'CurrentVersion', InstalledCurrentVersion);
		Result := InstalledCurrentVersion;
	end;

function InitializeSetup(): Boolean;
	var
		Response: Integer;
		PrevDir: String;
		InstalledVersion: String;
		InstalledCurrentVersion: String;
		//VersionError: String;
	begin
		Result := true;

		// read the installation folder
		PrevDir := GetPathInstalled(getAppID(''));

		if length(Prevdir) > 0 then begin
			// I found the folder so it's an upgrade.
			
			// compare versions
			InstalledCurrentVersion := GetInstalledCurrentVersion();
			InstalledVersion := GetAppCurrentVersion('');
			if (InstalledCurrentVersion < InstalledVersion) then begin
				Result := True;
			end else if (InstalledCurrentVersion = InstalledVersion) then begin
				Response := MsgBox(
					'It appears that the existing {#appname} installation is already current.' + #13#13 +
					'Do you want to continue with the update installation?', mbError, MB_YESNO
				);
				Result := (Response = IDYES);
			end else begin
				Response := MsgBox(
					'It appears that the existing {#appname} installation is newer than this update.' + #13#13 +
					'The existing installation is v'+ GetAppMajorVersion('') + '.' + GetAppMinorVersion('')+'.'+InstalledCurrentVersion +'.  This update will change the installation to v'+ GetAppMajorVersion('') + '.' + GetAppMinorVersion('')+'.'+ GetAppCurrentVersion('') + '.' + #13#13 +
					'Do you want to continue with the update installation?', mbError, MB_YESNO
				);
				Result := (Response = IDYES);
			end;
		end else begin
			// Didn't find the folder so its a fresh installation.
			Result:=true;
		end;
    end;

function ShouldSkipPage(PageID: Integer): Boolean;
  var
    PrevDir:String;
	begin
		PrevDir := GetPathInstalled(getAppID(''));
		if length(Prevdir) > 0 then begin
		  // skip selectdir if It's an upgrade
		  if (PageID = wpSelectDir) then begin
			 Result := true;
		  end else if (PageID = wpSelectProgramGroup) then begin
			 Result := true;
  		end else if (PageID = wpSelectTasks) then begin
	 		  Result := true;
  		end else begin
  			Result := false;
  		end;
		end;
	end;
