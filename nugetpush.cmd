SET version=%1
SET nuget="tools\nuget\nuget.exe"

%nuget% push ./packed/PingOwin.%version%.nupkg 
