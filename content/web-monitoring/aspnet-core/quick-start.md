---
title: "Quick Start"
draft: false
weight: 1
icon: "/images/quickstart.png"
---

This quick start guide assumes creating an ASP.NET Core MVC application, however a dependency on MVC is not required.

## Getting started

<i class="fa fa-hand-o-right"></i> Create a [new ASP.NET Core MVC project](https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-mvc-app/start-mvc).

<i class="fa fa-hand-o-right"></i> Then install the [App.Metrics.AspNetCore.Mvc](https://www.nuget.org/packages/App.Metrics.AspNetCore.Mvc/) nuget package:

```console
nuget install App.Metrics.AspNetCore.Mvc
```

### Bootstrapping

<i class="fa fa-hand-o-right"></i> Modify the `Program.cs` to apply the App Metrics ASP.NET Core defaults:

```csharp
public static class Program
{
	public static IWebHost BuildWebHost(string[] args)
	{
		return WebHost.CreateDefaultBuilder(args)
				.UseMetrics()
				.UseStartup<Startup>()
				.Build();
	}

	public static void Main(string[] args) { BuildWebHost(args).Run(); }
}
```

### Configuration

<i class="fa fa-hand-o-right"></i> *Optionally* add the following configuration to your `appsettings.json`

```json
{
  "MetricsOptions": {
    "DefaultContextLabel": "MyMvcApplication",
    "Enabled": true
  },
  "MetricsWebTrackingOptions": {
    "ApdexTrackingEnabled": true,
    "ApdexTSeconds": 0.1,
    "IgnoredHttpStatusCodes": [ 404 ],
    "IgnoredRoutesRegexPatterns": [],
    "OAuth2TrackingEnabled": true
  },
  "MetricEndpointsOptions": {
    "MetricsEndpointEnabled": true,
    "MetricsTextEndpointEnabled": true,
    "PingEndpointEnabled": true,
    "EnvironmentInfoEndpointEnabled": true
  }
}
```

<i class="fa fa-hand-o-right"></i> Modify the `Startup.cs` allowing App Metrics to inspect MVC routes by using the  `MvcOptions` extensions method `AddMetricsResourceFilter()`:

```csharp
public class Startup
{
	public void Configure(IApplicationBuilder app)
	{
		app.UseMvc();
	}

	public void ConfigureServices(IServiceCollection services)
	{
		services.AddMvc(options => options.AddMetricsResourceFilter());
	}
}
```

{{% notice info %}}
The `AddMetricsResourceFilter` extension method on `MvcOptions` is required to allow App Metrics to inspect MVC route templates to tag metrics.
{{% /notice %}}

### Testing it out

<i class="fa fa-hand-o-right"></i> Run your web application and request the following urls:

|Endpoint|Description|
|---------|:--------|
|`/metrics`|Exposes a metrics snapshot using the configured metrics formatter.
|`/metrics-text`|Exposes a metrics snapshot using the configured text formatter.
|`/env`|Exposes environment information about the application e.g. OS, Machine Name, Assembly Name, Assembly Version etc.

## What's next

- With your web application configured, choose and configure one of the [reporters]({{< ref "reporting/reporters/_index.md" >}}).
- Now you have your metrics persisted, check out the [Grafana Dashboards]({{< ref "web-monitoring/aspnet-core/grafana.md" >}}) provided.
- Start tracking your own [metrics]({{< ref "getting-started/metric-types/_index.md" >}}).