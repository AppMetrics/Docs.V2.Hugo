---
title: "Meters"
date: 2017-09-28T23:47:16+10:00
draft: false
weight: 3
icon: "/images/meter.png"
---

A Meter measures the rate at which an event occurs along with a total count of the occurances. Rates that are measured are the *mean*, *one minute*, *five minute* and *fifteen minute*. The *mean* rate is an average rate of events for the lifetime of the application which does not provide a sense of recency, x-minute rates use an [Exponential Weighted Moving Average](https://en.wikipedia.org/wiki/Moving_average#Exponential moving average) (**EWMA**) for their calculations which do provide a sense of recency.

Meters provide the ability to track a count and percentage of each item within a set along with the rate of each item, for example if we were measuring the rate of HTTP requests made to an API protected with OAuth2, we could also track the rate at which each client was making these requests along with the number of requests and their percentage of the overall count within the same meter.

## Using Meters

```csharp
var cacheHitsMeter = new MeterOptions
{
    Name = "Cache Hits",
    MeasurementUnit = Unit.Calls
};

_metrics.Measure.Meter.Mark(cacheHitsMeter);
_metrics.Measure.Meter.Mark(cacheHitsMeter, 10);
```

And if we wanted to track the rate at which each HTTP status was occuring with an API:

```csharp
var httpStatusMeter = new MeterOptions
{
    Name = "Http Status",
    MeasurementUnit = Unit.Calls
};

_metrics.Measure.Meter.Mark(httpStatusMeter, "200");
_metrics.Measure.Meter.Mark(httpStatusMeter, "500");
_metrics.Measure.Meter.Mark(httpStatusMeter, "401");
```

{{% notice note %}}
When reporting metrics with counts we should keep in mind that they are a cumulative count, see notes in the [Counters]({{< ref "counters.md#reporting-counters" >}})  documentation. A Meters values can also be reset like a [Counters]({{< ref "counters.md" >}}) as shown below.
{{% /notice %}}

```csharp
_metrics.Provider.Meter.Instance(httpStatusMeter).Reset();
```