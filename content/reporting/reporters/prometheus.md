---
title: "Prometheus"
draft: false
weight: 2
icon: "/images/prometheus.png"
---

## Basics

[Prometheus](https://prometheus.io/) promotes a *Pull* based approach rather than *Push*, therefore App Metrics does not include a reporter to push metrics, but rather supports formatting metric data in Prometheus formats using the [App.Metrics.Formatters.Prometheus](https://www.nuget.org/packages/App.Metrics.Formatters.Prometheus/) nuget package. `App.Metrics.Formatters.Prometheus` supports both Prometheus's plain text and protobuf formats.

<i class="fa fa-hand-o-right"></i> To use the Prometheus formatter, first install the [nuget package](https://www.nuget.org/packages/App.Metrics.Formatters.Prometheus/):

```console
nuget install App.Metrics.Formatters.Prometheus
```

<i class="fa fa-hand-o-right"></i> Then enable Prometheus formatting using the `MetricsBuilder`:

#### Plain text

<i class="fa fa-hand-o-right"></i> To configure Prometheus plain-text formatting:

```csharp
var metrics = new MetricsBuilder()
    .OutputMetrics.AsPrometheusPlainText()
    .Build();
```

#### Protobuf

<i class="fa fa-hand-o-right"></i> To configure Prometheus protobuf formatting:

```csharp
var metrics = new MetricsBuilder()
    .OutputMetrics.AsPrometheusProtobuf()
    .Build();
```

{{% notice info %}}
See the [metrics formatting]({{< ref "getting-started/formatting-metrics/_index.md" >}}) documentation on more details around outputting metrics using a specific formatter.
{{% /notice %}}

## ASP.NET Core Configuration

To expose metrics for Prometheus to scrape in an ASP.NET Core application:

<i class="fa fa-hand-o-right"></i> First install the [App.Metrics.AspNetCore](https://www.nuget.org/packages/App.Metrics.AspNetCore/) nuget package:

```console
nuget install App.Metrics.AspNetCore
```

<i class="fa fa-hand-o-right"></i> Then configure the `WebHost`:

```csharp
public static class Program
{
    public static IMetricsRoot Metrics { get; set; }

    public static IWebHost BuildWebHost(string[] args)
    {
        Metrics = AppMetrics.CreateDefaultBuilder()
                .OutputMetrics.AsPrometheusPlainText()
                .OutputMetrics.AsPrometheusProtobuf()
                .Build();

        return WebHost.CreateDefaultBuilder(args)
                        .ConfigureMetrics(Metrics)
                        .UseMetrics(
                            options =>
                            {
                                options.EndpointOptions = endpointsOptions =>
                                {
                                    endpointsOptions.MetricsTextEndpointOutputFormatter = Metrics.OutputMetricsFormatters.OfType<MetricsPrometheusTextOutputFormatter>().First();
                                    endpointsOptions.MetricsEndpointOutputFormatter = Metrics.OutputMetricsFormatters.OfType<MetricsPrometheusProtobufOutputFormatter>().First();
                                };
                            })
                        .UseStartup<Startup>()
                        .Build();
    }

    public static void Main(string[] args) { BuildWebHost(args).Run(); }
}
```

With the above configuration, `/metrics-text` will return metrics in Prometheus plain text format and `/metrics` in Prometheus protobuf format.

{{% notice warning %}}
Prometheus > 2.0 does not support Protobuf format so use plain text format for `/metrics` endpoint.
See the [Prometheus EXPOSITION FORMATS]({{< ref "https://prometheus.io/docs/instrumenting/exposition_formats" >}})
{{% /notice %}}

{{% notice info %}}
See the [ASP.NET Core documentation]({{< ref "web-monitoring/aspnet-core/_index.md" >}}) for more details on integrating App Metrics in an ASP.NET Core application.
{{% /notice %}}

## Web Monitoring

### Grafana

The [App.Metrics.AspNetCore.Tracking](https://www.nuget.org/packages/App.Metrics.AspNetCore.Tracking/) nuget package automatically records typical web metrics when added to an ASP.NET Core application. App Metrics includes pre-built Grafana dashboards to visualize these metrics:

1. Configure the Prometheus [scrape config](https://prometheus.io/docs/prometheus/latest/configuration/configuration/#scrape_config)
1. Install the App Metrics [Grafana dashboard](https://grafana.com/dashboards/2204).
