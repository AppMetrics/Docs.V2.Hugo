---
title: "Home"
draft: false
chapter: false
---

### What is App Metrics?

App Metrics is an open-source and cross-platform .NET library used to record metrics within an application. App Metrics can run on .NET Core or on the full .NET framework also supporting .NET 4.5.2.

App Metrics abstracts away the underlying repository of your Metrics for example InfluxDB, Prometheus, Graphite, Elasticsearch etc, by sampling and aggregating in memory and providing extensibility points to flush metrics to a repository at a specified interval.

App Metrics provides various metric types to measure things such as the rate of requests, counting the number of user logins over time, measure the time taken to execute a database query, measure the amount of free memory and so on. Metrics types supported are Apdex, Gauges, Counters, Meters, Histograms and Timers.

App Metrics also provides a health checking system allowing you to monitor the health of your application through user defined checks.

With App Metrics you can:

+ Capture application metrics within any type of .NET application e.g. Windows Service, MVC Site, Web API etc
+ Automatically measure the performance and error of each endpoint in an MVC or Web API project
+ When securing an API with OAuth2, automatically measure the request rate and error rate per client
+ Choose where to persist captured metrics and the dashboard you wish to use to visualize these metrics
+ Support Push and Pull based metrics collection via TSDB specific reporting extensions and exposing metrics in the required format over http
+ Support pushing metrics to a custom HTTP endpoint in the required format
+ Support writing metrics to file in the required format for collection by a metrics agent

### What's the Performance Overhead?

The performance overhead in terms of processing is minimal, see the [benchmark results](https://github.com/AppMetrics/AppMetrics/tree/master/benchmarks/App.Metrics.Benchmarks.Runner/BenchmarkDotNet.Artifacts/results) for the overhead on recording metrics.

The memory overhead is also minimal, but will depend on the number of metrics your're tracking. However even with a very large number of metrics the memory overhead is still minimal. Histograms consume the most memory in terms of metric types as they keep a stream of data in memory to measure the statistical distribution of values, although App Metrics uses [Reservoir Sampling]({{< ref "getting-started/reservoir-sampling/_index.md" >}}) to work around this which allows us to maintain a small, manageable reservoir representing the entire data stream.

### Why build App Metrics?

App Metrics was built to provide an easy way to capture the desired metrics within an application whilst having minimal impact on the performance of your application, and allowing these metrics to be reported to the desired respository through the library's reporting capabilities.

There are many open-source time series databases and payed monitoring solutions out there each with their own pros and cons, App Metrics provides your application the ability to capture metrics and then easily swap out the underlying metric repository or report to multiple repositories with little effort.

With App Metrics you can:

+ Capture application metrics within any type of .NET application e.g. Windows Service, MVC Site, Web API etc
+ Automatically measure the performance and error of each endpoint in an MVC or Web API project
+ When securing an API with OAuth2, automatically measure the request rate and error rate per client
+ Choose where to persist captured metrics and the dashboard you wish to use to visualize these metrics
