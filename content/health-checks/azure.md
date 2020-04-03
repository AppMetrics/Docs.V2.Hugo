---
title: "Azure Health Checks"
draft: false
chapter: false
weight: 3
icon: "/images/azure.png"
---

## Azure Health Checks

{{% notice warning %}}
Health Checks have now been retired given Microsoft have an equivalent implementation.
{{% /notice %}}

App Metrics includes pre-defined health checks for Azure resources which can be registered using the `HealthBuilder`.

```console
nuget install App.Metrics.Health.Checks.AzureDocumentDB
nuget install App.Metrics.Health.Checks.AzureStorage
nuget install App.Metrics.Health.Checks.AzureServiceBus
nuget install App.Metrics.Health.Checks.AzureEventHubs
```

```csharp
var healthBuilder = new HealthBuilder()
    .HealthChecks.AddAzureBlobStorageConnectivityCheck("Blob Storage Connectivity Check", storageAccount)
    .HealthChecks.AddAzureBlobStorageContainerCheck("Blob Storage Container Check", storageAccount, containerName)
    .HealthChecks.AddAzureQueueStorageConnectivityCheck("Queue Storage Connectivity Check", storageAccount)
    .HealthChecks.AddAzureQueueStorageCheck("Queue Storage Check", storageAccount, queueName)
    .HealthChecks.AddAzureDocumentDBDatabaseCheck("DocumentDB Database Check", documentDbDatabaseUri, documentDbUri, doucmentDbKey)
    .HealthChecks.AddAzureDocumentDBCollectionCheck("DocumentDB Collection Check", collectionUri, documentDbUri, doucmentDbKey)
    .HealthChecks.AddAzureTableStorageConnectivityCheck("Table Storage Connectivity Check", storageAccount)
    .HealthChecks.AddAzureTableStorageTableCheck("Table Storage Table Exists Check", storageAccount, "test")
    .HealthChecks.AddAzureEventHubConnectivityCheck("Service EventHub Connectivity Check", eventHubConnectionString, eventHubName)
    .HealthChecks.AddAzureServiceBusQueueConnectivityCheck("Service Bus Queue Connectivity Check", serviceBusConnectionString, queueName)
    .HealthChecks.AddAzureServiceBusTopicConnectivityCheck("Service Bus Topic Connectivity Check", serviceBusConnectionString, topicName)
    .Build();
```
