---
title: "Microsoft HealthChecks"
draft: false
weight: 5
icon: "/images/health.png"
---

The [App.Metrics.Extensions.HealthChecks](https://www.nuget.org/packages/App.Metrics.Extensions.HealthChecks/) nuget package records AspNetCore health check results as metrics allowing results to be flushed to a supported TSDB via one of the App Metrics metrics [available reporters]({{< ref "reporting/reporters/_index.md" >}}).


## How to use

With App Metrics configured in either an AspNetCore application or dotnet core application enable the `Microsoft.Extensions.Diagnostics.HealthChecks.IHealthCheckPublisher` implementation to record health check results as metrics on the `IServiceCollection`

```csharp
services.AddAppMetricsHealthPublishing();
```