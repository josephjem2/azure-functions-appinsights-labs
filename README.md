# Azure Functions + Application Insights Labs

Hands-on Azure Monitor and Application Insights labs using Azure Functions and .NET 8 isolated.

## Overview

This repository contains practical labs focused on:

- Azure Functions
- Application Insights
- Azure Monitor
- Log Analytics
- Kusto Query Language (KQL)
- Telemetry Correlation
- Failure Investigation

## Single Lab Guide

### Azure Functions Monitoring with Application Insights

This repository now contains one comprehensive, end-to-end lab that covers the full monitoring workflow in a single place.

It includes:

- creating monitoring resources
- connecting Azure Functions to Application Insights
- generating and validating telemetry
- investigating failures with KQL
- tracing dependencies
- configuring alerts and health monitoring
- creating an Azure Operations Dashboard

Start here: [Complete Lab Guide](docs/Lab-Complete-Guide.md).

## Prerequisites

- Azure Subscription
- Azure Portal access
- Visual Studio Code
- Azure Functions Core Tools v4
- .NET 8 SDK

## Repository Structure

```text
azure-functions-appinsights-labs
├── docs/
├── HealthCheck/
├── Program.cs
├── azure-functions-appinsights-labs.csproj
├── host.json
├── local.settings.json
└── README.md
```

## Technologies

- Azure Functions (.NET 8 Isolated)
- Azure Monitor
- Application Insights
- Log Analytics
- KQL