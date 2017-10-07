---
title: "Defining & Recording"
date: 2017-09-27T21:17:43+10:00
draft: false
weight: 2
icon: "/images/record-metrics.png"
---

## Defining Metrics

App Metrics provides an `IMetrics` used to record application metrics. An instance of `IMetrics` can be built using the `MetricsBuilder` and is intended to be used as single instance.

Each metric being measured is defined via one the available [metric types]({{< ref "getting-started/metric-types/_index.md" >}}) and only needs to be defined once using a static class.

<i class="fa fa-hand-o-right"></i> The following is an example of how you could define your custom metrics:

```csharp
public static class MyMetricsRegistry
{
    public static GaugeOptions Errors => new GaugeOptions
    {
        Name = "Errors"
    };

    public static CounterOptions SampleCounter => new CounterOptions
    {
        Name = "Sample Counter",
        MeasurementUnit = Unit.Calls,
    };

    public static HistogramOptions SampleHistogram => new HistogramOptions
    {
        Name = "Sample Histogram",
        Reservoir = () => new DefaultAlgorithmRReservoir(),
        MeasurementUnit = Unit.MegaBytes
    };

    public static MeterOptions SampleMeter => new MeterOptions
    {
        Name = "Sample Meter",
        MeasurementUnit = Unit.Calls
    };

    public static TimerOptions SampleTimer => new TimerOptions
    {
        Name = "Sample Timer",
        MeasurementUnit = Unit.Items,
        DurationUnit = TimeUnit.Milliseconds,
        RateUnit = TimeUnit.Milliseconds,
        Reservoir = () => new DefaultForwardDecayingReservoir(sampleSize: 1028, alpha: 0.015)
    };

    public static ApdexOptions SampleApdex => new ApdexOptions
    {
        Name = "Sample Apdex"
    };
}
```

{{% notice info %}}
`Name` is the only *required* property when defining a metric.
{{% /notice %}}

## Recording Metrics

<i class="fa fa-hand-o-right"></i> Metrics are recorded via the `Measure` property of an `IMetrics` instance. The simpliest way to create an instance of `IMetrics` is as follows:

```csharp
var metrics = AppMetrics.CreateDefaultBuilder().Build();
```

<i class="fa fa-hand-o-right"></i> Then with the `IMetrics` instance we can record values on each of our defined metrics:

```csharp
metrics.Measure.Counter.Increment(MyMetricsRegistry.SampleCounter);

metrics.Measure.Gauge.SetValue(MyMetricsRegistry.Errors, 1);

metrics.Measure.Histogram.Update(MyMetricsRegistry.SampleHistogram, 1);

metrics.Measure.Meter.Mark(MyMetricsRegistry.SampleMeter, 1);

using (metrics.Measure.Timer.Time(MyMetricsRegistry.SampleTimer))
{
    // Do something
}

using (metrics.Measure.Apdex.Track(MyMetricsRegistry.SampleApdex))
{
    // Do something
}
```

## Next Steps

- [Metric Types]({{< ref "getting-started/metric-types/_index.md" >}})
- [Configuration Options]({{< ref "getting-started/fundamentals/configuration.md" >}})