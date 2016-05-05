nuget restore ./source/PingOwin.sln
nuget pack ./source/PingOwin.Core.Frontend/PingOwin.Core.Frontend.nuspec -Build -OutputDirectory ./packed/ -Prop Configuration=Release
