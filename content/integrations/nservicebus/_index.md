---
title: "NServiceBus"
draft: false
weight: 1
icon: ""
---

## Overview

App Metrics provides a NServiceBus Feature to capture and record metrics emitted by NServiceBus. You can find the metrics captured by NServiceBus [here](https://docs.particular.net/monitoring/metrics/definitions).

|Package|Description|
|------|:--------|
|[App.Metrics.Extensions.NServiceBus](https://www.nuget.org/packages/App.Metrics.Extensions.NServiceBus/)|Includes a NServiceBus [Feature](https://docs.particular.net/nservicebus/pipeline/features) to record metrics emitted by NServiceBus.|

## Enabling the feature

To enable App Metrics to start capturing and recording NServiceBus metrics, simply add the feature to the NServiceBus endpoint configuration.

```csharp
 var cfg = new EndpointConfiguration("EndpointName");
 cfg.EnableFeature<AppMetricsFeature>();
```

## Reporting

With the NService Feature enabled any of the App Metrics reporters can be used to visualize NServiceBus emitted metrics.

You can find an InfluxDB Grafana Dashaboard [here](https://grafana.com/grafana/dashboards/12471).

![NServiceBus Grafana](images\nservicebus_grafana.png)