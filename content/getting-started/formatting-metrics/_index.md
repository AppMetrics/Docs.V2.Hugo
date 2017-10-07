---
title: "Formatting Metrics"
date: 2017-09-28T22:21:52+10:00
draft: false
chapter: false
weight: 3
icon: "/images/formatting.png"
---

App Metrics supports formatting metrics in a [Plain Text](https://www.nuget.org/packages/App.Metrics.Formatters.Ascii/) and [JSON](https://www.nuget.org/packages/App.Metrics.Formatters.Json/) format as well as formatting metrics recorded in [supported time series database]({{< ref "reporting/reporters/_index.md">}}) formats such as InfluxDB, Prometheus, Elasticsearch and Graphite.

Formatter packages allow metric snapshots to be serialized, they are designed be used with reporters allowing us to *Push* metrics or wired-up with a web application allowing us to *Pull* metrics.

## Basics

One or more formatters can be configured using the `MetricsBuilder` as follows.

<i class="fa fa-hand-o-right"></i> First install the [App Metrics](https://www.nuget.org/packages/App Metrics/) nuget package which includes Plain Text and Json Formatters.

```console
nuget install App.Metrics
```

<i class="fa fa-hand-o-right"></i> Then to configure both the Plain Text and Json Formatters:

```csharp
var builder = new MetricsBuilder()
    .OutputMetrics.AsPlainText()
    .OutputMetrics.AsJson();
```

<i class="fa fa-hand-o-right"></i> Then to write metrics to `System.Console` without configuring a reporter, retreive a metrics snapshot and use each formatter to serialize and then write to `System.Console`:

```csharp
var metrics = builder.Build();

// TODO: Record some metrics

var snapshot = metrics.Snapshot.Get();

foreach(var formatter in metrics.OutputMetricsFormatters)
{
    using (var stream = new MemoryStream())
    {
        await formatter.WriteAsync(stream, snapshot);

        var result = Encoding.UTF8.GetString(stream.ToArray());

        System.Console.WriteLine(result);
    }
}
```

{{% notice tip %}}
When building an `IMetricsRoot` using `AppMetrics.CreateDefaultBuilder()` to build the default configuration, the plain text and JSON formatters are added as part of the defaults.
{{% /notice %}}

The following is an example ouput of a simple counter using the plain text formatter:

```text
# TIMESTAMP: 111111111111111111
# MEASUREMENT: [Application] my_counter
# TAGS:
                  mtype = counter
                   unit = none
# FIELDS:
                  value = 1
--------------------------------------------------------------
```

And the following an example output of a simple counter using the JSON formatter:

```json
{
  "timestamp": "0001-01-1T00:00:00.0000000Z",
  "contexts": [
    {
      "context": "Application",
      "counters": [
        {
          "name": "my_counter",
          "unit": "none",
          "count": 1
        }
      ]
    }
  ]
}
```

## Custom Formatters

To implement a custom formatter:

<i class="fa fa-hand-o-right"></i> First install the [App.Metrics.Abstractions](https://www.nuget.org/packages/App.Metrics.Abstractions/) nuget package:

```console
nuget install App.Metrics.Abstractions
```

<i class="fa fa-hand-o-right"></i> Then implement your custom formatter by implementing `IMetricsOutputFormatter`: 

```csharp
public class CustomOutputFormatter : IMetricsOutputFormatter
{
    public MetricsMediaTypeValue MediaType => new MetricsMediaTypeValue("text", "vnd.custom.metrics", "v1", "plain");

    public Task WriteAsync(Stream output,
        MetricsDataValueSource snapshot,
        CancellationToken cancellationToken = default)
    {
        // TODO: Serialize the snapshot

        return Task.CompletedTask;
    }
}
```

{{% notice note %}}
The `MetricsMediaTypeValue` allows content negotiation in a web context, e.g. when using the `/metrics`.
{{% /notice %}}

The custom formatter can made available on the `IMetricsRoot` using the `MetricsBuilder`:

```csharp
var metrics = new MetricsBuilder()
    .OutputMetrics.Using<CustomOutputFormatter>()
    // OR
    // .OutputMetrics.Using(new CustomOutputFormatter())
    .Builder();
```

{{% notice tip %}}
Formatters can be configured so that they can be used with the endpoints provided by the [App.Metrics.AspNetCore.Endpoints](https://www.nuget.org/packages/App.Metrics.AspNetCore.Endpoints/) nuget package. This allows for a pull based metrics approach rather than push via a reporter. See [Prometheus Reporting]({{< ref "reporting/reporters/prometheus.md#asp-net-core-configuration" >}}) for example.
{{% /notice %}}