---
title: "Influx Data"
draft: false
weight: 1
icon: "/images/influx_logo.png"
---

## InfluxDB

The [App.Metrics.Reporting.InfluxDB](https://www.nuget.org/packages/App.Metrics.Reporting.InfluxDB/) nuget package reports metrics to [InfluxDB](https://www.influxdata.com/time-series-platform/influxdb/) using the [App.Metrics.Formatters.InfluxDB](https://www.nuget.org/packages/App.Metrics.Formatters.InfluxDB/) nuget package to format metrics by default using the [Line Protocol](https://docs.influxdata.com/influxdb/v1.3/write_protocols/line_protocol_tutorial/).

### Getting started

<i class="fa fa-hand-o-right"></i> To use the InfluxDB reporter, first install the [nuget package](https://www.nuget.org/packages/App.Metrics.Reporting.InfluxDB/):

```console
nuget install App.Metrics.Reporting.InfluxDB
```

<i class="fa fa-hand-o-right"></i> Then enable the reporter using `Report.ToInfluxDb(...)`:

```csharp
var metrics = new MetricsBuilder()
    .Report.ToInfluxDb("http://127.0.0.1:8086", "metricsdatabase")
    .Build();
```

<i class="fa fa-hand-o-right"></i> App Metrics at the moment leaves report scheduling up the the user unless using [App.Metrics.AspNetCore.Reporting]({{< ref "web-monitoring/aspnet-core/reporting.md" >}}). To run all configured reports use the `ReportRunner` on `IMetricsRoot`:

```csharp
await Task.WhenAll(metrics.ReportRunner.RunAllAsync());
```

{{% notice info %}}
Report Scheduling will be added when [Microsoft.Extensions.Hosting.Background](https://github.com/aspnet/Hosting/blob/dev/src/Microsoft.Extensions.Hosting.Abstractions/BackgroundService.cs) is released, otherwise there are various other scheduling solutions available for C#. For ASP.NET Core reporting see details on the [App.Metrics.AspNetCore.Reporting]({{< ref "web-monitoring/aspnet-core/reporting.md" >}}) support package.
{{% /notice %}}

### Configuration

Configuration options are provided as a setup action used with `ToInfluxDb(...)`.

<i class="fa fa-hand-o-right"></i> To configure InfluxDB reporting options:

```csharp
var filter = new MetricsFilter().WhereType(MetricType.Timer);
var metrics = new MetricsBuilder()
    .Report.ToInfluxDb(
        options => {
            options.InfluxDb.BaseUri = new Uri("http://127.0.0.1:8086");
            options.InfluxDb.Database = "metricsdatabase";
            options.InfluxDb.Consistenency = "consistency";
            options.InfluxDb.UserName = "admin";
            options.InfluxDb.Password = "password";
            options.InfluxDb.RetentionPolicy = "rp";
            options.InfluxDb.CreateDataBaseIfNotExists = true;
            options.HttpPolicy.BackoffPeriod = TimeSpan.FromSeconds(30);
            options.HttpPolicy.FailuresBeforeBackoff = 5;
            options.HttpPolicy.Timeout = TimeSpan.FromSeconds(10);
            options.MetricsOutputFormatter = new MetricsInfluxDbLineProtocolOutputFormatter();
            options.Filter = filter;
            options.FlushInterval = TimeSpan.FromSeconds(20);
        })
    .Build();
```

<i class="fa fa-hand-o-right"></i> The configuration options provided are:

|Option|Description|
|------|:--------|
|MetricsOutputFormatter|The formatter used when reporting metrics to InfluxDB.
|Filter|The filter used to filter metrics just for this reporter.
|FlushInterval|The delay between reporting metrics to InfluxDB.
|InfluxDb.Database|The InfluxDB database where metrics are reported.
|InfluxDb.BaseUri|The URI of the InfluxDB server.
|InfluxDb.UserName|The username when using basic auth to auth with InfluxDB.
|InfluxDb.Password|The password when using basic auth to auth with InfluxDB.
|InfluxDb.Consistenency|The InfluxDB database consistency level to use.
|InfluxDb.RetentionPolicy|The InfluxDB databases rentention policy to write metrics to.
|InfluxDb.CreateDataBaseIfNotExists|Will attempt to create the specified influxdb database if it does not exist.
|HttpPolicy.BackoffPeriod|The `TimeSpan` to back-off when metrics are failing to report to the metrics ingress endpoint.
|HttpPolicy.FailuresBeforeBackoff|The number of failures before backing-off when metrics are failing to report to the metrics ingress endpoint.
|HttpPolicy.Timeout|The HTTP timeout duration when attempting to report metrics to the metrics ingress endpoint.

## Web Monitoring

### Grafana

The [App.Metrics.AspNetCore.Tracking](https://www.nuget.org/packages/App.Metrics.AspNetCore.Tracking/) nuget package automatically records typical web metrics when added to an ASP.NET Core application. App Metrics includes a couple of pre-built Grafana dashboards to visualize these metrics:

Install the App Metrics [Grafana dashboard](https://grafana.com/dashboards/2125). There is also an [OAuth2 dashboard](https://grafana.com/dashboards/2137) for APIs using OAuth2.
