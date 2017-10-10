---
title: "Histograms"
draft: false
weight: 4
icon: "/images/histogram.png"
---

Histograms measure the statistical distribution of a set of values including the *min*, *max*, *mean*, *median*, *standard deviation* and [quantiles](https://en.wikipedia.org/wiki/Quantile) i.e. the *75th percentile*, *90th percentile*, *95th percentile*, *99th percentile* and *99.9th percentile* allowing us to sample observations for things like response sizes.

A use case for a Histogram could be tracking the POST and PUT requests sizes made to a web service or tracking the file sizes uploaded to an endpoint.

## Using Histograms

```csharp
var postAndPutRequestSize = new HistogramOptions
{
    Name = "Web Request Post & Put Size",
    MeasurementUnit = Unit.Bytes
};

public async Task Invoke(HttpContext context)
{
    var httpMethod = context.Request.Method.ToUpperInvariant();

    if (httpMethod == "POST" || httpMethod == "PUT")
    {
        if (context.Request.Headers != null && context.Request.Headers.ContainsKey("Content-Length"))
        {            
            _metrics.Measure.Histogram.Update(long.Parse(context.Request.Headers["Content-Length"].First()));
        }
    }

    await Next(context);
}
```

Histograms also allow us to track the *min*, *max* and *last value* that has been recorded in cases when a "user value" is provided when updating the histogram. For example if we wanted to measure the statistical distribution of files uploaded to a file service and track which applications are uploading the largest files, we could use an applications client id when updating the histgoram:

```csharp
var fileSize = new HistogramOptions
{
    Name = "Document File Size",
    MeasurementUnit = Unit.Calls
};

_metrics.Measure.Histogram.Update(httpStatusMeter, CalculateDocumentSize(), "client_1");
_metrics.Measure.Histogram.Update(httpStatusMeter, CalculateDocumentSize(), "client_2");
```

## Reservoir Sampling

In high performance applications it is not possible to keep the entire data stream of a histogram in memory. To work around this [reservoir sampling]({{< ref "getting-started/reservoir-sampling/_index.md" >}}) algorithms allow us to maintain a small, manageable reservoir which is statistically representative of an entire data stream.

Out-of-box App.Metrics supports three types of [reservoir sampling]({{< ref "getting-started/reservoir-sampling/_index.md" >}}), Uniform, Exponentially Decaying and Sliding Window. By default all metrics which require sampling will use an Exponentially Decaying Reservoir.

It is possible to change the reservoir both [globally]({{< ref "getting-started/reservoir-sampling/_index.md#configuration" >}}) for all metrics and for an individual metric:

```csharp
var postRequestSizeHistogram = new HistogramOptions
{
    Context = ContextName,
    Name = "POST Size",
    MeasurementUnit = Unit.Bytes,
    Reservoir = () => new DefaultSlidingWindowReservoir(sampleSize: 1028)
};
```

{{% notice note %}}
You can also implement custom Reservoirs by implementing *IReservoir*.
{{% /notice %}}

## Related Docs

- [Reservoir Sampling]({{< ref "getting-started/reservoir-sampling/_index.md" >}})