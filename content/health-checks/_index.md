---
title: "Health Checks"
draft: false
chapter: false
weight: 2
pre: "<b>2. </b>"
icon: "/images/health.png"
---

Health Checks give you the ability to monitor the health of your application by writing a small tests which returns either a **healthy**, **degraded** or **unhealthy** result. This is useful not only to test the internal health of your application but also it's external dependencies such as an third party api which your application relies on to function correctly.

## Basics

App Metrics Health uses a simple C# fluent builder API to configure health checks. Configuration options are provided by the `HealthBuilder`. App Metrics Health functionality is provided by the [App.Metrics.Health](https://www.nuget.org/packages/App.Metrics.Health/) nuget package.

<i class="fa fa-hand-o-right"></i> First install the [nuget package](https://www.nuget.org/packages/App.Metrics.Health/):

```console
nuget install App.Metrics.Health
```

<i class="fa fa-hand-o-right"></i> Then use the `HealthBuilder` to configure:

```csharp
var healthBuilder = new HealthBuilder();
```

### Defining Health Checks

Health checks can be defined by implementing a class which inherits `HealthCheck` or by implementing them in-line use the `HealthBuilder`.

<i class="fa fa-hand-o-right"></i> Define a health check using a C# class:

```csharp
public class SampleHealthCheck : HealthCheck
{
    public SampleHealthCheck()
        : base("Sample Health Check") { }

    protected override ValueTask<HealthCheckResult> CheckAsync(CancellationToken cancellationToken = default)
    {
        if (DateTime.UtcNow.Second <= 20)
        {
            return new ValueTask<HealthCheckResult>(HealthCheckResult.Degraded());
        }

        if (DateTime.UtcNow.Second >= 40)
        {
            return new ValueTask<HealthCheckResult>(HealthCheckResult.Unhealthy());
        }

        return new ValueTask<HealthCheckResult>(HealthCheckResult.Healthy());
    }
}
```

<i class="fa fa-hand-o-right"></i> Register the health check with the `HealthBuilder`:

```csharp
healthBuilder.HealthChecks.AddCheck(new SampleHealthCheck());
```

{{% notice tip %}}
If your application is using `Microsoft.Extensions.DependencyInjection`, install the [App.Metrics.Health.Extensions.DependencyInjection](https://www.nuget.org/packages/App.Metrics.Health.Extensions.DependencyInjection/) nuget package which can be used to automatically scan and register any class inheriting `HealthCheck`.
{{% /notice %}}

<i class="fa fa-hand-o-right"></i> Define a second health check in-line using the `HealthBuilder`:

```csharp
healthBuider.HealthChecks.AddCheck("DatabaseConnected", 
    () => new ValueTask<HealthCheckResult>(HealthCheckResult.Healthy("Database Connection OK")));
```

### Retrieving a Health Status

After having registered your health checks, an `IHealthRoot` can be built using the `HealthBuilder` and used to execute all health checks to get their status.

```csharp
var health = healthBuilder.Build();
var healthStatus = await health.HealthCheckRunner.ReadAsync();
```

### Formatting Health Status

We can output the applications health in different formats, by default a plain-text formatter is applied. We can write the health status result evaulated above as follows:

```csharp
using (var stream = new MemoryStream())
{
    await health.DefaultOutputHealthFormatter.WriteAsync(stream, healthStatus);
    var result = Encoding.UTF8.GetString(stream.ToArray());
    System.Console.WriteLine(result);
}
```

{{% notice info %}}
See [ASP.NET Core Health]({{< ref "web-monitoring/aspnet-core/health.md#example-reponses" >}}) for an example plain-text and JSON formatted responses.
{{% /notice %}}

## Configuring a Console Application

<i class="fa fa-hand-o-right"></i> First reate a new dotnet core console application.

<i class="fa fa-hand-o-right"></i> Then install the [App.Metrics.Health](https://www.nuget.org/packages/App.Metrics.Health/) nuget package:

```console
nuget install App.Metrics.Health
```

<i class="fa fa-hand-o-right"></i> Then modify your `Program.cs` with the following:

```csharp
public static class Program
{
    public static async Task Main()
    {
        var health = new HealthBuilder()
            .HealthChecks.AddCheck("DatabaseConnected",
                () => new ValueTask<HealthCheckResult>(HealthCheckResult.Healthy("Database Connection OK")))
            .Build();

        var healthStatus = await health.HealthCheckRunner.ReadAsync();

        using (var stream = new MemoryStream())
        {
            await health.DefaultOutputHealthFormatter.WriteAsync(stream, healthStatus);
            var result = Encoding.UTF8.GetString(stream.ToArray());
            System.Console.WriteLine(result);
        }

        System.Console.ReadKey();
    }
}
```

{{% notice tip %}}
In your `.csproj` add `<LangVersion>latest</LangVersion>` to allow the `async Task Main` method.
{{% /notice %}}