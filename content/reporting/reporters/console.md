---
title: "Console"
date: 2017-09-28T22:34:27+10:00
draft: false
weight: 6
icon: "/images/cmd.png"
---

The [App.Metrics.Reporting.Console](https://www.nuget.org/packages/App.Metrics.Reporting.Console/) nuget package writes metrics to the Windows Console via standard output. The default output is plain text using [App.Metrics.Formatters.Ascii](https://www.nuget.org/packages/App.Metrics.Formatters.Ascii/) which can be substituted with any other [App Metrics Formatter]({{< ref "reporting/formatting/_index.md" >}}).

## Getting started

<i class="fa fa-hand-o-right"></i> To use the console reporter, first install the [nuget package](https://www.nuget.org/packages/App.Metrics.Reporting.Console/):

```console
nuget install App.Metrics.Reporting.Console
```

<i class="fa fa-hand-o-right"></i> Then enable the reporter using `Report.ToConsole()`:

```csharp
var metrics = new MetricsBuilder()
    .Report.ToConsole()
    .Build();
```

<i class="fa fa-hand-o-right"></i> App Metrics at the moment leaves report scheduling up the the user unless using [App.Metrics.AspNetCore]({{< ref "reporting/aspnet-core/_index.md#reporter" >}}). To run all configured reports use the `ReportRunner` on `IMetricsRoot`:

```csharp
await metrics.ReportRunner.RunAllAsync();
```

{{% notice info %}}
Report Scheduling will be added when [Microsoft.Extensions.Hosting.Background](https://github.com/aspnet/Hosting/blob/dev/src/Microsoft.Extensions.Hosting.Abstractions/BackgroundService.cs) is released, otherwise there are various other scheduling solutions available for C#.
{{% /notice %}}

## Configuration

Configuration options are provided as a setup action used with `ToConsole()`.

<i class="fa fa-hand-o-right"></i> To configure the console reporters options:

```csharp
var filter = new MetricsFilter().WhereType(MetricType.Timer);
var metrics = new MetricsBuilder()
    .Report.ToConsole(
        options => {
            options.FlushInterval = TimeSpan.FromSeconds(5);
            options.Filter = filter;
            options.MetricsOutputFormatter = new MetricsJsonOutputFormatter();
        })
    .Build();
```

<i class="fa fa-hand-o-right"></i> Configuration options provided are:

|Option|Description|
|------|:--------|
|MetricsOutputFormatter|The formatter used when writing metrics to `System.Console`.
|Filter|The filter used to filter metrics just for this reporter.
|FlushInterval|The delay between flushing metrics to `System.Console`.