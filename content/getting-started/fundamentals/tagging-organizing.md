---
title: "Tagging & Organizing"
draft: false
weight: 3
icon: "/images/tag.png"
---

App Metrics provides a two ways to organize your metrics:

1. Through the use of **Tags** when [defining your metrics]({{< ref "getting-started/fundamentals/recording-metrics.md#defining-metrics" >}}) or by [configuring global tags]({{< ref "getting-started/fundamentals/configuration.md" >}}) which apply for all Metrics.
1. By specifing a **Context** label when [defining your metrics]({{< ref "getting-started/fundamentals/recording-metrics.md#defining-metrics" >}}). By default all custom metrics will belong to the "Application" context.

## Metric Tagging

Metrics can be tagged when they are [defined]({{< ref "getting-started/fundamentals/recording-metrics.md#defining-metrics" >}}), these tags can then be shipped with your metric values to your database of choice which is useful for commonly-queried metadata.

A use case for tagging could be: recording an APIs response time per endpoint, where a timer is recorded per endpoint but reported as the same metric name e.g. "response_time", using an *endpoint* tag key with the route template as the tag value. This allows us to more easily have a dynamic list of endpoints and their response times when visualizing with Grafana for example.

### Tagging at runtime

Metrics can be tagged at runtime using `MetricTags` as follows:

```csharp
var tags = new MetricTags(new[] { "client_id", "route" }, new[] { clientId, routeTemplate });
metrics.Measure.Meter.Mark("throughput", tags);
```

### Tagging globally

Tags can also be defined for all metrics globally, which is useful for tagging by things like machine name, environment, ip address, application name etc. Global tags can be applied using the `MetricsBuilder`'s [configuration options]({{< ref "getting-started/fundamentals/configuration.md" >}}). You will also find extension methods on `MetricsOptions` for adding typical global tags, for example:

```csharp
var metrics = new MetricsBuilder()
    .Configuration.Configure(
        options =>
        {
            options.AddServerTag();
            options.GlobalTags.Add("myTagKey", "myTagValue");
        })
     ... // configure other options
    .Build();
```

App Metrics provides an extension method on `MetricsOptions` passing environment information that can be used to configure additional global tags.

```csharp
var metrics = new MetricsBuilder()
    .Configuration.Configure(
        options =>
        {
            options.WithGlobalTags((tags, envInfo) =>
            {
                 options.WithGlobalTags(
                    (tags, envInfo) =>
                    {
                        tags.Add("app_version", envInfo.EntryAssemblyVersion);
                    });
            });
        })
     ... // configure other options
    .Build();
```

#### Default global tags

When using the `AppMetrics.CreateDefaultBuilder()` static helper to configure metrics, a few default global tags are added which are typically used. These are as follows:

|Tag|Description|
|------|:--------|
|app|The name of the application, which is set to the entry assembly name of the application i.e. `Assembly.GetEntryAssembly().GetName().Name`.
|server|The name of the machine the application is currently running on i.e. `Environment.MachineName`.
|env|The environment the application is running in. When running in debug mode will be set to `debug` otherwise `release`, alternatively if the `ASPNETCORE_ENVIRONMENT` environment variable exists, it will be used as the the env tag value.

The static helper can be used as follows which builds an `IMetrics` with the default configuration.

```csharp
var metrics = AppMetrics.CreateDefaultBuilder()
     ... // configure other options or override specific defaults
    .Build();
```

## Metric Contexts

Organizing your metrics into separate meaningful contexts is helpful for visualization and reporting. Only one level of Context categorization is supported.

The default global context can be modified using the **DefaultContextLabel** in the [configuration options]({{< ref "getting-started/fundamentals/configuration.md" >}}) when configuring your application to use App Metrics.

### Defining a Context for Metrics

To add metrics to a particular context, the context can be specified when declaring your metrics. For example:

```csharp
public static class MyMetrics
{
    public static class ProcessMetrics
    {
        private static readonly string ContextName = "Process";

        public static GaugeOptions SystemNonPagedMemoryGauge = new GaugeOptions
        {
            Context = ContextName,
            Name = "System Non-Paged Memory",
            MeasurementUnit = Unit.Bytes
        };

        public static GaugeOptions ProcessVirtualMemorySizeGauge = new GaugeOptions
        {
            Context = ContextName,
            Name = "Process Virtual Memory Size",
            MeasurementUnit = Unit.Bytes
        };
    }

    public static class DatabaseMetrics
    {
        private static readonly string ContextName = "Database";

        public static TimerOptions SearchUsersSqlTimer = new TimerOptions
        {
            Context = ContextName,
            Name = "Search Users",
            MeasurementUnit = Unit.Calls
        };
    }
}
```

{{% notice tip %}}
Metrics can be [filtered]({{< ref "getting-started/filtering-metrics/_index.md" >}}) by Context which could be useful if only wanting to report a subset of metrics for example.
{{% /notice %}}
