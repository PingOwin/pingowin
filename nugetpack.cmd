SET version=%1
nuget restore ./source/PingOwin.sln
"C:\Program Files (x86)\MSBuild\14.0\Bin\MsBuild.exe" ./source/PingOwin.sln /p:Configuration=Release
nuget pack ./source/PingOwin.Core.Frontend/PingOwin.Core.Frontend.nuspec -Build -OutputDirectory ./packed/ -Version %version%
