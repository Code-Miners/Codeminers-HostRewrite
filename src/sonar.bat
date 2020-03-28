SET Scanner="SonarScanner.MSBuild.exe"

SET EntMSBuild="C:\Program Files (x86)\Microsoft Visual Studio\2017\Enterprise\MSBuild\15.0\Bin\MSBuild.exe"
SET ProMSBuild="C:\Program Files (x86)\Microsoft Visual Studio\2017\Professional\MSBuild\15.0\Bin\MSBuild.exe"

IF EXIST %ProMSBuild% goto protool
IF EXIST %EntMSBuild% goto enttool

goto errormessage

:protool
SET MSBuildTool=%ProMSBuild%
goto runmain

:enttool
SET MSBuildTool=%EntMSBuild%
goto runmain

:errormessage
echo MSBuild not found
goto scriptend

:runmain

%Scanner% begin /key:"Core.Modules.Web.ForwardedHostRewriteModule" /name:"Codeminers Forwarded Host Rewrite" /v:"1.0"

%MSBuildTool% /t:Rebuild

%Scanner% end

:scriptend
