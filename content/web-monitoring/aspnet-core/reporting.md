---
title: "Reporting"
draft: false
chapter: false
weight: 5
icon: "/images/reporting.png"
---

The [App.Metrics.AspNetCore.Reporting](https://www.nuget.org/packages/App.Metrics.AspNetCore.Reporting/) nuget package provides functionality to schedule metrics reporting using one or more of the [metric reporters]({{< ref "reporting/reporters/_index.md" >}}) in an ASP.NET Core application.

App Metrics uses an [Microsoft.Extensions.Hosting.IHostedService](https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.hosting.ihostedservice?view=aspnetcore-2.0) implementation for scheduling reporters which is automatically configured if reporting is enabled when using the `UseMetrics` [Microsoft.Extensions.Hosting.IWebHostBuilder](https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.hosting.iwebhostbuilder?view=aspnetcore-2.0) extensions.

## How to use

`App.Metrics.AspNetCore.Reporting` can be configured a couple of ways in an ASP.NET Core application:

1. Using the `Microsoft.AspNetCore.WebHost` in a `Program.cs`.
1. Using the `Microsoft.AspNetCore.Builder.IApplicationBuilder` in a `Startup.cs`.

<i class="fa fa-hand-o-right"></i> First create a [new ASP.NET Core MVC project](https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-mvc-app/start-mvc).

### Bootstrapping Microsoft.AspNetCore.WebHost

This is a simpler approach as it will wire up the App Metrics feature middleware on the IApplicationBuilder, as well as the metrics infrastructure on the `IServiceCollection` for you.

<i class="fa fa-hand-o-right"></i> First install the [nuget package](https://www.nuget.org/packages/App.Metrics.AspNetCore/):

```console
nuget install App.Metrics.AspNetCore
```

<i class="fa fa-hand-o-right"></i> Then modify the `Program.cs` using the `UseMetrics` extension on `IWebHostBuilder` to configure all the App Metrics defaults including report scheduling, and also the `ConfigureMetricsWithDefaults` extension method to add the desired [metrics reporter(s)]({{< ref "reporting/reporters/_index.md" >}}).

```csharp
public static class Program
{
    public static IWebHost BuildWebHost(string[] args)
    {
        return WebHost.CreateDefaultBuilder(args)
            .ConfigureMetricsWithDefaults(
                builder =>
                {
                    builder.Report.ToConsole(TimeSpan.FromSeconds(2));
                    builder.Report.ToTextFile(@"C:\metrics.txt", TimeSpan.FromSeconds(20));
                })
            .UseMetrics()
            .UseStartup<Startup>()
            .Build();
    }

    public static void Main(string[] args) { BuildWebHost(args).Run(); }
}
```

### Bootstrapping Startup.cs

The [App.Metrics.AspNetCore](https://www.nuget.org/packages/App.Metrics.AspNetCore/) nuget packages is sort of a meta package which references other App Metrics ASP.NET core features. If it is preferred to cherry pick App Metrics ASP.NET Core functionality, feature packages can be referenced explicity instead, in this case we can configure App Metrics report scheduling using `IApplicationBuider` extensions in the `Startup.cs`.

<i class="fa fa-hand-o-right"></i> First install the [nuget package](https://www.nuget.org/packages/App.Metrics.AspNetCore.Reporting/):

```console
nuget install App.Metrics.AspNetCore.Reporting
```

{{% notice note %}}
The `App.Metrics.AspNetCore.Reporting` nuget package only references core ASP.NET Core App Metrics functionality rather than all available feature packages.
{{% /notice %}}

<i class="fa fa-hand-o-right"></i> Then modify the `Startup.cs` to add App Metrics and the metrics report scheduling feature using the `IServiceCollection` extensions:

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
                                    ... // configure a reporter
                                    .Build();

        services.AddMetrics(metrics);
        services.AddMetricsReportScheduler();

        services.AddMvc();
    }
}
```

{{% notice info %}}
The `App.Metrics.AspNetCore.Reporting` nuget package does not reference any metric reporter packages, install and configure one of the [available reporters]({{< ref "reporting/reporters/_index.md" >}}) in the *"configure a reporter"* comment in the previous code snippet.
{{% /notice %}}

{{% notice tip %}}
The configured metric reporter(s) will only be scheduled to run if both the `MetricsOptions.Enabled` and `MetricsOptions.ReportingEnabled` [configuration properties]({{< ref "getting-started/fundamentals/configuration.md" >}}) are set to `true`.
{{% /notice %}}