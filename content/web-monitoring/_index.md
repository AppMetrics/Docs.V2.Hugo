---
title: "Web Applications"
date: 2017-09-28T22:30:58+10:00
draft: false
chapter: false
pre: "<b>3. </b>"
weight: 3
icon: "/images/webmetrics.png"
---

Insight into real-time application metrics is such a great thing to have, once you have it you won't look back, especially when working with distributed systems which can be very difficult to at a glance understand the health of a system.

It allows us to detect and alert on anomalies when releasing a new version, assists in identifing bottlenecks, provides insight into why our appilcation may be scaling too often during a particular time of the day, allows us to provide trending statistics to our stakeholders, allows us to identifiy how often and at what rate clients are requesting specific endpoints in an API and much much more.

App Metrics makes it easy to get up and running with an open source monitoring solution, providing extensions allowing us to report to various open source time series databases (TSDB) and also provides generic dashbaords allowing us to visualize our application's metrics in real-time.

There are several [TSDB reporting extensions]({{< ref "reporting/reporters/_index.md" >}}) which App Metrics provides. If you can't find what you're looking for, [open a new github issue](https://github.com/AppMetrics/AppMetrics/issues/new) to discuss adding support.

Out-of-box App Metrics does not provide a visualization tool, instead it includes [Grafana Dashboards](visualization-grafana.md) for web application monitoring, Grafana does such an amazing job at this already and supports the most [popular TSBDs](http://docs.grafana.org/features/datasources/influxdb/) available today.

The following documentation sections assist in getting started with App Metrics in a web application:

{{% children description="true" %}}
