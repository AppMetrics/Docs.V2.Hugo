---
title: "Graphite"
draft: false
weight: 
icon: "/images/GraphiteLogo.png"
---

## Graphite

The [App.Metrics.Graphite](https://www.nuget.org/packages/App.Metrics.Graphite/) nuget package reports metrics to [Graphite](https://graphiteapp.org) using the [App.Metrics.Formatters.Graphite](https://www.nuget.org/packages/App.Metrics.Formatters.Graphite/) nuget package to format metrics by default using the [PlainText Protocol](http://graphite.readthedocs.io/en/latest/feeding-carbon.html#the-plaintext-protocol).

### Getting started

<i class="fa fa-hand-o-right"></i> To use the Graphite reporter, first install the [nuget package](https://www.nuget.org/packages/App.Metrics.Graphite/):

```console
nuget install App.Metrics.Graphite
```

<i class="fa fa-hand-o-right"></i> Then enable the reporter using `Report.ToGraphite(...)`:

```csharp
var metrics = new MetricsBuilder()
    .Report.ToGraphite("http://127.0.0.1:2003", TimeSpan.FromSeconds(5))
    .Build();
```

{{% notice info %}}
<i class="fa fa-hand-o-right"></i> See [Reporting]({{< ref "web-monitoring/aspnet-core/reporting.md" >}}) for details on configuring metric reporting.
{{% /notice %}}

```csharp
await Task.WhenAll(metrics.ReportRunner.RunAllAsync());
```

{{% notice info %}}
<i class="fa fa-hand-o-right"></i> See [Reporting]({{< ref "web-monitoring/aspnet-core/reporting.md" >}}) for details on configuring metric reporting.
{{% /notice %}}

### Configuration

Configuration options are provided as a setup action used with `ToGraphite(...)`.

<i class="fa fa-hand-o-right"></i> To configure ToGraphite reporting options:

```csharp
var filter = new MetricsFilter().WhereType(MetricType.Timer);
var metrics = new MetricsBuilder()
    .Report.ToGraphite(
        options => {
            options.Graphite.BaseUri = new Uri("http://127.0.0.1:8086");
            options.Graphite.Protocol = Protocol.Tcp; // or Protocol.Udp
            options.ClientPolicy.BackoffPeriod = TimeSpan.FromSeconds(30);
            options.ClientPolicy.FailuresBeforeBackoff = 5;
            options.ClientPolicy.Timeout = TimeSpan.FromSeconds(10);
            options.Filter = filter;
            options.FlushInterval = TimeSpan.FromSeconds(20);
        })
    .Build();
```

## Web Monitoring

### Grafana

The [App.Metrics.AspNetCore.Tracking](https://www.nuget.org/packages/App.Metrics.AspNetCore.Tracking/) nuget package automatically records typical web metrics when added to an ASP.NET Core application. App Metrics includes a couple of pre-built Grafana dashboards to visualize these metrics:

Install the App Metrics [Grafana dashboard](https://grafana.com/dashboards/2192). There is also an [OAuth2 dashboard](https://grafana.com/dashboards/2198) for APIs using OAuth2.
