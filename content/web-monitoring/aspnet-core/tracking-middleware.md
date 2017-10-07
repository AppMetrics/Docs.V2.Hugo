---
title: "Web Metrics"
date: 2017-09-28T22:49:49+10:00
draft: false
weight: 2
icon: "/images/webmetrics.png"
---

## Tracking Middleware

The [App.Metrics.AspNetCore.Tracking](https://www.nuget.org/packages/App.Metrics.AspNetCore.Tracking/) nuget package provides a set of middleware components which can be configured to automatically track typical metrics used in monitoring a web application.

### Metrics Recorded

The tracking middleware records typical web application metrics which are summarized below.

#### Apdex

Monitors an [Application Performance Index]({{< ref "getting-started/metric-types/apdex.md" >}}) on the overall response times allowing us to monitor end-user satisfication.

#### Errors

The error middleware records the following error metrics:

- The total number of error requests per http status code.
- The percentage of overall error requests and percentage of each endpoints error requests.
- An overall error request rate and error request rate per endpoint.
- A overall count of each uncaught exception types.
- A overall count of each uncaught exception types per endpoint.

{{% notice info %}}
There are several types of [Gauges]({{< ref "getting-started/metric-types/gauges.md" >}}) provided by App Metrics, a Hit Percentage Gauge is used for example to calculate the request error rate percentage by calculating the percentage of failed requests using the one minute rate of error requests and the one minute rate of overall web requests.
{{% /notice %}}

#### Throughput & Response Times

To measure the throughput and response times within a web application, [Timers]({{< ref "getting-started/metric-types/timers.md" >}}) are registered to record:

- The overall throughput and request duration of all routes.
- The throughput and request duration per route within the web application.

#### POST and PUT request sizes

[Histograms]({{< ref "getting-started/metric-types/histograms.md" >}}) are used to track POST and PUT requests sizes of incomming HTTP requests.

#### OAuth2

If your web application is secured with OAuth2, by default App.Metrics will record metrics on a per client basis. This provides some useful insights into clients usage of your APIs. The OAuth2 middleware will record:

- An overall and per endpoint request rate for each client.
- An overall and per endpoint error rate for each client.
- The POST and PUT request sizes for each client.

### How to use

`App.Metrics.AspNetCore.Tracking` supports a couple ways to enable metrics tracking in an ASP.NET Core application:

1. Using the `Microsoft.AspNetCore.WebHost` in a `Program.cs`.
1. Using the `Microsoft.AspNetCore.Builder.IApplicationBuilder` in a `Startup.cs`.

<i class="fa fa-hand-o-right"></i> First install the [nuget package](https://www.nuget.org/packages/App.Metrics.AspNetCore.Tracking/):

```console
nuget install App.Metrics.AspNetCore.Tracking
```

<i class="fa fa-hand-o-right"></i> If bootstrapping with the `Microsoft.AspNetCore.WebHost`:

This is a simpler approach as it will wire up the tracking middleware on the `IApplicationBuilder`, as well as tracking and metrics infrastructure on the `IServiceCollection` for you.

```csharp
public static class Program
{
    public static IWebHost BuildWebHost(string[] args)
    {
        return WebHost.CreateDefaultBuilder(args)
                        .UseMetricsWebTracking()
                        .UseStartup<Startup>()
                        .Build();
    }

    public static void Main(string[] args) { BuildWebHost(args).Run(); }
}
```

{{% notice info %}}
Configuration options can be passed into `UseMetricsWebTracking()`
{{% /notice %}}

<i class="fa fa-hand-o-right"></i> If bootstrapping in the `Startup.cs`:

```csharp
public class Startup
{
    public void Configure(IApplicationBuilder app)
    {
        // To add all available tracking middleware
        app.UseMetricsAllMiddleware();

        // Or to cherry-pick the tracking of interest
        // app.UseMetricsActiveRequestMiddleware();
        // app.UseMetricsErrorTrackingMiddleware();
        // app.UseMetricsPostAndPutSizeTrackingMiddleware();
        // app.UseMetricsRequestTrackingMiddleware();
        // app.UseMetricsOAuth2TrackingMiddleware();
        // app.UseMetricsApdexTrackingMiddleware();
    }

    public void ConfigureServices(IServiceCollection services)
    {
        var metrics = AppMetrics.CreateDefaultBuilder()
            ... // configure other options
            .Build();

        services.AddMetrics(metrics);
        services.AddMetricsTrackingMiddleware();
    }
}
```

### Configuration

<i class="fa fa-hand-o-right"></i> The `App.Metrics.AspNetCore.Tracking` nuget package supports the following configuration:

|Property|Description|
|------|:--------|
|IgnoredRoutesRegexPatterns|An list of regex patterns used to ignore matching routes from metrics tracking.
|OAuth2TrackingEnabled|Allows recording of all OAuth2 Client tracking to be enabled/disabled. Defaults to `true`.
|ApdexTrackingEnabled|Allows enabling/disabling of calculating the [Apdex]({{< ref "getting-started/metric-types/apdex.md">}}) score on the overall responses times. Defaults to `true`.
|ApdexTSeconds|The [Apdex]({{< ref "getting-started/metric-types/apdex.md">}}) T seconds value used in calculating the score on the samples collected.
|IgnoredHttpStatusCodes|Allows specific http status codes to be ignored when reporting on response related information, e.g. You might not want to monitor 404 status codes.

#### Configure in code

<i class="fa fa-hand-o-right"></i> Tracking configuration can be applied using `UseMetricsWebTracking()`:

```csharp
...

.UseMetricsWebTracking(options =>
{
    // apply configuration options
});

...
```

{{% notice info %}}
The configuration set with `Microsoft.Extensions.Configuration.IConfiguration` will override any code configuration.
{{% /notice %}}

#### Configure using [IConfiguration](https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.configuration.iconfiguration?view=aspnetcore-2.0)

<i class="fa fa-hand-o-right"></i> Web Tracking configuration is automatically applied from the `Microsoft.Extensions.Configuration.IConfiguration`. 

An `appsettings.json` can be used for example:

```json
  "MetricsWebTrackingOptions": {
    "ApdexTrackingEnabled": true,
    "ApdexTSeconds": 0.1,
    "IgnoredHttpStatusCodes": [ 404 ],
    "IgnoredRoutesRegexPatterns": [],
    "OAuth2TrackingEnabled": true
  },
```

{{% notice note %}}
At the moment formatters cannot be configured via configuration setting files.
{{% /notice %}}