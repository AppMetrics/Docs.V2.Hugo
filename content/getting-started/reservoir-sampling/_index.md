---
title: "Reservoir Sampling"
draft: false
chapter: false
weight: 5
icon: "/images/sampling.png"
---

Histograms measure the statistical distribution of a set of values. In high performance applications it is not possible to keep the entire data stream of a histogram in memory. To work around this reservoir sampling algorithms allow us to maintain a small, manageable reservoir which is statistically representative of an entire data stream.

[Reservoir sampling](https://en.wikipedia.org/wiki/Reservoir_sampling) is a family of randomized algorithms for randomly choosing a sample of `k` items from a list `S` containing `n` items, where `n` is either a very large or unknown number. Typically `n` is large enough that it cannot be stored in memory. This type of sampling allows us to measure descriptive statisitcs including the *min*, *max*, *mean*, *median*, *standard deviation* and [quantiles](https://en.wikipedia.org/wiki/Quantile) i.e. the *75th percentile*, *90th percentile*, *95th percentile*, *99th percentile* and *99.9th percentile* on a stream of data.

App Metrics supports four types of sampling such data streams:

## Uniform Reservoir Sampling

A histogram with a uniform reservoir produces [quantiles](https://en.wikipedia.org/wiki/Quantile) which are valid for the entirely of the histogram’s lifetime.

This sampling reservoir can be used when you are interested in long-term measurements, it does not offer a sense of recency over the stream of data being measured. Use when you are interested in all the data produced as opposed to a time window snapshot.

The default sample size is 1028.

App Metrics uses [Algorithm R](http://www.cs.umd.edu/~samir/498/vitter.pdf) for uniform reservoir sampling.

## Exponentially Decaying Reservoir Sampling

A histogram with an forward decaying reservoir produces [quantiles](https://en.wikipedia.org/wiki/Quantile) which are representative of approximately the last five minutes of data, providing a sense of recency unlike Uniform Reservoir Sampling.

This sampling reservoir can be used when you are interested in recent changes to the distribution of data rather than a median on the lifetime of the histgram.

The default sample size of 1028 and alpha value of 0.015, offers a 99.9% confidence level with a 5% margin of error assuming a normal distribution and heavily biases the reservoir to the past 5 mins of measurements. The higher the alpha, the more biased the reservoir will be towards newer values.  The behavior of the reservoir can further be adjusted with the minimum sample weight parameter. If set to non-zero value, older samples with low weights will be removed from the reservoir when the reservoir is preiodically rescaled. This improves reported statistics when periods of very low or no activity alternate with periods of high activity.

App Metrics uses a [forward-decaying](http://dimacs.rutgers.edu/~graham/pubs/papers/fwddecay.pdf) reservoir with an exponential weighting towards recent samples.

## Sliding Window Reservoir Sampling

A Reservoir implementation backed by a [fixed-size sliding window](http://web.cs.ucla.edu/~rafail/PUBLIC/100.pdf) that stores only the measurements made in the last N data entries (or other time unit) and therefore like an Exponentially Decaying Reservoir provides a sense of recency. Statistical descritption with the type of reservoir are deterministic, so there is no danger that unfortunate random selections will produce bad approximations.

The default sample size is 1028.

## Configuration

The default sampling type used by App Metrics is Exponentially Decaying. This can be adjusted using the `MetricsBuilder` or on a per metrics basis as demonstrated with [Histograms]({{< ref "getting-started/metric-types/histograms.md#set-the-reservoir-for-a-specific-metric" >}}) for example. To modify the reservoir used by default for all metrics, the `SampleWith` extension can be used:

```csharp
var builder = AppMetrics.CreateDefaultBuilder()
    .SampleWith.ForwardDecaying()
    .SampleWith.AlgorithmR()
    .SampleWith.SlidingWindow()
    .SampleWith.Reservoir<CustomReservoir>(); // To provide a custom implementation
```

{{% notice note %}}
In the above we can of course only choose a single default, if mulitple reservoir implementations are configured, the last implementation will be used as the default.
{{% /notice %}}

{{% notice info %}}
Original Implementation Reservoir Sampling was originally implemented by Iulian Margarintescu in the [Metrics.NET](https://github.com/etishor/Metrics.NET/tree/master/Src/Metrics/Sampling) library.
{{% /notice %}}