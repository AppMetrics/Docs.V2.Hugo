---
title: "Timers"
date: 2017-09-28T23:47:20+10:00
draft: false
weight: 5
icon: "/images/timer.png"
---

A Timer is a combination of a [Histogram]({{< relref "histograms.md" >}}) and a [Meter]({{< relref "meters.md" >}}) allowing us to measure the duration of a type of event, the rate of its occurrence and provide duration statistics. For example, the `App.Metrics.AspNetCore.Tracking` nuget package provides the ability to record a timer per endpoint.

## Using Timers

Timers can be recorded by either passing an action into the `Time` method or by using a `using` statement as show below. When a `using` statement is used the Timer will end recording upon disposing.

```csharp
var requestTimer = new TimerOptions
{
    Name = "Request Timer",
    MeasurementUnit = Unit.Requests,
    DurationUnit = TimeUnit.Milliseconds,
    RateUnit = TimeUnit.Milliseconds
};

_metrics.Measure.Timer.Time(requestTimer, () => PerformRequest());

// OR

using(_metrics.Measure.Timer.Time(requestTimer))
{
    PerformRequest();
}
```

Timers, like [Histogram]({{< relref "histograms.md" >}}) also allow us to track the *min*, *max* and *last value* that has been recorded in cases when a "user value" is provided when recording the timer. For example, when timing requests where the endpoint has couple of features flags implemented, we could track which feature flag as producing the min and max response times.

```csharp
using(_metrics.Measure.Timer.Time(requestTimer, "feature-1"))
{
    PerformRequest();
}
```

{{% notice note %}}
When reporting metrics with counts we should keep in mind that they are a cumulative count, see notes in the  [Counters]({{< relref "counters.md#reporting-counters" >}}) documentation. A Meters values can also be reset like a [Counters]({{< relref "counters.md" >}})  as shown below.
{{% /notice %}}

```csharp
_metrics.Provider.Timer.Instance(httpStatusMeter).Reset();
```

## Related Docs

- [Reservoir Sampling]({{< ref "getting-started/reservoir-sampling/_index.md" >}})
- [Histograms]({{< relref "histograms.md" >}})
- [Meters]({{< relref "meters.md" >}})