---
title: "Configuration"
draft: false
weight: 1
icon: "/images/configuration.png"
---

App Metrics provides access to configuration options via the [MetricsBuilder]({{< ref "getting-started/_index.md" >}}). These configuration options include:

|Property|Description|
|------|:--------|
|DefaultContextLabel|Metrics recorded through the `IMetrics` interface are grouped into "Contexts", for example a database context or application context. Metrics names should be unique per context. If no context label is presented when recording a metric. This value can be changed through the DefaultContextLabel option, *default is "Application"*.
|GlobalTags|All metric types can be tagged, for example we could tag all metrics by environment e.g. staging or production so when we report metrics we know the environment for which they originated.
|Enabled|Allows recording of all metrics to be enabled/disabled, *default is true*.
|ReportingEnabled|Allows all configured reporters to be enabled/disabled, *default is true*.

### Configure in code

<i class="fa fa-hand-o-right"></i> App Metrics options can be configured using a setup action which provides access to the `MetricsOptions`:

```csharp
var metrics = new MetricsBuilder()
    .Configuration.Configure(
        options =>
        {
            options.DefaultContextLabel = "MyContext";
            options.GlobalTags.Add("myTagKey", "myTagValue");
            options.Enabled = true;
            options.ReportingEnabled = true;
        })
     ... // configure other options
    .Build();
```

### Configure with key/value pairs

<i class="fa fa-hand-o-right"></i> App Metrics supports reading configuration from key value pairs. With this, `<appSettings>` based configuration in an `App.config` or `Web.config` is supported:

```csharp
var appSettings = ConfigurationManager.AppSettings.AllKeys.ToDictionary(k => k, k => ConfigurationManager.AppSettings[k]);
var metrics = new MetricsBuilder()
    .Configuration.Configure(appSettings)
     ... // configure other options
    .Build();
```

<i class="fa fa-hand-o-right"></i> Here is an example `App.config` configuration file:

```xml
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
  <appSettings>
    <add key="MetricsOptions:DefaultContextLabel" value="MyContext" />
    <add key="MetricsOptions:GlobalTags:myTagKey" value="myTagValue" />
    <add key="MetricsOptions:Enabled" value="true" />
    <add key="MetricsOptions:ReportingEnabled" value="true" />
  </appSettings>
</configuration>
```

### Configure with Microsoft.Extensions.Configuration

<i class="fa fa-hand-o-right"></i> App Metrics supports reading configuration using `Microsoft.Extensions.Configuration`. The [support package](https://www.nuget.org/packages/App.Metrics.Extensions.Configuration/) needs to be installed from nuget:

```console
install-package App.Metrics.Extensions.Configuration
```

To read from `appsettings.json` for example, use the `Microsoft.Extensions.Configuration.ConfigurationBuilder` to build an `Microsoft.Extensions.Configuration.IConfiguration` and then use the `ReadFrom(Microsoft.Extensions.Configuration.IConfiguration)` extension method on the `MetricsBuilder`:

```csharp
var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

var metrics = new MetricsBuilder()
    .Configuration.ReadFrom(configuration)
     ... // configure other options
    .Build();
```

<i class="fa fa-hand-o-right"></i> Here is an example `appsettings.json` configuration file:

```json
{
  "MetricsOptions": {
    "DefaultContextLabel": "MyContext",
    "GlobalTags": { "myTagKey": "myTagValue" },
    "Enabled": true,
    "ReportingEnabled": true
  }
}
```
