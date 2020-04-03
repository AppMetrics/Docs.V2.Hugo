---
title: "Pre-defined Health Checks"
draft: false
chapter: false
weight: 3
icon: "/images/health.png"
---

## Predefined Health Checks

{{% notice warning %}}
Health Checks have now been retired given Microsoft have an equivalent implementation.
{{% /notice %}}

App Metrics includes various pre-defined health checks which can be registered using the `HealthBuilder`.

```console
nuget install App.Metrics.Health
nuget install App.Metrics.Health.Checks.Http
nuget install App.Metrics.Health.Checks.Network
nuget install App.Metrics.Health.Checks.Process
nuget install App.Metrics.Health.Checks.Sql
```

```csharp
var healthBuilder = new HealthBuilder()
    // Check that the current amount of private memory in bytes is below a threshold
    .HealthChecks.AddProcessPrivateMemorySizeCheck("Private Memory Size", threshold)
    // Check that the current amount of virtual memory in bytes is below a threshold
    .HealthChecks.AddProcessVirtualMemorySizeCheck("Virtual Memory Size", threshold)
    // Check that the current amount of physical memory in bytes is below a threshold
    .HealthChecks.AddProcessPhysicalMemoryCheck("Working Set", threshold)
    // Check connectivity to google with a "ping", passes if the result is `IPStatus.Success`
    .HealthChecks.AddPingCheck("google ping", "google.com", TimeSpan.FromSeconds(10))
    // Check SQL connectivity, taking a Func<IDbConnection>, with a 10sec timeout and 1 minute caching of the result
    .HealthChecks.AddSqlCachedCheck("DB Connection Cached", () => new SqliteConnection(ConnectionString),
                                          TimeSpan.FromSeconds(10),
                                          TimeSpan.FromMinutes(1),
    // Check connectivity to github by ensuring the GET request results in `IsSuccessStatusCode`
    .HealthChecks.AddHttpGetCheck("github", new Uri("https://github.com/"), TimeSpan.FromSeconds(10))
    // Check connectivity to github by ensuring the GET request results in `IsSuccessStatusCode`, this time performing 3 retries with a 100ms delay between failed requests
    .HealthChecks.AddHttpGetCheck("github", new Uri("https://github.com/"), retries: 3, delayBetweenRetries: TimeSpan.FromMilliseconds(100), TimeSpan.FromSeconds(10));
```
