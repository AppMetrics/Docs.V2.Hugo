---
title: "GrafanaCloud Hosted Metrics"
draft: false
weight: 1
icon: "/images/grafana.png"
---

## GrafanaCloud Hosted Metrics

{{% notice warning %}}
The GrafanaCloud Hosted Metrics Reporter is not yet released to nuget. Dev builds can be found on [MyGet](https://www.myget.org/feed/appmetrics/package/nuget/App.Metrics.Reporting.GrafanaCloudHostedMetrics)
{{% /notice %}}

The [App.Metrics.Reporting.GrafanaCloudHostedMetrics](https://www.nuget.org/packages/App.Metrics.Reporting.GrafanaCloudHostedMetrics/) nuget package reports metrics to [GrafanaCloud Hosted Metrics](https://grafana.com/cloud/metrics) using the [App.Metrics.Formatters.GrafanaCloudHostedMetrics](https://www.nuget.org/packages/App.Metrics.Formatters.GrafanaCloudHostedMetrics/) nuget package to format metrics.

### Getting started

<i class="fa fa-hand-o-right"></i> To use the GrafanaCloud Hosted Metrics reporter, first install the [nuget package](https://www.nuget.org/packages/App.Metrics.Reporting.GrafanaCloudHostedMetrics/):

```console
nuget install App.Metrics.Reporting.GrafanaCloudHostedMetrics
```

<i class="fa fa-hand-o-right"></i> Then enable the reporter using `Report.ToHostedMetrics(...)`:

```csharp
var metrics = new MetricsBuilder()
    .Report.ToHostedMetrics("base url of your hosted metrics", "your hosted metrics api key")
    .Build();
```

<i class="fa fa-hand-o-right"></i> App Metrics at the moment leaves report scheduling up the the user unless using [App.Metrics.AspNetCore.Reporting]({{< ref "web-monitoring/aspnet-core/reporting.md" >}}). To run all configured reports use the `ReportRunner` on `IMetricsRoot`:

```csharp
await metrics.ReportRunner.RunAllAsync();
```

{{% notice info %}}
Report Scheduling will be added when [Microsoft.Extensions.Hosting.Background](https://github.com/aspnet/Hosting/blob/dev/src/Microsoft.Extensions.Hosting.Abstractions/BackgroundService.cs) is released, otherwise there are various other scheduling solutions available for C#. For ASP.NET Core reporting see details on the [App.Metrics.AspNetCore.Reporting]({{< ref "web-monitoring/aspnet-core/reporting.md" >}}) support package.
{{% /notice %}}

### Configuration

Configuration options are provided as a setup action used with `ToHostedMetrics(...)`.

<i class="fa fa-hand-o-right"></i> To configure GrafanaCloud Hosted Metrics reporting options:

```csharp
var filter = new MetricsFilter().WhereType(MetricType.Timer);
var metrics = new MetricsBuilder()
    .Report.ToInfluxDb(
        options => {
            options.HostedMetrics.BaseUri = "base url of your hosted metrics";
            options.HostedMetrics.ApiKey = "your hosted metrics api key";
            options.HttpPolicy.BackoffPeriod = TimeSpan.FromSeconds(30);
            options.HttpPolicy.FailuresBeforeBackoff = 5;
            options.HttpPolicy.Timeout = TimeSpan.FromSeconds(10);
            options.Filter = filter;
            options.FlushInterval = TimeSpan.FromSeconds(20);
        })
    .Build();
```

<i class="fa fa-hand-o-right"></i> The configuration options provided are:

|Option|Description|
|------|:--------|
|MetricsHostedMetricsJsonOutputFormatter|The metric payment formatter used when reporting metrics to GrafanCloud Hosted Metrics.
|Filter|The filter used to filter metrics just for this reporter.
|FlushInterval|The delay between reporting metrics to InfluxDB.
|InfluxDb.BaseUri|The URI of the GrafanaCloud Hosted Metrics instance.
|InfluxDb.ApiKey|The Api Key which your application will use to authenticate with GrafanaCloud Hosted Metrics.
|HttpPolicy.BackoffPeriod|The `TimeSpan` to back-off when metrics are failing to report to the metrics ingress endpoint.
|HttpPolicy.FailuresBeforeBackoff|The number of failures before backing-off when metrics are failing to report to the metrics ingress endpoint.
|HttpPolicy.Timeout|The HTTP timeout duration when attempting to report metrics to the metrics ingress endpoint.

## Web Monitoring

### Grafana

The [App.Metrics.AspNetCore.Tracking](https://www.nuget.org/packages/App.Metrics.AspNetCore.Tracking/) nuget package automatically records typical web metrics when added to an ASP.NET Core application. App Metrics includes a couple of pre-built Grafana dashboards to visualize these metrics:

Install the App Metrics [Grafana dashboard](https://grafana.com/dashboards/5117). There is also an [OAuth2 dashboard](https://grafana.com/dashboards/5117) for APIs using OAuth2.