---
title: "Grafana"
draft: false
icon: "/images/grafana.png"
weight: 3
---

App Metrics does not include any visualzation tool but does include [Grafana](http://grafana.org/) dashboards for web applications which will get you started with the default metrics recorded by [App Metrics ASP.NET Core Tracking]({{< ref "web-monitoring/aspnet-core/tracking-middleware.md" >}}).

{{% notice tip %}}
You can find App Metrics compatible Grafana dashboards on [Grafana Labs](https://grafana.com/dashboards?search=app metrics).
{{% /notice %}}

### Web Monitoring

<i class="fa fa-hand-o-right"></i> Web application dashboard that can be used with [App Metrics ASP.NET Core Tracking]({{< ref "web-monitoring/aspnet-core/tracking-middleware.md" >}}). With metrics tagged by application, the same instance of the dashbaord can be used by mulitple applications to monitor typical web metrics.

<img alt="grafana app metrics middleware" src="https://raw.githubusercontent.com/AppMetrics/Docs.V2.Hugo/main/static/images/generic_grafana_dashboard_demo.gif" />

### OAuth2 Monitoring

<i class="fa fa-hand-o-right"></i> OAuth2 client tracking dashboard that can be used with [App Metrics ASP.NET Core Tracking]({{< ref "web-monitoring/aspnet-core/tracking-middleware.md" >}}).

<img alt="grafana app metrics oauth2 middleware" src="https://raw.githubusercontent.com/AppMetrics/Docs.V2.Hugo/main/static/images/generic_grafana_oauth2_dashboard_demo.gif"/>

### InfluxDB Demo Dashboard

<img alt="grafana demo" src="https://raw.githubusercontent.com/alhardy/app-metrics-docs/master/images/grafana_console.gif" />
