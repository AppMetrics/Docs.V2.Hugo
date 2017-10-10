---
title: "Grafana"
date: 2017-09-28T22:35:43+10:00
draft: false
icon: "/images/grafana.png"
weight: 3
---

App Metrics does not include any visualzation tool but does include [Grafana](http://grafana.org/) dashboards for web applications which will get you started with the default metrics recorded by [App Metrics ASP.NET Core Tracking]({{< ref "web-monitoring/aspnet-core/tracking-middleware.md" >}}). There are also [Sample Applications](https://github.com/AppMetrics/Samples), each with a ready-to-go [Grafana dashboards](https://github.com/AppMetrics/Samples/tree/master/grafana_dashboards) to help you get started.

{{% notice tip %}}
You can find App Metrics compatible Grafana dashboards on [Grafana Labs](https://grafana.com/dashboards?search=app metrics).
{{% /notice %}}

### InfluxDB Demo Dashboard

The [sample console application](https://github.com/AppMetrics/Samples/tree/master/src/App.Sample) is configured to report metrics to [InfluxDB](https://www.influxdata.com/time-series-platform/influxdb/), with metrics persisted in InfluxDB, [Grafana](https://grafana.net/) can be used to visualize and alert on metrics. Below is a Grafana dashbaord showing some of the metrics recorded by the sample console application.

<img alt="grafana demo" src="https://raw.githubusercontent.com/alhardy/app-metrics-docs/master/images/grafana_console.gif" />

### Web Monitoring

<i class="fa fa-hand-o-right"></i> A web application dashboard that can be used with [App Metrics ASP.NET Core Tracking]({{< ref "web-monitoring/aspnet-core/tracking-middleware.md" >}}).

<img alt="grafana app metrics middleware" src="https://raw.githubusercontent.com/alhardy/AppMetrics.DocFx/master/images/generic_grafana_dashboard_demo.gif" />

### OAuth2 Monitoring

<i class="fa fa-hand-o-right"></i> OAuth2 client tracking dashboard that can be used with [App Metrics ASP.NET Core Tracking]({{< ref "web-monitoring/aspnet-core/tracking-middleware.md" >}}).

<img alt="grafana app metrics oauth2 middleware" src="https://raw.githubusercontent.com/alhardy/AppMetrics.DocFx/master/images/generic_grafana_oauth2_dashboard_demo.gif"/>
