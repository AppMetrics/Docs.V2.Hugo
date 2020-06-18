---
title: "Reporting"
draft: false
chapter: false
weight: 5
icon: "/images/reporting.png"
---

The [App.Metrics.Extensions.Hosting](https://www.nuget.org/packages/App.Metrics.Extensions.Hosting/) nuget package provides functionality to schedule metrics reporting using one or more of the [metric reporters]({{< ref "reporting/reporters/_index.md" >}}) in an ASP.NET Core application.

App Metrics uses a [Microsoft.Extensions.Hosting.IHostedService](https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.hosting.ihostedservice?view=aspnetcore-2.0) implementation for scheduling reporters which is automatically configured if reporting is enabled when using the `UseMetrics` [Microsoft.Extensions.Hosting.IHostBuilder](https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.hosting.ihostbuilder?view=dotnet-plat-ext-3.1) extensions.

## How to use

Reporting can be configured a few of ways in an ASP.NET Core application:

1. Using the legacy `Microsoft.AspNetCore.WebHost` in a `Program.cs`.
1. Using the `Microsoft.Extensions.Hosting.Host` in a `Program.cs`.
1. Using the `Microsoft.AspNetCore.Builder.IApplicationBuilder` in a `Startup.cs`.

<i class="fa fa-hand-o-right"></i> First create a [new ASP.NET Core MVC project](https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-mvc-app/start-mvc).

### Bootstrapping Microsoft.AspNetCore.WebHost

This is a simpler approach as it will wire up the App Metrics feature middleware on the IApplicationBuilder, as well as the metrics infrastructure on the `IServiceCollection` for you.

<i class="fa fa-hand-o-right"></i> First install the [nuget package](https://www.nuget.org/packages/App.Metrics.AspNetCore.All/):

```console
nuget install App.Metrics.AspNetCore.All
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

### Bootstrapping Microsoft.Extensions.Hosting.Host

This is a simpler approach as it will wire up the App Metrics feature middleware on the IApplicationBuilder, as well as the metrics infrastructure on the `IServiceCollection` for you.

<i class="fa fa-hand-o-right"></i> First install the [nuget package](https://www.nuget.org/packages/App.Metrics.AspNetCore.All/):

```console
nuget install App.Metrics.AspNetCore.All
```

<i class="fa fa-hand-o-right"></i> Then modify the `Program.cs` using the `UseMetrics` extension on `IHostBuilder` to configure all the App Metrics defaults including report scheduling, and also the `ConfigureMetricsWithDefaults` extension method to add the desired [metrics reporter(s)]({{< ref "reporting/reporters/_index.md" >}}).

```csharp
public static class Program
{
    public static IHost BuildHost(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureMetricsWithDefaults(
                builder =>
                {
                    builder.Report.ToConsole(TimeSpan.FromSeconds(2));
                    builder.Report.ToTextFile(@"C:\metrics.txt", TimeSpan.FromSeconds(20));
                })
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

{{% notice tip %}}
The same approach can be used for non-web applications using the Hosting building via the [App.Metrics.App.All](https://www.nuget.org/packages/App.Metrics.App.All/) nuget package.
{{% /notice %}}

### Bootstrapping Startup.cs

The [App.Metrics.AspNetCore.All](https://www.nuget.org/packages/App.Metrics.AspNetCore.All/) nuget packages is a metapackage which references all App Metrics ASP.NET core features. If it is preferred to cherry pick App Metrics ASP.NET Core functionality, feature packages can be referenced explicity instead, in this case we can configure App Metrics report scheduling using `IApplicationBuider` extensions in the `Startup.cs`.

<i class="fa fa-hand-o-right"></i> First install the [nuget package](https://www.nuget.org/packages/App.Metrics.AspNetCore.All/):

```console
nuget install App.Metrics.AspNetCore.All
```

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
        services.AddMetricsReportingHostedService();

        services.AddMvc().AddMetrics();
    }
}
```

{{% notice info %}}
The `App.Metrics.AspNetCore.All` nuget package does not reference any metric reporter packages, install and configure one of the [available reporters]({{< ref "reporting/reporters/_index.md" >}}) in the *"configure a reporter"* comment in the previous code snippet.
{{% /notice %}}

{{% notice tip %}}
The configured metric reporter(s) will only be scheduled to run if both the `MetricsOptions.Enabled` and `MetricsOptions.ReportingEnabled` [configuration properties]({{< ref "getting-started/fundamentals/configuration.md" >}}) are set to `true`.
{{% /notice %}}
