SET version=%1
SET nuget="tools\nuget\nuget.exe"
SET projBaseDir=./source/PingOwin.Core.Frontend/
SET projPath=%projBaseDir%PingOwin.Core.Frontend.csproj
SET packagesPath=%projBaseDir%packages.config
SET buildOutput="../../build"

rm -R source/packages/

%nuget% restore source/PingOwin.sln 
"C:\Program Files (x86)\MSBuild\14.0\Bin\MsBuild.exe" %projPath% /p:Configuration=Release /p:OutputPath=%buildOutput%

call Powershell.exe -executionpolicy remotesigned -File .\ilmerge.ps1

%nuget% pack ./source/PingOwin.Core.Frontend/PingOwin.Core.Frontend.nuspec -BasePath ./distribution/ -OutputDirectory ./packed/ -Version %version%
