---
title: "Grafana"
draft: false
icon: "/images/grafana.png"
weight: 7
---

Out-of-box, App.Metrics includes [Grafana dashboards](https://grafana.com/dashboards?search=app%20metrics) that are built to monitor metrics reported by ASP.NET Core application's that are using the [App.Metrics.AspNetCore.Tracking](https://www.nuget.org/packages/App.Metrics.AspNetCore.Tracking/) nuget pacakge.

## Dashboard Features

### Web Monitoring

- Metrics recorded by [App Metrics ASP.NET Core Tracking]({{< ref "web-monitoring/aspnet-core/tracking-middleware.md" >}}) are pre-configured in the Grafana dashboard.
- Supports filtering graphs by environment allow the same dashbaord to be re-used across environments.
- Supports filtering graphs by application. The idea is to tag metrics by your application's name allowing you to re-use the same dashboard instance for all web applications using App Metrics.
- Graphs are configured to use a `datasource` template variable so that you don't need to update all charts with the datasource you configured in Grafana.
- Display a health overview showing passed, degraded and failed checks as well as an overall health status, color coded by the status value. (***Not yet available in App Metrics 2.0***)

<img alt="grafana web demo" src="https://raw.githubusercontent.com/alhardy/AppMetrics.DocFx/master/images/generic_grafana_dashboard_demo.gif" />

<i class="fa fa-hand-o-right"></i> Get the Grafana Dashboards

- [InfluxDB](https://grafana.com/dashboards/2125).
- [Elasticsearch](https://grafana.com/dashboards/2140).
- [Graphite](https://grafana.com/dashboards/2192).
- [Prometheus](https://grafana.com/dashboards/2204).

### OAuth2 Monitoring

- OAUth2 Metrics measured by [App Metrics ASP.NET Core Tracking]({{< ref "web-monitoring/aspnet-core/tracking-middleware.md#oauth2" >}}) are pre-configured in the Grafana dashboard.
- Supports filtering graphs by environment, application and datasource as explained above.
- Supports filtering graphs by `client_id` so that we can visualize request rates for example on each API endpoint, useful for example to determine client rate limits or what versions clients are using.
- Supports filtering graphs by `route`, allowing us to more clearly visualize a clients usage of a specific endpoint.

<img alt="grafana web oauth2 demo" src="https://raw.githubusercontent.com/alhardy/AppMetrics.DocFx/master/images/generic_grafana_oauth2_dashboard_demo.gif" />

<i class="fa fa-hand-o-right"></i> Get the Grafana Dashboards

- [InfluxDB](https://grafana.com/dashboards/2137).
- [Elasticsearch](https://grafana.com/dashboards/2143).
- [Graphite](https://grafana.com/dashboards/2198).

## How to setup the dashboards

1. Configure your application to use [App Metrics ASP.NET Core Tracking]({{< ref "web-monitoring/aspnet-core/tracking-middleware.md" >}}) and [Reporting]({{< ref "reporting/reporters/_index.md" >}}) targetting the time series database of choice.
1. Run your application to report some metrics.
1. Download and install [Grafana](https://grafana.com/grafana/download), then create a new datasource specifying the reporter's details configured as part of your application setup.
1. [Import the desired Grafana dashboard(s)](https://grafana.com/dashboards?search=app metrics) by copying and pasting the dasboard ID into the [Import Dashboard](http://docs.grafana.org/reference/export_import/#importing-a-dashboard) window in Grafana

{{% notice warning %}}
The Grafana dashboards expect `app`, `env` and `server` [global tags]({{< ref "getting-started/fundamentals/tagging-organizing.md#default-global-tags" >}}) to be set.
{{% /notice %}}