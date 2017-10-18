---
title: "Metrics"
draft: false
icon: "/images/code.png"
weight: 1
---

## App Metrics Code Samples

You can find code samples referenced below for App Metrics features on [GitHub](https://github.com/AppMetrics/Samples.V2).

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

#### Net452.Metrics.Console.sln

<i class="fa fa-hand-o-right"></i> `Net452.Metrics.Console.QuickStart.csproj`: A NET452 console application with App Metrics 2.0 basics configured.

{{% notice info %}}
Get the code [here](https://github.com/AppMetrics/Samples.V2/tree/master/Net452.Metrics.Console.QuickStart)
{{% /notice %}}

___

{{% notice note %}}
App Metrics 1.0 samples can be found on GitHub [here](https://github.com/AppMetrics/Samples)
{{% /notice %}}
