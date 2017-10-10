---
title: "Endpoints"
draft: false
weight: 3
icon: "/images/webmetrics.png"
---

## Endpoints Middleware

The [App.Metrics.AspNetCore.Endpoints](https://www.nuget.org/packages/App.Metrics.AspNetCore.Endpoints/) nuget package provides a set of middleware components which can be configured to expose endpoints whereby metric snapshots can be exposed over HTTP in different formats as well as information about the running environment of the application.

### Endpoints Provided

The following lists the endpoints provided:

|Endpoint|Description|
|---------|:--------|
|`/metrics`|Exposes a metrics snapshot using the configured metrics formatter.
|`/metrics-text`|Exposes a metrics snapshot using the configured text formatter.
|`/env`|Exposes environment information about the application e.g. OS, Machine Name, Assembly Name, Assembly Version etc.

{{% notice info %}}
Both the `/metrics` and `/metrics-text` endpoints support content negotiation if mulitple [formatters]({{< ref "getting-started/formatting-metrics/_index.md" >}}) are configured, the thinking in providing both is that: `/metrics-text` could be used as a quick way to observe all recorded metrics in a human readable format using the [Plain Text formatter](https://www.nuget.org/packages/App.Metrics.Foramtters.Ascii/) from a browser when the `/metrics` endpoint used a type of binary formatter for example, or in cases like Prometheus's scrape config where it might not be possible to modify the request headers when using mulitple metrics formatters.
{{% /notice %}}

### How to use

`App.Metrics.AspNetCore.Endpoints` supports a couple ways to enable such endpoints in an ASP.NET Core application:

1. Using the `Microsoft.AspNetCore.WebHost` in a `Program.cs`.
1. Using the `Microsoft.AspNetCore.Builder.IApplicationBuilder` in a `Startup.cs`.

<i class="fa fa-hand-o-right"></i> First install the [nuget package](https://www.nuget.org/packages/App.Metrics.AspNetCore.Endpoints/):

```console
nuget install App.Metrics.AspNetCore.Endpoints
```

<i class="fa fa-hand-o-right"></i> If bootstrapping with the `Microsoft.AspNetCore.WebHost`:

This is a simpler approach as it will wire up the endpoint middleware on the `IApplicationBuilder`, as well as the metrics infrastructure on the `IServiceCollection` for you.

```csharp
public static class Program
{
    public static IWebHost BuildWebHost(string[] args)
    {
        return WebHost.CreateDefaultBuilder(args)
                        .UseMetricsEndpoints()
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
        app.UseMetricsAllEndpoints();

        // Or to cherry-pick endpoint of interest
        // app.UseMetricsEndpoint();
        // app.UseMetricsTextEndpoint();
        // app.UseEnvInfoEndpoint();
    }

    public void ConfigureServices(IServiceCollection services)
    {
        var metrics = AppMetrics.CreateDefaultBuilder()
            ... // configure other options
            .Build();

        services.AddMetrics(metrics);
        services.AddMetricsEndpoints();
    }
}
```

### Configuration

<i class="fa fa-hand-o-right"></i> The `App.Metrics.AspNetCore.Endpoints` nuget package supports the following configuration:

|Property|Description|
|------|:--------|
|MetricsEndpointEnabled|Allows enabling/disabling of the `/metrics` endpoint, when disabled will result in a 404 status code, the default is `true`.
|MetricsTextEndpointEnabled|Allows enabling/disabling of the `/metrics-text` endpoint, when disabled will result in a 404 status code, the default is `true`.
|EnvironmentInfoEndpointEnabled|Allows enabling/disabling of the `/env` endpoint, when disabled will result in a 404 status code, the default is `true`.
|MetricsEndpointOutputFormatter|The formatter used to serialize a snapshot of metrics when `/metrics` is requested.
|MetricsTextEndpointOutputFormatter|The formatter used to serialize a snapshot of metrics when `/metrics-text` is requested.
|EnvInfoEndpointOutputFormatter|The formatter used to serialize environment information when `/env` is requested.

#### Configure in code

<i class="fa fa-hand-o-right"></i> Endpoint configuration can be applied using `UseMetricsEndpoints()`:

```csharp
...

.UseMetricsEndpoints(options =>
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
 "MetricEndpointsOptions": {
    "MetricsEndpointEnabled": true,
    "MetricsTextEndpointEnabled": true,
    "EnvironmentInfoEndpointEnabled": true
  }
```

{{% notice note %}}
At the moment formatters cannot be configured via configuration setting files.
{{% /notice %}}

### Hosting Configuration

As well as the endpoint configuration above, the `App.Metrics.AspNetCore.Endpoints` nuget package includes endpoint hosting options:

|Property|Description|
|------|:--------|
|AllEndpointsPort|Allows a port to be specified on which the configured endpoints will be available. This value will override any other endpoint port configuration.
|EnvironmentInfoEndpoint|The path to use for the environment info endpoint, the defaults is `/env`.
|EnvironmentInfoEndpointPort|The port to use for the environment info endpoint, if not specified uses your application's default port.
|MetricsEndpoint|The path to use for the metrics endpoint, the defaults is `/metrics`.
|MetricsEndpointPort|The port to use for the metrics endpoint, if not specified uses your application's default port.
|MetricsTextEndpoint|The path to use for the metrics text endpoint, the defaults is `/metrics-text`.
|MetricsTextEndpointPort|The port to use for the metrics text endpoint, if not specified uses your application's default port.

<i class="fa fa-hand-o-right"></i> To modify these configuration options, use the `ConfigureAppMetricsHostingConfiguration()` extension on the `IWebHostBuilder`:

```csharp
...

.ConfigureAppMetricsHostingConfiguration(options =>
{
    options.AllEndpointsPort = 1111;
    options.MetricsEndpoint = "app-metrics";
});

...
```

{{% notice note %}}
When a custom port is defined on any or all of the endpoints, App Metrics will append the additional urls with the defined ports to the [Microsoft.AspNetCore.Hosting.WebHostDefaults.ServerUrlsKey](https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.hosting.webhostdefaults.serverurlskey?view=aspnetcore-2.0)'s value.
{{% /notice %}}

### Example Reponses

The following are example response generated by the App Metrics ASP.NET Core MVC [sandbox project](https://github.com/AppMetrics/AspNetCore/tree/dev/sandbox/MetricsSandboxMvc)

{{%expand "metrics response using Plain Text formatting" %}}
```text
# TIMESTAMP: 0001-01-01T00:00:00Z
# MEASUREMENT: [Sandbox] Database Call
# TAGS:
              client_id = client-1
                 server = server1
                    app = MetricsSandboxMvc
                    env = configoverride
                  mtype = timer
                   unit = none
               unit_dur = ms
              unit_rate = min
# FIELDS:
            count.meter = 1
                 rate1m = 0
                 rate5m = 0
                rate15m = 0
              rate.mean = 131.315109396188
                samples = 1
                   last = 301.420798
             count.hist = 1
                    sum = 301.420798
                    min = 301.420798
                    max = 301.420798
                   mean = 301.420798
                 median = 301.420798
                 stddev = 0
                   p999 = 301.420798
                    p99 = 301.420798
                    p98 = 301.420798
                    p95 = 301.420798
                    p75 = 301.420798
--------------------------------------------------------------
# TIMESTAMP: 0001-01-01T00:00:00Z
# MEASUREMENT: [Application.HttpRequests] Apdex
# TAGS:
                 server = server1
                    app = MetricsSandboxMvc
                    env = configoverride
                  mtype = apdex
                   unit = result
# FIELDS:
                samples = 0
                  score = 1
              satisfied = 0
             tolerating = 0
            frustrating = 0
--------------------------------------------------------------
# TIMESTAMP: 0001-01-01T00:00:00Z
# MEASUREMENT: [Application.HttpRequests] One Minute Error Percentage Rate
# TAGS:
                 server = server1
                    app = MetricsSandboxMvc
                    env = configoverride
                  mtype = gauge
                   unit = req
# FIELDS:
                  value = 17.668121810276
--------------------------------------------------------------
# TIMESTAMP: 0001-01-01T00:00:00Z
# MEASUREMENT: [Application.HttpRequests] POST Size
# TAGS:
                 server = server1
                    app = MetricsSandboxMvc
                    env = configoverride
                  mtype = histogram
                   unit = B
# FIELDS:
                samples = 1
                   last = 43007
             count.hist = 1
                    sum = 43007
                    min = 43007
                    max = 43007
                   mean = 43007
                 median = 43007
                 stddev = 0
                   p999 = 43007
                    p99 = 43007
                    p98 = 43007
                    p95 = 43007
                    p75 = 43007
--------------------------------------------------------------
# TIMESTAMP: 0001-01-01T00:00:00Z
# MEASUREMENT: [Application.HttpRequests] PUT Size
# TAGS:
                 server = server1
                    app = MetricsSandboxMvc
                    env = configoverride
                  mtype = histogram
                   unit = B
# FIELDS:
                samples = 1
                   last = 4084
             count.hist = 1
                    sum = 4084
                    min = 4084
                    max = 4084
                   mean = 4084
                 median = 4084
                 stddev = 0
                   p999 = 4084
                    p99 = 4084
                    p98 = 4084
                    p95 = 4084
                    p75 = 4084
--------------------------------------------------------------
```
{{% /expand%}}
{{%expand "metrics response using JSON formatting" %}}
```json
{
  "timestamp": "0001-01-01T00:00:00Z",
  "contexts": [
    {
      "context": "context_one",
      "apdexScores": [
        {
          "name": "test_apdex",
          "frustrating": 10,
          "sampleSize": 200,
          "satisfied": 170,
          "score": 0.9,
          "tolerating": 20,
          "tags": {
            "host": "server1",
            "env": "staging"
          }
        }
      ],
      "counters": [
        {
          "name": "test_counter",
          "unit": "items",
          "count": 200,
          "items": [
            {
              "count": 20,
              "item": "item1",
              "percent": 10.0
            },
            {
              "count": 40,
              "item": "item2",
              "percent": 20.0
            },
            {
              "count": 140,
              "item": "item3",
              "percent": 70.0
            }
          ],
          "tags": {
            "host": "server1",
            "env": "staging"
          }
        }
      ],
      "gauges": [
        {
          "name": "test_gauge",
          "unit": "calls",
          "value": 0.5,
          "tags": {
            "host": "server1",
            "env": "staging"
          }
        }
      ],
      "histograms": [
        {
          "name": "test_histogram",
          "unit": "items",
          "count": 1,
          "sum": 1.0,
          "lastUserValue": "3",
          "lastValue": 2.0,
          "max": 4.0,
          "maxUserValue": "5",
          "mean": 6.0,
          "median": 10.0,
          "min": 7.0,
          "minUserValue": "8",
          "percentile75": 11.0,
          "percentile95": 12.0,
          "percentile98": 13.0,
          "percentile99": 14.0,
          "percentile999": 15.0,
          "sampleSize": 16,
          "stdDev": 9.0,
          "tags": {
            "host": "server1",
            "env": "staging"
          }
        }
      ],
      "meters": [
        {
          "name": "test_meter",
          "unit": "calls",
          "count": 5,
          "fifteenMinuteRate": 4.0,
          "fiveMinuteRate": 3.0,
          "items": [
            {
              "count": 1,
              "fifteenMinuteRate": 5.0,
              "fiveMinuteRate": 4.0,
              "item": "item",
              "meanRate": 2.0,
              "oneMinuteRate": 3.0,
              "percent": 0.5
            }
          ],
          "meanRate": 1.0,
          "oneMinuteRate": 2.0,
          "rateUnit": "s",
          "tags": {
            "host": "server1",
            "env": "staging"
          }
        }
      ],
      "timers": [
        {
          "name": "test_timer",
          "unit": "req",
          "activeSessions": 0,
          "count": 5,
          "durationUnit": "ms",
          "histogram": {
            "sum": 1E-06,
            "lastUserValue": "3",
            "lastValue": 2E-06,
            "max": 4E-06,
            "maxUserValue": "5",
            "mean": 6E-06,
            "median": 9.999999999999999,
            "min": 7E-06,
            "minUserValue": "8",
            "percentile75": 1.1E-05,
            "percentile95": 1.2E-05,
            "percentile98": 1.3E-05,
            "percentile99": 1.4E-05,
            "percentile999": 1.4999999999999999,
            "sampleSize": 16,
            "stdDev": 1.4999999999999999
          },
          "rate": {
            "fifteenMinuteRate": 4.0,
            "fiveMinuteRate": 3.0,
            "meanRate": 1.0,
            "oneMinuteRate": 2.0
          },
          "rateUnit": "s",
          "tags": {
            "host": "server1",
            "env": "staging"
          }
        }
      ]
    }
  ]
}
```
{{% /expand%}}
{{%expand "env response using Plain Text formatting" %}}
```text
          Assembly Name = MetricsSandboxMvc
       Assembly Version = 2.0.0.0
  Framework Description = .NET Core 4.6.00001.0
             Local Time = 0001-01-01T00:00:00Z
           Machine Name = server1
        OS Architecture = X64
            OS Platform = WINDOWS
             OS Version = Microsoft Windows 10.0.15063
   Process Architecture = X64
```
{{% /expand%}}