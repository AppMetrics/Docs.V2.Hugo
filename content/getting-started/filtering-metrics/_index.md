---
title: "Filtering Metrics"
draft: false
chapter: false
weight: 4
icon: "/images/filter.png"
---

App Metrics supports filtering metrics on various properties such as Metric Type, Tags, Context and Name. Filtering can be applied either globally, when retrieving a snapshot of metrics or for a specific reporter.

## Global Filter

A metrics filter can be applied on the `IMetricsBuilder` which will be applied by default when retrieving metric snapshots.

## Metrics Field Filter

[Metric types]({{< ref "getting-started/metric-types/_index.md" >}}) supported by App Metrics typically record several fields e.g. a Meter will record for example a 1-min, 5-min and 15-min rate. Such values can also be calculated using the chosen TSDB where metrics are flushed. In cases where metrics are frequently flushed and it is necessary to save on storage, metrics which aren't be used can be excluded:

```csharp
var metrics = new MetricsBuilder()
    .MetricFields.Configure(
        fields =>
        {
            fields.Counter.Exclude(CounterFields.Total, CounterFields.SetItem, CounterFields.SetItemPercent);
            fields.Meter.OnlyInclude(MeterFields.Rate1M);
            fields.Histogram.OnlyInclude(HistogramFields.P95, HistogramFields.P99);
        })
    .Build();
```

The `MetricsFields` extension also allows renaming the default metric names, for example to customise the default gauge and counter `value` fields:

```csharp
var metrics = new MetricsBuilder()
    .MetricFields.Configure(
        fields =>
        {
            fields.Counter.Set(CounterFields.Value, "custom_val");
            fields.Gauge.Set(GaugeFields.Value, "custom_val");
        })
    .Build();
```

<i class="fa fa-hand-o-right"></i> The following is an example of how to apply a global filter:

```csharp
var filter = new MetricsFilter()
    .WhereType(MetricType.Counter, MetricType.Gauge)
    .WhereContext("Application")
    .WhereNameStartsWith("test_");

var metrics = new MetricsBuilder()
    .Filter.With(filter)
    .Build();
```

{{% notice note %}}
When retrieving a metric snapshot, the global filter can be overriden by passing in a new filter: `metrics.Snapshot.Get(filter)`
{{% /notice %}}

## Report Filtering

App Metrics supports filtering metrics for specific reporters which could be useful to report a subset metrics to different sources.

<i class="fa fa-hand-o-right"></i> The following example reports all metrics in the *Console* context to `System.Console` and metrics in the *TextFile* context to a text file.

```csharp
var metrics = new MetricsBuilder()
    .Report.ToConsole(options => options.Filter = new MetricsFilter().WhereContext("Console"))
    .Report.ToTextFile(options => options.Filter = new MetricsFilter().WhereContext("TextFile"))
    .Build();
```

## Snapshot Filter

<i class="fa fa-hand-o-right"></i> Metrics can be filtered when retriving a snapshot via an `IMetrics` as follows:

```csharp
var filter = new MetricsFilter()
    .WhereName(name => name == "test_gauge");
var snapshot = metrics.Snapshot.Get(filter);
```

## Filtering Specifics

A default implementation of `IFilterMetrics` is provided to filter metrics, create an instance of `MetricsFilter`.

```csharp
var filter = new MetricsFilter();
```

#### Metric Context

Metrics can be grouped into *Contexts* which can be useful in [organizing metrics]({{< ref "getting-started/fundamentals/tagging-organizing.md#metric-contexts" >}}). 

<i class="fa fa-hand-o-right"></i> To filter by a specific context:

```csharp
filter.WhereContext("MyContext");
```

#### Metric Name

All metrics are required to be labelled with a *Name*.

<i class="fa fa-hand-o-right"></i> To filter a metric by it's name:

```csharp
filter.WhereName(metric => metric == "my_metric_name");
filter.WhereNameStartsWith("my_");
```

#### Metric Type

Several [metric types]({{< ref "getting-started/metric-types/_index.md" >}}) are supported by App Metrics.

<i class="fa fa-hand-o-right"></i> To filter by one or more types:

```csharp
filter.WhereType(MetricType.Timer);
filter.WhereType(MetricType.Counter, MetricType.Gauge);
```

#### Metric Tags

Metric tagging is very useful when reporting to a time series database allowing querying and aggregating by various dimensions straightforward. App Metrics supports filtering metrics on tags by matching tag key(s) and tag key/value pairs.

<i class="fa fa-hand-o-right"></i> To filter metrics by tags:

```csharp
filter.WhereTaggedWithKey("myTagKey1", "myTagKey2");
filter.WhereTaggedWithKeyValue(new TagKeyValueFilter { { "tag1", "value1" } });
```