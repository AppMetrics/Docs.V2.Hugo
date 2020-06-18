---
title: "ASP.NET Core"
draft: false
weight: 1
icon: "/images/aspnetcore.png"
---

## Overview

App Metrics provides a set of packages designed for ASP.NET Core monitoring. The core packages are as follows:

|Package|Description|
|------|:--------|
|[App.Metrics.AspNetCore.All](https://www.nuget.org/packages/App.Metrics.AspNetCore.All/)|Metapackage containing a full set of AspNetCore AppMetrics features.
|[App.Metrics.AspNetCore.Tracking](https://www.nuget.org/packages/App.Metrics.AspNetCore.Tracking/)|A set of middleware components which automatically track typical metrics used in monitoring a web application.
|[App.Metrics.AspNetCore.Endpoints](https://www.nuget.org/packages/App.Metrics.AspNetCore.Endpoints/)|A set of middleware components for exposing metrics over HTTP as well as information about the application's running environment.
|[App.Metrics.AspNetCore.Reporting](https://www.nuget.org/packages/App.Metrics.AspNetCore.Reporting/)|Provides a away of scheduling configured reporters to periodically flush metrics.
|[App.Metrics.AspNetCore.Hosting](https://www.nuget.org/packages/App.Metrics.AspNetCore.Hosting/)|Provides [Microsoft.Extensions.Hosting.IWebHostBuilder](https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.hosting.iwebhostbuilder?view=aspnetcore-2.0) extensions methods to configure and host App Metrics in ASP.NET core applications.
|[App.Metrics.AspNetCore.Mvc](https://www.nuget.org/packages/App.Metrics.AspNetCore.Mvc/)|Includes App Metrics ASP.NET Core packages as well as ASP.NET Core MVC specifics such as supporting metric tagging using [MvcAttributeRouteHandler](https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.internal.mvcattributeroutehandler?view=aspnetcore-2.0) routes.
|[App.Metrics.AspNetCore](https://www.nuget.org/packages/App.Metrics.AspNetCore/)|Similar to `App.Metrics.AspNetCore.Mvc`, which includes App Metrics ASP.NET Core feature packages as well as additional [Microsoft.Extensions.Hosting.IWebHostBuilder](https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.hosting.iwebhostbuilder?view=aspnetcore-2.0) extensions methods to simplify hosting App Metrics in ASP.NET core applications.

The following documentation sections provide more details into App Metric's ASP.NET Core support:

{{% children description="true" %}}