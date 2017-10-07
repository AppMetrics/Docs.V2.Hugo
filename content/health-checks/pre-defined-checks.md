---
title: "Pre-defined Health Checks"
date: 2017-09-28T22:21:34+10:00
draft: false
chapter: false
weight: 3
icon: "/images/health.png"
---

## Predefined Health Checks

App Metrics includes various pre-defined health checks which can be registered using the `HealthBuilder`.

```console
nuget install App.Metrics.Health
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
    // Check connectivity to github by ensuring the GET request results in `IsSuccessStatusCode`
    .HealthChecks.AddHttpGetCheck("github", new Uri("https://github.com/"), TimeSpan.FromSeconds(10));
```