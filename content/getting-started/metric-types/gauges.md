---
title: "Gauges"
draft: false
weight: 1
icon: "/images/gauge.png"
---

A Gauge is simply an action that returns the instantaneous measure of a value, where the value abitrarily increases and decreases, for example CPU usage.

Gauges are ideal to use for measured values such as current memory usage, cpu usage, temperatures, disk space etc.

### Using Gauges

```csharp
var processPhysicalMemoryGauge = new GaugeOptions
{
    Name = "Process Physical Memory",
    MeasurementUnit = Unit.Bytes
}

var process = Process.GetCurrentProcess();

_metrics.Measure.Gauge.SetValue(MetricsRegistry.Gauges.TestGauge, process.WorkingSet64);
```

Which for example when using the [JSON formatter](../intro.md#configuring-a-web-host) would result in something similar to: **TODO Link to json formatter**

```csharp
{
    "context": "Process",
    "gauges": [
        {
            "value": 51683328,
            "name": "Process Physical Memory",
            "unit": "bytes"
        }
    ]
}
```

### Derived Gauges

Derived Gauges allow you to derive a value from another Gauge and using a transformation to calculate the measurement.

```csharp
var process = Process.GetCurrentProcess();

var derivedGauge = new GaugeOptions
{
    Name = "Derived Gauge",
    MeasurementUnit = Unit.MegaBytes
};

var processPhysicalMemoryGauge = new GaugeOptions
{
    Name = "Process Physical Memory (MB)",
    MeasurementUnit = Unit.MegaBytes
};

var physicalMemoryGauge = new FunctionGauge(() => process.WorkingSet64);

_metrics.Measure.Gauge.SetValue(derivedGauge, () => new DerivedGauge(physicalMemoryGauge, g => g / 1024.0 / 1024.0));
```

### Ratio Gauges

Ratio Gauges allow you to measure a Gauge with a measurement which is the ratio between two values.

```csharp
var cacheHitRatioGauge = new GaugeOptions
{
    Name = "Cache Gauge",
    MeasurementUnit = Unit.Calls
};

var cacheHits = new MeterOptions
{
    Name = "Cache Hits Meter",
    MeasurementUnit = Unit.Calls
};

var databaseQueryTimer = new TimerOptions
{
    Name = "Database Query Timer",
    MeasurementUnit = Unit.Calls,
    DurationUnit = TimeUnit.Milliseconds,
    RateUnit = TimeUnit.Milliseconds
};

var cacheHits = _metrics.Provider.Meter.Instance(cacheHits);
var calls = _metrics.Provider.Timer.Instance(databaseQueryTimer);

var cacheHit = Rnd.Next(0, 2) == 0;

using (calls.NewContext())
{
    if (cacheHit)
    {
        cacheHits.Mark();
    }

    Thread.Sleep(cacheHit ? 10 : 100);
}

_metrics.Measure.Gauge.SetValue(cacheHitRatioGauge, () => new HitRatioGauge(cacheHits, calls, m => m.OneMinuteRate));
```