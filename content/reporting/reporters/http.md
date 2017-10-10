---
title: "Http"
draft: false
weight: 5
icon: "/images/http.png"
---

The [App.Metrics.Reporting.Http](https://www.nuget.org/packages/App.Metrics.Reporting.Http/) nuget package reports metrics to a custom HTTP endpoint. The default output is JSON using [App.Metrics.Formatters.JSON](https://www.nuget.org/packages/App.Metrics.Formatters.JSON/) which can be substituted with any other [App Metrics Formatter]({{< ref "reporting/formatting/_index.md" >}}).

## Getting started

<i class="fa fa-hand-o-right"></i> To use the HTTP reporter, first install the [nuget package](https://www.nuget.org/packages/App.Metrics.Reporting.HTTP/):

```console
nuget install App.Metrics.Reporting.HTTP
```

<i class="fa fa-hand-o-right"></i> Then enable the reporter using `Report.OverHttp(url)`:

```csharp
var metrics = new MetricsBuilder()
    .Report.OverHttp("http://localhost/metrics")
    .Build();
```

<i class="fa fa-hand-o-right"></i> App Metrics at the moment leaves report scheduling up the the user unless using [App.Metrics.AspNetCore]({{< ref "reporting/aspnet-core/_index.md#reporter" >}}). To run all configured reports use the `ReportRunner` on `IMetricsRoot`:

```csharp
await metrics.ReportRunner.RunAllAsync();
```

{{% notice info %}}
Report Scheduling will be added when [Microsoft.Extensions.Hosting.Background](https://github.com/aspnet/Hosting/blob/dev/src/Microsoft.Extensions.Hosting.Abstractions/BackgroundService.cs) is released, otherwise there are various other scheduling solutions available for C#.
{{% /notice %}}

## Configuration

Configuration options are provided as a setup action used with `OverHttp()`.

<i class="fa fa-hand-o-right"></i> To configure HTTP reporting options:

```csharp
var filter = new MetricsFilter().WhereType(MetricType.Timer);
var metrics = new MetricsBuilder()
    .Report.OverHttp(
        options => {
            options.HttpSettings.RequestUri = new Uri("http://localhost/metrics");.
            options.HttpSettings.UserName = "admin";
            options.HttpSettings.Password = "password";
            options.HttpPolicy.BackoffPeriod = TimeSpan.FromSeconds(30);
            options.HttpPolicy.FailuresBeforeBackoff = 5;
            options.HttpPolicy.Timeout = TimeSpan.FromSeconds(10);
            options.MetricsOutputFormatter = new MetricsJsonOutputFormatter();
            options.Filter = filter;
            options.FlushInterval = TimeSpan.FromSeconds(20);
        })
    .Build();
```

<i class="fa fa-hand-o-right"></i> The configuration options are:

|Option|Description|
|------|:--------|
|MetricsOutputFormatter|The formatter used when reporting metrics over HTTP.
|Filter|The filter used to filter metrics just for this reporter.
|FlushInterval|The delay between reporting metrics over HTTP.
|HttpSettings.RequestUri|The endpoint accepting metrics in the specified format.
|HttpSettings.UserName|The username when using basic auth on the metrics ingress endpoint.
|HttpSettings.Password|The password when using basic auth on the metrics ingress endpoint.
|HttpPolicy.BackoffPeriod|The `TimeSpan` to back-off when metrics are failing to report to the metrics ingress endpoint.
|HttpPolicy.FailuresBeforeBackoff|The number of failures before backing-off when metrics are failing to report to the metrics ingress endpoint.
|HttpPolicy.Timeout|The HTTP timeout duration when attempting to report metrics to the metrics ingress endpoint.