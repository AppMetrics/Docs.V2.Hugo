---
title: "Getting Started"
draft: false
chapter: false
pre: "<b>1. </b>"
weight: 1
icon: "/images/logo.png"
---

## Basics

App Metrics uses a simple C# fluent builder API to configure metrics. Core functionality is provided through an `IMetrics` interface. To build the interface use the `MetricsBuilder`. App Metrics core functionality is provided in the [App.Metrics](https://www.nuget.org/packages/App.Metrics/) nuget package.

```console
nuget install App.Metrics
```

```csharp
var metrics = new MetricsBuilder()
    ... // configure options
.Build();
```

### Recording Metrics

With an `IMetrics` instance we can record different types of metrics. The following example shows how to track a [Counter]({{< ref "getting-started/metric-types/counters.md" >}}) using the `metrics` instance created in the above example.

```csharp
var counter = new CounterOptions { Name = "my_counter" };
metrics.Measure.Counter.Increment(counter);
```

{{% notice info %}}
App Metrics includes various [metric types]({{< ref "getting-started/metric-types/_index.md" >}}) each providing their own usefulness depending on the measurement being tracked.
{{% /notice %}}

### Retrieving Metrics

The `IMetrics` interface allows us to retrieve a snapshot of recorded metrics.

```csharp
var snapshot = metrics.Snapshot.Get();
```

### Filtering Metrics

When retrieving a snapshot of currently recorded metrics, metrics can be filtered using the `MetricsFilter` which allows filtering by metric type, tags, name etc. To get a snapshot of all counters for example:

```csharp
var filter = new MetricsFilter().WhereType(MetricType.Counter);
var snapshot = Metrics.Snapshot.Get(filter);
```

{{% notice info %}}
See [filtering metrics]({{< ref "getting-started/filtering-metrics/_index.md" >}}) for more details on the metrics filtering available.
{{% /notice %}}

### Formatting Metrics

We can output metrics in different formats, by default a text formatter is applied. We can write the counter which was incremented in the previous example as follows:

```csharp
using (var stream = new MemoryStream())
{
    await metrics.DefaultOutputMetricsFormatter.WriteAsync(stream, snapshot);
    var result = Encoding.UTF8.GetString(stream.ToArray());
    System.Console.WriteLine(result);
}
```

{{% notice info %}}
See [formatting metrics]({{< ref "getting-started/formatting-metrics/_index.md" >}}) for more details on the metrics formatting.
{{% /notice %}}


### Environment Information

App Metrics also provides access to information of the current running environment such as the machine name, this environment information can be useful in tagging metrics.

```csharp
var env = metrics.EnvironmentInfo;
var tags = new MetricTags("server", env.MachineName);
metrics.Measure.Counter.Increment(counter, tags);
```

Similarly outputing metrics, we can output the current environment information as follows:

```csharp
using (var stream = new MemoryStream())
{
    await metrics.DefaultOutputEnvFormatter.WriteAsync(stream, metrics.EnvironmentInfo);
    var result = Encoding.UTF8.GetString(stream.ToArray());
    System.Console.WriteLine(result);
}
```

### Reporting Metrics

Metrics are generally reported periodically, App Metrics reporters are distributed via nuget, this example uses the [console reporter](https://www.nuget.org/packages/App.Metrics.Reporting.Console/) nuget package, which will allow reporting metrics in the default plain-text format to `System.Console`.

{{% notice info %}}
App Metrics supports a variety of [metric reporters]({{< ref "reporting/reporters/_index.md" >}}).
{{% /notice %}}

<i class="fa fa-hand-o-right"></i> To use the console reporter, first install the [nuget package](https://www.nuget.org/packages/App.Metrics.Reporting.Console/):

```console
nuget install App.Metrics.Reporting.Console
```

<i class="fa fa-hand-o-right"></i> Then to enable console reporting:

```csharp
var metrics = new MetricsBuilder()
    .Report.ToConsole()
    .Build();
```

The above will configure the console reporter but App Metrics will not schedule the reporting by default. The is to allow flexiblity in scheduling for different types of applications. App Metrics does however provide a task schedular which can be used to schedule the reporting of metrics via all configured reporters.

```csharp
var scheduler = new AppMetricsTaskScheduler(
    TimeSpan.FromSeconds(3),
    async () =>
    {
        await Task.WhenAll(metrics.ReportRunner.RunAllAsync());
    });
scheduler.Start();
```

{{% notice tip %}}
If integrating App Metrics in an [ASP.NET Core application]({{< ref "web-monitoring/aspnet-core/reporting.md" >}}), install the [App.Metrics.AspNetCore.Reporting](https://www.nuget.org/packages/App.Metrics.AspNetCore.Reporting/) nuget package which schdules reporting via an `Microsoft.Extensions.Hosting.IHostedService` implementation.
{{% /notice %}}

## Configuring a Console Application

<i class="fa fa-hand-o-right"></i> Create a new dotnet core console application and install the [App.Metrics](https://www.nuget.org/packages/App.Metrics/) and [App.Metrics.Reporting.Console](https://www.nuget.org/packages/App.Metrics.Reporting.Console/) nuget packages.

```console
nuget install App.Metrics
nuget install App.Metrics.Reporting.Console
```

<i class="fa fa-hand-o-right"></i> Modify your `Program.cs` with the following:

```csharp
public static class Program
{
    public static async Task Main()
    {
        var metrics = new MetricsBuilder()
            .Report.ToConsole()
            .Build();

        var counter = new CounterOptions { Name = "my_counter" };
        metrics.Measure.Counter.Increment(counter);

        await Task.WhenAll(metrics.ReportRunner.RunAllAsync());

        System.Console.ReadKey();
    }
}
```

{{% notice tip %}}
In your `.csproj` add `<LangVersion>latest</LangVersion>` to allow the `async Task Main` method.
{{% /notice %}}