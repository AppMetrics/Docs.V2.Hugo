---
title: "Health"
draft: false
icon: "/images/code.png"
weight: 2
---

## App Metrics Health Code Samples

You can find code samples referenced below for App Metrics features on [GitHub](https://github.com/AppMetrics/Samples.V2).

### Solutions

#### AspNetCore2.Health.Api.sln

<i class="fa fa-hand-o-right"></i> `AspNetCore2.Health.Api.QuickStart.csproj`: An ASP.NET Core 2.0 Api with App Metrics Health 2.0 basics configured. Enable the following compiler directives to highlight example configuration options:

- `HOSTING_OPTIONS`: Custom hosting configuration which sets a custom port and endpoint for each of the endpoints added by App Metrics Health.
- `INLINE_CHECKS`: Adds example of [pre-defined health checks]({{< ref "health-checks/pre-defined-checks.md" >}}) and an example in-line health check.

{{% notice info %}}
Get the code [here](https://github.com/AppMetrics/Samples.V2/tree/master/AspNetCore2.Health.Api.QuickStart)
{{% /notice %}}

#### NetFullFramework.Health.Console

<i class="fa fa-hand-o-right"></i> `Net452.Health.Console.QuickStart.csproj`: A NET452 console application with App Metrics Health 2.0 basics configured.

{{% notice info %}}
Get the code [here](https://github.com/AppMetrics/Samples.V2/tree/master/Net452.Health.Console.QuickStart)
{{% /notice %}}

<i class="fa fa-hand-o-right"></i> `Net461.Health.MicrosoftDI.Console.QuickStart`: A NET461 console application with App Metrics Health 2.0 basics configured using `Microsoft.Extensions.DependencyInjection`.

{{% notice info %}}
Get the code [here](https://github.com/AppMetrics/Samples.V2/tree/master/Net461.Health.MicrosoftDI.Console.QuickStart)
{{% /notice %}}

___

{{% notice note %}}
App Metrics 1.0 samples can be found on GitHub [here](https://github.com/AppMetrics/Samples)
{{% /notice %}}
