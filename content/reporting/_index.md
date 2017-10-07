---
title: "Reporting"
date: 2017-09-28T22:30:39+10:00
draft: false
chapter: false
pre: "<b>4. </b>"
weight: 4
icon: "/images/reporting.png"
---

There are several time series database reporting extensions which App Metrics provides as well as more generic reporters, see the *Reporters* menu on the left.

App Metrics does not include a visualization tool, [Grafana](https://grafana.com/) and [Timelion](https://www.elastic.co/blog/timelion-timeline) for example already do an amazing job of this.

You can find App Metrics specific Grafana Dashboards on [Grafana Labs](https://grafana.com/dashboards?search=app%20metrics). These dashboards work with the metrics recorded by the [App.Metrics.AspNetCore.Tracking](https://www.nuget.org/packages/App.Metrics.AspNetCore.Tracking/) nuget package.

For App Metrics reporting topics, review the following documentation:

{{% children description="true" %}}

{{% notice info %}}
Don't see the reporter your looking for? [Open a new github issue](https://github.com/AppMetrics/AppMetrics/issues/new) to discuss adding support.
{{% /notice %}}