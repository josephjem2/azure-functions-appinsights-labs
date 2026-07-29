# Azure Functions + Application Insights Labs

Hands-on Azure Monitor and Application Insights labs using Azure Functions and .NET 8.

## Overview

This repository contains practical labs focused on:

- Azure Functions
- Application Insights
- Azure Monitor
- Log Analytics
- Kusto Query Language (KQL)
- Telemetry Correlation
- Failure Investigation

## Labs

### Lab 1 – Telemetry Health Check

Validate:

- Application Insights connectivity
- Requests
- Traces
- Exceptions
- Live Metrics
- Telemetry correlation using operation_Id

See [Lab 01](docs/Lab01-Telemetry-Health-Check.md).

### Lab 2 – Known Failure Investigation

Investigate:

- Failed requests
- Exceptions
- Dependency failures
- Transaction timelines
- Failure boundaries

See [Lab 02](docs/Lab02-Known-Failure-Investigation.md).

### Lab 3 – KQL Fundamentals

Learn:

- basic KQL syntax
- filtering telemetry
- summarizing requests and failures
- building simple investigation queries

See [Lab 03](docs/Lab03-KQL-Fundamentals.md).

### Lab 4 – Dependency Tracing

Explore:

- dependency telemetry
- failed outbound calls
- slow dependencies
- correlation with requests and exceptions

See [Lab 04](docs/Lab04-Dependency-Tracing.md).

### Lab 5 – Alerts and Health Monitoring

Understand:

- Azure Monitor alerts
- health signals and availability
- alert thresholds and notifications
- proactive monitoring practices

See [Lab 05](docs/Lab05-Alerts-and-Health-Monitoring.md).

## Prerequisites

- Azure Subscription
- Azure Portal access
- Visual Studio Code
- Azure Functions Core Tools v4
- .NET 8 SDK

## Repository Structure

```text
azure-functions-appinsights-labs
│
├── docs
├── images
├── kql
├── src
│
└── README.md
```

## Technologies

- Azure Functions (.NET 8 Isolated)
- Azure Monitor
- Application Insights
- Log Analytics
- KQL