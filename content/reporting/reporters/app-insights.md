---
title: "AppInsights"
draft: false
weight: 7
icon: "/images/cmd.png"
---

## Getting started
1. Install nuget package: [App.Metrics.Reporting.ApplicationInsights](https://www.nuget.org/packages/App.Metrics.Reporting.ApplicationInsights/)
2. Obtain Application Insights [instrumentation key](https://docs.microsoft.com/en-us/azure/azure-monitor/app/create-new-resource).
3. Configure App.Metrics like so:
```
using App.Metrics.Reporting.ApplicationInsights;

var instrumentationKey = "00000000-0000-0000-0000-000000000000";

var metrics = new MetricsBuilder()
    .Configuration.Configure(metricsOptions)
    .Report.ToApplicationInsights(instrumentationKey)
    .Build();
```

## Caveats
There are few caveats that come along by marriage of two frameworks which both do metric pre-aggragetion before sending it upstream using a reporter.<br/>
Also the interface for sending metric aggregates on Application Insight's `TelemetryClient` is lacking some features which are otherwise available (metric dimensions).

Read [here](https://github.com/jdvor/appmetrics-applicationinsights) about such issues and their solutions.
