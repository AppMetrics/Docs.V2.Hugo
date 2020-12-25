---
title: "StatsD"
draft: false
weight: 4
icon: "/images/statsd.png"
---

## StatsD

The [App.Metrics.StatsD](https://www.nuget.org/packages/App.Metrics.StatsD/) nuget package reports metrics to [StatsD](https://github.com/statsd/statsd) using the [App.Metrics.Formatting.StatsD](https://www.nuget.org/packages/App.Metrics.Formatting.StatsD/) and [App.Metrics.Reporting.StatsD](https://www.nuget.org/packages/App.Metrics.Reporting.StatsD/) nuget packages to format and report metrics.

### Getting started

<i class="fa fa-hand-o-right"></i> To use the Datadog HTTP reporter, first install the [nuget package](https://www.nuget.org/packages/App.Metrics.Reporting.StatsD/):

```console
nuget install App.Metrics.StatsD
```

<i class="fa fa-hand-o-right"></i> Then enable the reporter using `Report.ToStatsDTcp(...)`:

```csharp
var metrics = new MetricsBuilder()
    .Report.ToStatsDTcp(options => {})
    .Build();
```

<i class="fa fa-hand-o-right"></i> or using `Report.ToStatsDUdp(...)`:

```csharp
var metrics = new MetricsBuilder()
    .Report.ToStatsDUdp(options => {})
    .Build();
```

{{% notice info %}}
<i class="fa fa-hand-o-right"></i> See [Reporting]({{< ref "web-monitoring/aspnet-core/reporting.md" >}}) for details on configuring metric report scheduling.
{{% /notice %}}

### Variants

There are several flavors of StatsD available. `App.Metrics.StatsD` currently supports the original Etsy and DogStatsD variants of StatsD, DogStatsD is a StatsD variant that is used to report StatsD metrics to DataDog; by default, the Etsy variant is used.

#### Supported Metrics

- __Gauge__ metrics will be formatted as StatsD gauge "__g__"
- __Count__ metrics will be formatted as StatsD count "__c__"
- __Histogram__ metrics will be formatted as StatsD histogram "__h__"
- __Meter__ metrics will be formatted as StatsD meter "__m__"
- __Timer__ metrics will be formatted as StatsD timer "__ms__"

{{% notice warning %}}
<i class="fa fa-hand-o-right"></i> Timer metrics are not natively supported by DogStatsD. Replace timer metric with histogram if you have to send a timer-like StatsD metric to DogStatsD ingress endpoint.
{{% /notice %}}

#### Formatting

To change the default formatter to DogStatsD, change the formatter in the options:

```csharp
var metrics = new MetricsBuilder()
    .Report.ToStatsDTcp(
        options => {
            options.StatsDOptions.MetricNameFormatter = new DefaultDogStatsDMetricStringSerializer();
        })
    .Build();
```

### Configuration

Configuration options are provided as a setup action used with `ToStatsDTcp(...)` and `ToStatsDUdp(...)`.

<i class="fa fa-hand-o-right"></i> To configure StatsD reporting options:

```csharp
var filter = new MetricsFilter().WhereType(MetricType.Timer);
var metrics = new MetricsBuilder()
    .Report.ToStatsDTcp(
        options => {
            options.StatsDOptions.MetricNameFormatter = new DefaultStatsDMetricStringSerializer();
            options.StatsDOptions.DefaultSampleRate = 1.0;
            options.StatsDOptions.TagMarker = '#';
            options.SocketPolicy.BackoffPeriod = TimeSpan.FromSeconds(30);
            options.SocketPolicy.FailuresBeforeBackoff = 5;
            options.SocketPolicy.Timeout = TimeSpan.FromSeconds(10);
            options.SocketSettings.Address = "localhost";
            options.SocketSettings.Port = 8125;
            options.Filter = filter;
        })
    .Build();
```

<i class="fa fa-hand-o-right"></i> The configuration options provided are:

|Option|Description|
|------|:--------|
|StatsDOptions.MetricNameFormatter|The metric payload formatter used when reporting StatsD metrics. There are 2 built-in formatter provided, `DefaultStatsDMetricStringSerializer` and `DefaultDogStatsDMetricStringSerializer`
|StatsDOptions.DefaultSampleRate|The sample rate used to send metrics that supports them.
|StatsDOptions.TagMarker|The character used to mark the start of tag section for reporters that supports metric tagging.
|Filter|The filter used to filter metrics just for this reporter.
|SocketPolicy.BackoffPeriod|The `TimeSpan` to back-off when metrics are failing to report to the metrics ingress endpoint.
|SocketPolicy.FailuresBeforeBackoff|The number of failures before backing-off when metrics are failing to report to the metrics ingress endpoint.
|SocketPolicy.Timeout|The socket timeout duration when attempting to report metrics to the metrics ingress endpoint.
|SocketSettings.Address|The socket address of the metrics ingress endpoint.
|SocketSettings.Port|The socket port of the metrics ingress endpoint.
