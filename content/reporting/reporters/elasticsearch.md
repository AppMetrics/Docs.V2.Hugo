---
title: "Elasticsearch"
date: 2017-09-28T22:34:02+10:00
draft: false
weight: 3
icon: "/images/elasticsearch.png"
---

The [App.Metrics.Reporting.Elasticsearch](https://www.nuget.org/packages/App.Metrics.Reporting.Elasticsearch/) nuget package reports metrics to [ElasticSearch](https://www.elastic.co/) using the [App.Metrics.Formatters.Elasticsearch](https://www.nuget.org/packages/App.Metrics.Formatters.Elasticsearch/) nuget package to format metrics as Elasticsearch documents.

## Getting started

<i class="fa fa-hand-o-right"></i> To use the Elasticsearch reporter, first install the [nuget package](https://www.nuget.org/packages/App.Metrics.Reporting.Elasticsearch/):

```console
nuget install App.Metrics.Reporting.Elasticsearch
```

<i class="fa fa-hand-o-right"></i> Then enable the reporter using `Report.ToElasticsearch(...)`:

```csharp
var metrics = new MetricsBuilder()
    .Report.ToElasticsearch("http://127.0.0.1:9200", "metricsindex")
    .Build();
```

<i class="fa fa-hand-o-right"></i> App Metrics at the moment leaves report scheduling up the the user unless using [App.Metrics.AspNetCore.Reporting]({{< ref "web-monitoring/aspnet-core/reporting.md" >}}). To run all configured reports use the `ReportRunner` on `IMetricsRoot`:

```csharp
await metrics.ReportRunner.RunAllAsync();
```

{{% notice info %}}
Report Scheduling will be added when [Microsoft.Extensions.Hosting.Background](https://github.com/aspnet/Hosting/blob/dev/src/Microsoft.Extensions.Hosting.Abstractions/BackgroundService.cs) is released, otherwise there are various other scheduling solutions available for C#. For ASP.NET Core reporting see details on the [App.Metrics.AspNetCore.Reporting]({{< ref "web-monitoring/aspnet-core/reporting.md" >}}) support package.
{{% /notice %}}

## Configuration

Configuration options are provided as a setup action used with `ToElasticsearch(...)`.

<i class="fa fa-hand-o-right"></i> To configure the Elasticsearch reporters options:

```csharp
var filter = new MetricsFilter().WhereType(MetricType.Timer);
var metrics = new MetricsBuilder()
    .Report.OverHttp(
        options => {
            options.Elasticsearch.Index = "metricsindex";
            options.Elasticsearch.BaseUri = new Uri("http://127.0.0.1:9200");
            options.Elasticsearch.AuthorizationSchema = ElasticSearchAuthorizationSchemes.Basic;
            options.Elasticsearch.BearerToken = "token";
            options.Elasticsearch.UserName = "admin";
            options.Elasticsearch.Password = "password";
            options.HttpPolicy.BackoffPeriod = TimeSpan.FromSeconds(30);
            options.HttpPolicy.FailuresBeforeBackoff = 5;
            options.HttpPolicy.Timeout = TimeSpan.FromSeconds(10);
            options.MetricsOutputFormatter = new MetricsJsonOutputFormatter();
            options.Filter = filter;
            options.FlushInterval = TimeSpan.FromSeconds(20);
        })
    .Build();
```

<i class="fa fa-hand-o-right"></i> Configuration options provided are:

|Option|Description|
|------|:--------|
|MetricsOutputFormatter|The formatter used when reporting metrics to Elasticsearch.
|Filter|The filter used to filter metrics just for this reporter.
|FlushInterval|The delay between reporting metrics to Elasticsearch.
|Elasticsearch.Index|The Elasticsearch index where metrics are reported.
|Elasticsearch.BaseUri|The URI of the Elasticsearch server.
|Elasticsearch.AuthorizationSchema|The authentication scheme used to authenticate with Elasticsearch, `Anonymous`, `Basic` or `BearerToken`.
|Elasticsearch.BearerToken|The password when using OAuth to auth with Elasticsearch.
|Elasticsearch.UserName|The username when using basic auth to auth with Elasticsearch.
|Elasticsearch.Password|The password when using basic auth to auth with Elasticsearch.
|HttpPolicy.BackoffPeriod|The `TimeSpan` to back-off when metrics are failing to report to the metrics ingress endpoint.
|HttpPolicy.FailuresBeforeBackoff|The number of failures before backing-off when metrics are failing to report to the metrics ingress endpoint.
|HttpPolicy.Timeout|The HTTP timeout duration when attempting to report metrics to the metrics ingress endpoint.

## Web Monitoring

### Grafana

The [App.Metrics.AspNetCore.Tracking](https://www.nuget.org/packages/App.Metrics.AspNetCore.Tracking/) nuget package automatically records typical web metrics when added to an ASP.NET Core application. App Metrics includes a couple of pre-built Grafana dashboards to visualize these metrics:

1. Add an index and the document mappings to Elasticsearch using the [default mappings](https://raw.githubusercontent.com/alhardy/AppMetrics.Extensions.Elasticsearch/master/visualization/App.Metrics.Sandbox-Elasticsearch-IndexAndMappingSetup.json) provided in the github repository
1. Install the App Metrics [Grafana dashboard](https://grafana.com/dashboards/2140). There is also an [OAuth2 dashboard](https://grafana.com/dashboards/2143) for APIs using OAuth2.