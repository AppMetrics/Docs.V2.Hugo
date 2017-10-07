---
title: "Configuration"
date: 2017-09-28T22:50:31+10:00
draft: false
weight: 6
icon: "/images/configuration.png"
---

Here you will find App Metrics configuration options related to integrating App Metrics in an ASP.NET Core application.

### App Metrics Core

#### Program.cs

When bootstrapping an ASP.NET Core application in `Program.cs` using the `Microsoft.AspNetCore.WebHost`, App Metrics core functionality can be configured using extension methods provided on [Microsoft.Extensions.Hosting.IWebHostBuilder](https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.hosting.iwebhostbuilder?view=aspnetcore-2.0).

<i class="fa fa-hand-o-right"></i> Modifying or extending a `MetricsBuilder` that is pre-configured with the defaults configuration:

{{%expand "view code snippet" %}}
```csharp
...

public static IWebHost BuidWebHost(string[] args)
{
    return WebHost.CreateDefaultBuilder(args)
        .ConfigureMetricsWithDefaults(
            builder =>
                {
                    builder.Configuration.Configure(
                        options =>
                        {
                            options.DefaultContextLabel = "Testing";
                            options.Enabled = false;
                        });
                })
        .UseMetrics()
        .UseStartup<Startup>()
        .Build();
}

...
```
{{% /expand%}}

<i class="fa fa-hand-o-right"></i> Using a `MetricsBuilder` where you define the desired configuration without defaults:

{{%expand "view code snippet" %}}
```csharp
...

public static IWebHost BuidWebHost(string[] args)
{
    return WebHost.CreateDefaultBuilder(args)
        .ConfigureMetrics(
            builder =>
                {
                    builder.Configuration.Configure(
                        options =>
                        {
                            options.DefaultContextLabel = "Testing";
                            options.Enabled = false;
                        });
                })
        .UseMetrics()
        .UseStartup<Startup>()
        .Build();
}

...
```
{{% /expand%}}

#### Startup.cs

Optionally, App Metrics core functionality can be configured in an ASP.NET Core application using the `Startup.cs` rather than bootstrapping on the `WebHost`. This is done by using the `MetricsBuilder` directly and the [Microsoft.Extensions.DependencyInjection.IServiceCollection](https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.iservicecollection?view=aspnetcore-2.0) extension methods provided by App Metrics.

<i class="fa fa-hand-o-right"></i> Modifying or extending a `MetricsBuilder` that is pre-configured with the defaults configuration:

{{%expand "view code snippet" %}}
```csharp
public class Startup
{
    public void Configure(IApplicationBuilder app)
    {
        app.UseMvc();
    }

    public void ConfigureServices(IServiceCollection services)
    {
        var metrics = AppMetrics.CreateDefaultBuilder()
                                ... // configure other options
                                .Build();

        services.AddMetrics(metrics);

        services.AddMvc(options => options.AddMetricsResourceFilter());
    }
}
```
{{% /expand%}}

<i class="fa fa-hand-o-right"></i> Using a `MetricsBuilder` where you define the desired configuration without defaults:

{{%expand "view code snippet" %}}
```csharp
public class Startup
{
    public void Configure(IApplicationBuilder app)
    {
        app.UseMvc();
    }

    public void ConfigureServices(IServiceCollection services)
    {
        var metrics = new MetricsBuilder()
                                ... // configure other options
                                .Build();

        services.AddMetrics(metrics);

        services.AddMvc(options => options.AddMetricsResourceFilter());
    }
}
```
{{% /expand%}}

{{% notice info %}}
For more details App Metrics core configuration options, refer to the [configuration fundamentals]({{< ref "getting-started/fundamentals/configuration.md" >}}) documentation.
{{% /notice %}}

### Tracking Middleware

For details on configuring [App.Metrics.AspNetCore.Tracking](https://www.nuget.org/packages/App.Metrics.AspNetCore.Tracking/), see the [ASP.NET Core Web Metrics]({{< ref "web-monitoring/aspnet-core/tracking-middleware.md#configuration ">}}) documentation.

### Endpoint Middleware

For details on configuring [App.Metrics.AspNetCore.Endpoints](https://www.nuget.org/packages/App.Metrics.AspNetCore.Endpoints/), see the [ASP.NET Core Endpoints]({{< ref "web-monitoring/aspnet-core/endpoint-middleware.md#configuration" >}}) documentation.