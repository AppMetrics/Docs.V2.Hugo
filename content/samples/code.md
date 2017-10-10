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

`AspNetCore2.Api.QuickStart.csproj`: An ASP.NET Core 2.0 Api with App Metrics 2.0 basics configured. This project works with the web monitoring dashboards which can be [imported from Grafana Labs](https://grafana.com/dashboards?search=app%20metrics). Enable the following compiler directives to highlight example configuration options:

- `HOSTING_OPTIONS`: Custom hosting configuration which sets a custom port and endpoint for each of the endpoints added by App Metrics.
- `REPORTING`: Schedules metric reporting via console.

{{% notice info %}}
Get the code [here](https://github.com/AppMetrics/Samples.V2/tree/master/AspNetCore2.Api.QuickStart)
{{% /notice %}}

##  Health

### Solutions

#### AspNetCore2.Health.Api.sln

`AspNetCore2.Health.Api.QuickStart.csproj`: An ASP.NET Core 2.0 Api with App Metrics Health 2.0 basics configured. Enable the following compiler directives to highlight example configuration options:

- `HOSTING_OPTIONS`: Custom hosting configuration which sets a custom port and endpoint for each of the endpoints added by App Metrics Health.
- `INLINE_CHECKS`: Adds example of [pre-defined health checks]({{< ref "health-checks/pre-defined-checks.md" >}}) and an example in-line health check.

{{% notice info %}}
Get the code [here](https://github.com/AppMetrics/Samples.V2/tree/master/AspNetCore2.Health.Api.QuickStart)
{{% /notice %}}
