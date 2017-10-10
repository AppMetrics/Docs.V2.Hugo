---
title: "Code"
date: 2017-09-28T22:35:43+10:00
draft: false
icon: "/images/code.png"
---

You can find code samples referenced below for App Metrics features on [GitHub](https://github.com/AppMetrics/Samples.V2).

## Metrics

### Solutions

#### AspNetCore2.Api.sln

<i class="fa fa-hand-o-right"></i> `AspNetCore2.Api.QuickStart.csproj`: An ASP.NET Core 2.0 Api with App Metrics 2.0 basics configured. This project works with the web monitoring dashboards which can be [imported from Grafana Labs](https://grafana.com/dashboards?search=app%20metrics). Enable the following compiler directives to highlight example configuration options:

- `HOSTING_OPTIONS`: Custom hosting configuration which sets a custom port and endpoint for each of the endpoints added by App Metrics.
- `REPORTING`: Schedules metric reporting via console.

{{% notice info %}}
Get the code [here](https://github.com/AppMetrics/Samples.V2/tree/master/AspNetCore2.Api.QuickStart)
{{% /notice %}}

<i class="fa fa-hand-o-right"></i> `AspNetCore2.Api.Reservoirs.csproj`: An ASP.NET Core 2.0 Api with App Metrics 2.0 [InfluxDB Reporting]({{< ref "reporting/reporters/influx-data.md" >}}) configured and demostrates the difference between supported [reservoir types]({{< ref "getting-started/reservoir-sampling/_index.md" >}}) through a sample [Grafana dashboard](https://grafana.com/dashboards/3408/edit). *Requires an `appmetricsreservoirs` InfluxDB database*.

{{% notice info %}}
Get the code [here](https://github.com/AppMetrics/Samples.V2/tree/master/AspNetCore2.Api.Reservoirs)
{{% /notice %}}

##  Health

### Solutions

#### AspNetCore2.Health.Api.sln

<i class="fa fa-hand-o-right"></i> `AspNetCore2.Health.Api.QuickStart.csproj`: An ASP.NET Core 2.0 Api with App Metrics Health 2.0 basics configured. Enable the following compiler directives to highlight example configuration options:

- `HOSTING_OPTIONS`: Custom hosting configuration which sets a custom port and endpoint for each of the endpoints added by App Metrics Health.
- `INLINE_CHECKS`: Adds example of [pre-defined health checks]({{< ref "health-checks/pre-defined-checks.md" >}}) and an example in-line health check.

{{% notice info %}}
Get the code [here](https://github.com/AppMetrics/Samples.V2/tree/master/AspNetCore2.Health.Api.QuickStart)
{{% /notice %}}

___

{{% notice note %}}
App Metrics 1.0 samples can be found on GitHub [here](https://github.com/AppMetrics/Samples)
{{% /notice %}}
