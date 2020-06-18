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
using App.Metrics.AspNetCore;

public static class Program
{
	public static IHost BuildHost(string[] args)
	{
		return Host.CreateDefaultBuilder(args)
				.UseMetrics()
				.ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseStartup<Startup>();
        });
				.Build();
	}

	public static void Main(string[] args) { BuildHost(args).Run(); }
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
    "EnvironmentInfoEndpointEnabled": true
  }
}
```

<i class="fa fa-hand-o-right"></i> Modify the `Startup.cs` allowing App Metrics to inspect MVC routes by using the  `IMvcBuilder` extension method `AddMetrics()`. 

This adds an `Microsoft.AspNetCore.Mvc.Filters.IAsyncResourceFilter` implementation to the `Microsoft.AspNetCore.Mvc.MvcOptions` `FilterCollection` allowing metrics tracked by App Metrics Middleware to be tagged with the route template of each endpoint.

```csharp
public class Startup
{
	public void Configure(IApplicationBuilder app)
	{
		app.UseMvc();
	}

	public void ConfigureServices(IServiceCollection services)
	{
		services.AddMvcCore().AddMetricsCore();
	}
}
```

{{% notice info %}}
The `AddMetrics()` extension method on `AddMvc()` is required to allow App Metrics to inspect MVC route templates to tag metrics.
{{% /notice %}}

{{% notice info %}}
The route template used to tag metrics can be customised by using a custom `App.Metrics.AspNetCore.IRouteNameResolver'. The custom implementation can be applied as shown in the snippet below:

```csharp
services.AddMvc(options => options.Filters.Add(new MetricsResourceFilter(new MyCustomMetricsRouteNameResolver())));
```

{{% /notice %}}

{{% notice tip %}}
To tag your own metrics with the route template provided by App Metrics, the route template can be accessed via an extension method on the HttpContext. See the snippet below as an example:
{{% /notice %}}

```csharp
var routeTemplate = httpContext.GetMetricsCurrentRouteName();
var tags = new MetricTags("route", routeTemplate);
_metrics.Measure.Meter.Mark(errorRateOptions, tags);
```

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
