nuget restore ./source/PingOwin.sln
C:\Windows\Microsoft.NET\Framework64\v4.0.30319\msbuild.exe ./source/PingOwin.sln /p:Configuration=Release
nuget pack ./source/PingOwin.Core.Frontend/PingOwin.Core.Frontend.nuspec -Build -OutputDirectory ./packed/ -Prop Configuration=Release
