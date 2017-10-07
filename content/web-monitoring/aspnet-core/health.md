---
title: "Health"
date: 2017-09-28T22:21:34+10:00
draft: false
chapter: false
weight: 4
icon: "/images/health.png"
---

## Overview

App Metrics provides a set of support packages to integrate [App Metrics Health]({{< ref "health-checks/_index.md" >}}) functionality in an ASP.NET Core application. The core packages are as follows:

|Package|Description|
|------|:--------|
|[App.Metrics.AspNetCore.Health.Endpoints](https://www.nuget.org/packages/App.Metrics.AspNetCore.Health.Endpoints/)|Middleware for configuring and exposing health check results over HTTP as well as a ping check.
|[App.Metrics.AspNetCore.Health.Hosting](https://www.nuget.org/packages/App.Metrics.AspNetCore.Health.Hosting/)|Provides [Microsoft.Extensions.Hosting.IWebHostBuilder](https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.hosting.iwebhostbuilder?view=aspnetcore-2.0) extensions methods to configure and host App Metrics Health in ASP.NET core applications.
|[App.Metrics.AspNetCore.Health](https://www.nuget.org/packages/App.Metrics.AspNetCore.Health/)|A sort of meta package which includes App Metrics Health ASP.NET Core feature packages as well as additional [Microsoft.Extensions.Hosting.IWebHostBuilder](https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.hosting.iwebhostbuilder?view=aspnetcore-2.0) extensions methods to simplify hosting App Metrics Health in ASP.NET core applications.

### Getting started

The quickest way to get started with health checks in an ASP.NET Core application is to use the `UseHealth` extension method on `Microsoft.AspNetCore.Hosting.IWebHostBuilder`. This will automatically scan the executing assembly, as well as any of it's referenced assemblies with a dependency on `App.Metrics.Health.*`, for health check implementations and register them.

<i class="fa fa-hand-o-right"></i> Create a [new ASP.NET Core MVC project](https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-mvc-app/start-mvc).

<i class="fa fa-hand-o-right"></i> Then install the [App.Metrics.AspNetCore.Health](https://www.nuget.org/packages/App.Metrics.AspNetCore.Health/) nuget package:

```console
nuget install App.Metrics.AspNetCore.Health
```

<i class="fa fa-hand-o-right"></i> Modify the `Program.cs` to apply the App Metrics ASP.NET Core defaults:

```csharp
public static class Program
{
    public static IWebHost BuildWebHost(string[] args)
    {
        return WebHost.CreateDefaultBuilder(args)
                .UseHealth()
                .UseStartup<Startup>()
                .Build();
    }

    public static void Main(string[] args) { BuildWebHost(args).Run(); }
}
```

{{% notice info %}}
See [defining health checks]({{< ref "health-checks/_index.md#defining-health-checks" >}}) for details on how to implement application health checks.
{{% /notice %}}

{{% notice note %}}
The [App.Metrics.AspNetCore.Health](https://www.nuget.org/packages/App.Metrics.AspNetCore.Health/) nuget package is a meta package that includes App Metrics Health feature packages.
{{% /notice %}}

## Endpoints Middleware

The [App.Metrics.AspNetCore.Health.Endpoints](https://www.nuget.org/packages/App.Metrics.AspNetCore.Health.Endpoints/) nuget package provides middleware which can be configured to expose an endpoint whereby health check results can be exposed over HTTP in different formats, as well as a simple ping endpoint. If you have followed the getting started section above, the `UseHealth` extension method on `Microsoft.AspNetCore.Hosting.IWebHostBuilder` already configures this feature.

### Endpoints Provided

The following lists the endpoints provided:

|Endpoint|Description|
|---------|:--------|
|`/health`|Executes the configured health checks and response with the result of each health check as well as an overall health status.
|`/ping`|Used to determine if you can get a successful "pong" response with a 200 HTTP status code, useful for load balancers.

<i class="fa fa-hand-o-right"></i> The overall health status returned by the `/health` endpoint can be either: **Healthy**, **Degraded** and **Unhealthy**.

|Status|Description|
|------|:--------|
|Healthy|Can be used to indicate that a check has passed. When all health checks have passed, `/health` responds with a *200 Http Status*.
|Degraded|Can be used to indicate that the check has failed but the application is still functioning as expected, this can be useful for example when different thresholds are met on the number of messages in a queue or an SSL certificate used for signing tokens is about to expire. When one or more health checks return a degraded result, `/health` responsds with a *200 Http Status* as well as a *Warning* response header.
|Unhealthy|Can be used to indicate that the check has failed. When one or more health checks return an unhealthy result, `/health` responds with a *500 Http Status*. Any health check which throws an uncaught exception will result in an unhealthy result.

{{% notice info %}}
The `/health` endpoint supports content negotiation via the *Accept* header if mulitple formatters are configured.
{{% /notice %}}

### How to use

`App.Metrics.AspNetCore.Health.Endpoints` supports a couple ways to enable health endpoints in an ASP.NET Core application:

1. Using the `Microsoft.AspNetCore.WebHost` in a `Program.cs`.
1. Using the `Microsoft.AspNetCore.Builder.IApplicationBuilder` in a `Startup.cs`.

<i class="fa fa-hand-o-right"></i> First install the [nuget package](https://www.nuget.org/packages/App.Metrics.AspNetCore.Health.Endpoints/):

```console
nuget install App.Metrics.AspNetCore.Health.Endpoints
```

<i class="fa fa-hand-o-right"></i> If bootstrapping with the `Microsoft.AspNetCore.WebHost`:

This is a simpler approach as it will wire up the endpoint middleware on the `IApplicationBuilder`, as well as the health infrastructure on the `IServiceCollection` for you.

```csharp
public static class Program
{
    public static IWebHost BuildWebHost(string[] args)
    {
        return WebHost.CreateDefaultBuilder(args)
                        .UseHealthEndpoints()
                        .UseStartup<Startup>()
                        .Build();
    }

    public static void Main(string[] args) { BuildWebHost(args).Run(); }
}
```

<i class="fa fa-hand-o-right"></i> If bootstrapping in the `Startup.cs`:

```csharp
public class Startup
{
    public void Configure(IApplicationBuilder app)
    {
        // To add all supported endpoints
        app.UseHealthAllEndpoints();

        // Or to cherry-pick endpoint of interest
        // app.UseHealthEndpoint();
        // app.UsePingEndpoint();
    }

    public void ConfigureServices(IServiceCollection services)
    {
        var metrics = AppMetricsHealth.CreateDefaultBuilder()
            .HealthChecks.RegisterFromAssembly(services, Assembly.GetEntryAssembly().GetName().Name)
            ... // configure options and add health checks
            .Build();

        services.AddMetrics(metrics);
        services.AddHealthEndpoints();
    }
}
```

{{% notice tip %}}
The `RegisterFromAssembly()` extension method nuget can be used to automatically scan and register any class inheriting `HealthCheck`.
{{% /notice %}}

### Configuration

<i class="fa fa-hand-o-right"></i> The `App.Metrics.AspNetCore.Health.Endpoints` nuget package supports the following configuration:

|Property|Description|
|------|:--------|
|HealthEndpointEnabled|Allows enabling/disabling of the `/health` endpoint, when disabled will result in a 404 status code, the default is `true`.
|PingEndpointEnabled|Allows enabling/disabling of the `/ping` endpoint, when disabled will result in a 404 status code, the default is `true`.
|HealthEndpointOutputFormatter|The formatter used to serialize health check results when `/health` is requested.
|Timeout|A `TimeSpan` for the `/health` request to execute in before cancelling the request.

#### Configure in code

<i class="fa fa-hand-o-right"></i> Endpoint configuration can be applied using `UseHealthEndpoints()`:

```csharp
...

.UseHealthEndpoints(options =>
{
    // apply configuration options
});

...
```

{{% notice info %}}
The configuration set with `Microsoft.Extensions.Configuration.IConfiguration` will override any code configuration.
{{% /notice %}}

#### Configure using [IConfiguration](https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.configuration.iconfiguration?view=aspnetcore-2.0)

<i class="fa fa-hand-o-right"></i> Endpoint configuration is automatically applied from the `Microsoft.Extensions.Configuration.IConfiguration`.

An `appsettings.json` can be used for example:

```json
 "HealthEndpointsOptions": {
    "HealthEndpointEnabled": true,
    "PingEndpointEnabled": true,
    "Timeout": "0:0:10"
  }
```

{{% notice note %}}
At the moment formatters cannot be configured via configuration setting files.
{{% /notice %}}

### Hosting Configuration

The `App.Metrics.AspNetCore.Health.Endpoints` nuget package includes endpoint hosting options:

|Property|Description|
|------|:--------|
|AllEndpointsPort|Allows a port to be specified on which the configured endpoints will be available. This value will override any other endpoint port configuration.
|HealthEndpointPort|Allows a port to be specified on which the health endpoint will be available.
|HealthEndpoint|The path to use for the health endpoint, the defaults is `/health`.
|PingEndpoint|The path to use for the ping endpoint, the defaults is `/ping`.
|PingEndpointPort|The port to use for the ping endpoint, if not specified uses your application's default port.

{{% notice tip %}}
External monitoring tools can be used to request the `/health` endpoint at a configured interval to continously monitor and alert the health of an API.
{{% /notice %}}

<i class="fa fa-hand-o-right"></i> To modify these configuration options, use the `ConfigureAppHealthHostingConfiguration()` extension method on the `IWebHostBuilder`:

```csharp
...

.ConfigureAppHealthHostingConfiguration(options =>
{
    options.AllEndpointsPort = 1111;
    options.HealthEndpoint = "app-metrics-health";
});

...
```

{{% notice note %}}
When a custom port is defined on any or all of the endpoints, App Metrics Health will append the additional urls with the defined ports to the [Microsoft.AspNetCore.Hosting.WebHostDefaults.ServerUrlsKey](https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.hosting.webhostdefaults.serverurlskey?view=aspnetcore-2.0)'s value.
{{% /notice %}}

## Adding in-line and pre-defined checks

As well as automatically registering any `HealthCheck` implementation, in-line and [pre-defined]({{< ref "health-checks/pre-defined-checks.md" >}}) checks can be added when building an `IWebHost` as show in the prevous code sample:

### How to use

```csharp
...

return WebHost.CreateDefaultBuilder(args)
  .ConfigureHealthWithDefaults(
    builder =>
    {
        builder.HealthChecks.AddCheck("DatabaseConnected", () => new ValueTask<HealthCheckResult>(HealthCheckResult.Healthy("Database Connection OK")));
        builder.HealthChecks.AddPingCheck("google ping", "google.com", TimeSpan.FromSeconds(10));
    })
  .UseHealth()
  .UseStartup<Startup>()
  .Build();

...
```

## Formatting Results

Health results can be serialized is different formats. App Metrics Health provides Plain-Text and JSON formatters where JSON format is the default.

### How to use

<i class="fa fa-hand-o-right"></i> To use plain-text formatting for example, we can use the `ConfigureHealth` extension on the `IWebHostBuilder` which provides access to the [HealthBuilder]({{< ref "health-checks/_index.md#basics" >}}) without applying the default configuration:

```csharp
...

return WebHost.CreateDefaultBuilder(args)
  .ConfigureHealth(
    builder =>
    {
        builder.OutputHealth.AsPlainText();

        ...
    })
  .UseHealth()
  .UseStartup<Startup>()
  .Build();

...
```

<i class="fa fa-hand-o-right"></i> Similary to use JSON formatting:

```csharp
...

return WebHost.CreateDefaultBuilder(args)
  .ConfigureHealth(
    builder =>
    {
        builder.OutputHealth.AsJson();

        ...
    })
  .UseHealth()
  .UseStartup<Startup>()
  .Build();

...
```

{{% notice note %}}
`ConfigureHealthWithDefaults` will apply the JSON formatter without you having to register it explicity.
{{% /notice %}}

### Example Reponses

The following are example response generated by the App Metrics Health ASP.NET Core MVC [sandbox project](https://github.com/AppMetrics/AspNetCoreHealth/tree/dev/sandbox/HealthSandboxMvc)

{{%expand "metrics response using Plain Text formatting" %}}

```text
# OVERALL STATUS: Healthy
--------------------------------------------------------------
# CHECK: google ping

           MESSAGE = FAILED. google.com
            STATUS = Unhealthy
--------------------------------------------------------------
# CHECK: DatabaseConnected

           MESSAGE = OK. Database Connection
            STATUS = Healthy
--------------------------------------------------------------
```
{{% /expand%}}
{{%expand "metrics response using JSON formatting" %}}


```json
{
  "degraded": {
    "message queue reached the threshold": "5000 messages in the processing queue which is above the threshold of 4000",
    "signing certificate expiry": "the signing certificate is going to expire in 1 week"
  },
  "healthy": {
    "database connection": "able make a connection to the database"
  },
  "status": "Unhealthy",
  "timestamp": "0001-01-01T00:00:00.0000Z",
  "unhealthy": {
    "unable to ping x api": "a connection to x could not be made"
  }
}
```
{{% /expand%}}