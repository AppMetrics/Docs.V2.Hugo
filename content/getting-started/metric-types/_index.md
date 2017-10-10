---
title: "Metric Types"
draft: false
chapter: false
weight: 2
icon: "/images/metrictypes.png"
---

App Metrics includes various types of metrics, each providing their own usefulness depending on the measurement being tracked. A great presentation can be found [here](https://www.youtube.com/watch?v=czes-oa0yik) which explains the fundamentals well.

The following metrics types are supported:

|Type|Description|
|------|:--------|
|[Apdex]({{< relref "apdex.md" >}})|By definition, an [Application Performance Index](https://en.wikipedia.org/wiki/Apdex) is an open industry standard that estimates end-user satisfaction. Apdex is provided as a metric type in App Metrics allowing us to not only estimate end-user satisfaction on a web application for example, but also allowing us to easily define SLA's on parts of our applications.
|[Counter]({{< relref "counters.md" >}})|Counters are one of the simpliest metrics types supported and allow us to track how many times something has happened, for example the number of successful user logins. They are an atomic 64-bit integer which can be incremented or decremented.
|[Gauge]({{< relref "gauges.md" >}})|A Gauge is simply an action that returns the instantaneous measure of a value, where the value abitrarily increases and decreases, for example CPU usage.
|[Histogram]({{< relref "histograms.md" >}})|Histograms measure the statistical distribution of a set of values including the min, max, mean, median, standard deviation and [quantiles](https://en.wikipedia.org/wiki/Quantile), for example HTTP POST and PUT request sizes.
|[Meter]({{< relref "meters.md" >}})|Meter measures the rate at which an event occurs along with a total count of the occurances, for example tracking the rate at which clients are requesting a particular HTTP resource.
|[Timer]({{< relref "timers.md" >}})|A Timer is a combination of a Histogram and a Meter allowing us to measure the duration of a type of event, the rate of its occurrence and provide duration statistics, for example tracking the time it takes to execute a particular SQL query or HTTP request.

## Start instrumenting your application

Once familar with the metric types available and what they have to offer, see the [getting started guide]({{< ref "getting-started/_index.md" >}})  on how to instrumenting your application.