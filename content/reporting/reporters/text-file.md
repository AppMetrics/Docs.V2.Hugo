---
title: "Text File"
date: 2017-09-28T22:34:33+10:00
draft: false
weight: 7
icon: "/images/txt.png"
---

The [App.Metrics.Reporting.TextFile](https://www.nuget.org/packages/App.Metrics.Reporting.TextFile/) nuget package writes metrics to a text file. The default output is plain text using [App.Metrics.Formatters.Ascii](https://www.nuget.org/packages/App.Metrics.Formatters.Ascii/) which can be substituted with any other [App Metrics Formatter]({{< ref "reporting/formatting/_index.md" >}}).

## Getting started

<i class="fa fa-hand-o-right"></i> To use the text file reporter, first install the [nuget package](https://www.nuget.org/packages/App.Metrics.Reporting.TextFile/):

```console
nuget install App.Metrics.Reporting.TextFile
```

<i class="fa fa-hand-o-right"></i> Then enable the reporter using `Report.ToTextFile()`:

```csharp
var metrics = new MetricsBuilder()
    .Report.ToTextFile()
    .Build();
```

{{% notice note %}}
If not output file name is specified, the default path is the application execution path and file name `metrics.txt`. The output path and file name can be specified as `ToTextFile(@"C:\metrics.txt")`
{{% /notice %}}

<i class="fa fa-hand-o-right"></i> App Metrics at the moment leaves report scheduling up the the user unless using [App.Metrics.AspNetCore.Reporting]({{< ref "web-monitoring/aspnet-core/reporting.md" >}}) . To run all configured reports use the `ReportRunner` on `IMetricsRoot`:

```csharp
await metrics.ReportRunner.RunAllAsync();
```

{{% notice info %}}
Report Scheduling will be added when [Microsoft.Extensions.Hosting.Background](https://github.com/aspnet/Hosting/blob/dev/src/Microsoft.Extensions.Hosting.Abstractions/BackgroundService.cs) is released, otherwise there are various other scheduling solutions available for C#. For ASP.NET Core reporting see details on the [App.Metrics.AspNetCore.Reporting]({{< ref "web-monitoring/aspnet-core/reporting.md" >}}) support package.
{{% /notice %}}

## Configuration

Configuration options are provided as a setup action used with `ToTextFile()`.

<i class="fa fa-hand-o-right"></i> To configure text file reporting options:

```csharp
var filter = new MetricsFilter().WhereType(MetricType.Timer);
var metrics = new MetricsBuilder()
    .Report.ToTextFile(
        options => {
            options.MetricsOutputFormatter = new MetricsJsonOutputFormatter();
            options.AppendMetricsToTextFile = true;
            options.Filter = filter;
            options.FlushInterval = TimeSpan.FromSeconds(20);
            options.OutputPathAndFileName = @"C:\metrics.txt";
        })
    .Build();
```

<i class="fa fa-hand-o-right"></i> The configuration options provided are:

|Option|Description|
|------|:--------|
|MetricsOutputFormatter|The formatter used when writing metrics to disk.
|AppendMetricsToTextFile|If `true` appends metrics to the output file, if `false` overrides the file context with the current metrics snapshot on each run.
|Filter|The filter used to filter metrics just for this reporter.
|FlushInterval|The delay between flushing metrics to disk.
|OutputPathAndFileName|The absolute path and file name where metrics are written.