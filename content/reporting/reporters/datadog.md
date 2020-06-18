---
title: "Datadog"
draft: false
weight: 4
icon: "/images/datadog.png"
---

## Datadog

The [App.Metrics.Datadog](https://www.nuget.org/packages/App.Metrics.Datadog/) nuget package reports metrics to [Datadog](https://www.datadoghq.com/) using the [App.Metrics.Formatting.Datadog](https://www.nuget.org/packages/App.Metrics.Formatting.Datadog/) and [App.Metrics.Reporting.Datadog](https://www.nuget.org/packages/App.Metrics.Reporting.Datadog/) nuget packages to format and report metrics.

### Datadog URL, API Key

You will need these two to correctly configure App.Metrics to report to Datadog over HTTP.

Create an API key in Datadog [here](https://app.datadoghq.com/account/settings#api).

The Datadog base URL to report metrics over http is https://api.datadoghq.com/

### Getting started

<i class="fa fa-hand-o-right"></i> To use the Datadog HTTP reporter, first install the [nuget package](https://www.nuget.org/packages/App.Metrics.Reporting.Datadog/):

```console
nuget install App.Metrics.Datadog
```

<i class="fa fa-hand-o-right"></i> Then enable the reporter using `Report.ToDatadogHttp(...)`:

```csharp
var metrics = new MetricsBuilder()
    .Report.ToDatadogHttp("base url of datadogs api", "datadog api key")
    .Build();
```

{{% notice info %}}
<i class="fa fa-hand-o-right"></i> See [Reporting]({{< ref "web-monitoring/aspnet-core/reporting.md" >}}) for details on configuring metric reporting.
{{% /notice %}}

### Configuration

Configuration options are provided as a setup action used with `ToDatadogHttp(...)`.

<i class="fa fa-hand-o-right"></i> To configure Datadog reporting options:

```csharp
var filter = new MetricsFilter().WhereType(MetricType.Timer);
var metrics = new MetricsBuilder()
    .Report.ToDatadogHttp(
        options => {
            options.Datadog.BaseUri = "base url of your datadog";
            options.Datadog.ApiKey = "datadog api key";
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
|MetricsDatadogJsonOutputFormatter|The metric payment formatter used when reporting metrics to Datadog
|Filter|The filter used to filter metrics just for this reporter.
|FlushInterval|The delay between reporting metrics.
|BaseUri|The URI of the Datadog's HTTP api
|ApiKey|The Api Key which your application will use to authenticate with Datadog.
|HttpPolicy.BackoffPeriod|The `TimeSpan` to back-off when metrics are failing to report to the metrics ingress endpoint.
|HttpPolicy.FailuresBeforeBackoff|The number of failures before backing-off when metrics are failing to report to the metrics ingress endpoint.
|HttpPolicy.Timeout|The HTTP timeout duration when attempting to report metrics to the metrics ingress endpoint.
