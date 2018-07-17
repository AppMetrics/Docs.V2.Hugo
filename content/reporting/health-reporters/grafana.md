---
title: "Grafana"
draft: false
weight: 6
icon: "/images/grafana.png"
---

Health check results can be visualised with Grafana by [reporting health check results as metrics]({{< ref "reporting/health-reporters/metrics.md" >}}). Grafana also supports [annotations](http://docs.grafana.org/reference/annotations/), whereby points can be marked on a graph with event descriptions.

The [App.Metrics.Health.Reporting.GrafanaAnnotation](https://www.nuget.org/packages/App.Metrics.Health.Reporting.GrafanaAnnotation/) nuget package supports marking Grafana graphs with failing and/or degraded health check results.

{{% notice info %}}
This is currently a work in progress. More documentation to come.
{{% /notice %}}