---
title: "Socket"
draft: false
weight: 5
---

The [App.Metrics.Reporting.Socket](https://www.nuget.org/packages/App.Metrics.Reporting.Socket/) nuget package reports metrics through a socket. The reporter has no default output formatter, so one of the [available formatters](https://www.nuget.org/packages?q=App.Metrics.Formatters) must be provided.

## Getting started

<i class="fa fa-hand-o-right"></i> To use the Socket reporter, first install the [Socket nuget package](https://www.nuget.org/packages/App.Metrics.Reporting.Socket/):

```console
nuget install App.Metrics.Reporting.Socket
```

<i class="fa fa-hand-o-right"></i> Then install output formatter package of your choice.<br>For example, to report metrics into [Telegraf](https://github.com/influxdata/telegraf/) the [InfluxDB](https://www.nuget.org/packages/App.Metrics.Formatters.InfluxDB/) package is required:

```console
nuget install App.Metrics.Formatters.InfluxDB
```

## Capabilities

Socket reporter is transport layer to send data via sockets. Socket reporter supports TCP and UDP protocols for both Windows and Unix systems and Unix Domain Sockets for Unix. Reporter is unspecific to formatting protocols, so it can be used with any application that can receive data via sockets.

The reporter was tested with [Telegraf](https://github.com/influxdata/telegraf/). Plugin [socket_listener](https://github.com/influxdata/telegraf/tree/master/plugins/inputs/socket_listener) was used to receive metrics.

<i class="fa fa-hand-o-right"></i> To send metrics over TCP protocol:

```csharp
var metrics = new MetricsBuilder()
    .Report.OverTcp("localhost", 8094)
    .Build();
```

<i class="fa fa-hand-o-right"></i> To send metrics over UDP protocol:

```csharp
var metrics = new MetricsBuilder()
    .Report.OverUdp("127.0.0.1", 8094)
    .Build();
```

<i class="fa fa-hand-o-right"></i> To send metrics over Unix Domain Sockets:

```csharp
var metrics = new MetricsBuilder()
    .Report.OverUds("//tmp/telegraf.sock")
    .Build();
```

## Configuration

<i class="fa fa-hand-o-right"></i> The reporter can be configured through an `options` object passed to the setup action. There are several reporter-specific and common options.<br>

```csharp
var formatter = new MetricsInfluxDbLineProtocolOutputFormatter();

var metrics = new MetricsBuilder()
    .Report.OverTcp(
        options => {
            options.SocketSettings.Address = "localhost";
            options.SocketSettings.Port = 8094;

            options.Filter = new MetricsFilter().WhereType(MetricType.Timer);
            options.FlushInterval = TimeSpan.FromSeconds(12);
            options.MetricsOutputFormatter = formatter;
            options.SocketPolicy = new SocketPolicy();
        })
    .Build();
```

<i class="fa fa-hand-o-right"></i> Reporter-specific options are:

||SocketSettings.Address|SocketSettings.Port|
|------|:--------|:--------|
|OverTcp<br>OverUdp|Address of a socket endpoint<br>(DNS name or IP address)|Port of a socket endpoint|
|OverUds|"//path/to/socket/file"<br>(Works only on Unix)|The port must be 0|

<i class="fa fa-hand-o-right"></i> Common options are:

|Option|Description|
|------|:--------|
|MetricsOutputFormatter|The formatter used for format metrics into a string which will be sent over sockets layer.|
|Filter|The filter used to filter metrics.|
|FlushInterval|The delay between each report attempt.|
|SocketPolicy.FailuresBeforeBackoff|The number of failures before backing-off when failing to report metrics to a socket.|
|SocketPolicy.BackoffPeriod|The `TimeSpan` to back-off when failing to report metrics to a socket.|
|SocketPolicy.Timeout|The timeout duration when attempting to report metrics to a socket.|
