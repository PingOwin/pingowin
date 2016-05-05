SET apikey=%1
SET version=%2
nuget push ./packed/PingOwin.%version%.nupkg %apikey%
