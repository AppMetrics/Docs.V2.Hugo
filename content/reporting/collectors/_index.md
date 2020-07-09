---
title: "Collectors"
draft: false
---

## System Usage

App Metrics includes a hosted service which collects system usage metrics. Metrics include:
- Total CPU Percentage Used
- Privileged CPU Percentage Used
- User CPU Percentage Used
- Memory Working Set
- Non Paged System Memory
- Paged Memory
- Paged System Memory
- Private Memory
- Virtual Memory

### How to configure

For console applications:

```console
nuget install App.Metrics.App.All
```

For ASP.NET applications:

```console
nuget install App.Metrics.AspNetCore.All
```

Use the `IServiceCollection` extension method to add the hosted service e.g.

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddAppMetricsSystemMetricsCollector();
}
```

## GC Events

App Metrics includes a hosted service which collects GC metrics. Metrics include:
- Gen 0 Collections
- Gen 1 Collections
- Gen 2 Collections
- Live Objects Size
- Gen 0 Heap Size
- Gen 1 Heap Size
- Gen 2 Heap Size
- Large Object Heap Size
- Bytes Promoted From Gen 0
- Bytes Promoted From Gen 1
- Bytes Survived From Gen 2
- Bytes Survived Large Object Heap

### How to configure

For console applications:

```console
nuget install App.Metrics.App.All
```

For ASP.NET applications:

```console
nuget install App.Metrics.AspNetCore.All
```

Use the `IServiceCollection` extension method to add the hosted service e.g.

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddAppMetricsGcEventsMetricsCollector();
}
```

## Register all available collectors

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddAppMetricsCollectors();
}
```

### Dashboard

An InfluxDB [Grafana dashboard](https://grafana.com/grafana/dashboards/12616) exists for an example on visualisation of captured metrics;
