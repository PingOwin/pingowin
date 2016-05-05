[![NuGet](https://img.shields.io/nuget/v/PingOwin.svg?maxAge=2592000)]()

# PingOwin
*Ping - because we ping stuff; Owin because we run in every Owin host.*

## Overview ##
The goal of the project is to have a simple, easy-to-install, easy-to-use ping server. 
It's a light weight, open source, self-hosted alternative to buying a PingDom account.

## Install
```csharp
PM> Install-Package PingOwin
```

## Use
```csharp
public void Configuration(IAppBuilder app)
{
    app.UsePingOwin();
}
```



*By innedag @ [Blank Oslo](http://blankoslo.no/)*
 
