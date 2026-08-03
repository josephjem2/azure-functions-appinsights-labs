# Azure Functions + Application Insights Labs

Hands-on observability demo for Azure Functions using .NET 8 isolated, Application Insights, Azure Monitor, and KQL.

## Demo Objective

This repository is optimized for a complete monitoring demo. It intentionally produces four telemetry categories so you can validate operational visibility end to end:

- `requests` from the HealthCheck endpoint
- `traces` from explicit structured logs
- `dependencies` from outbound HTTP calls
- `exceptions` from controlled failure scenarios

## Quick start

1. Run prerequisites check:

```powershell
./scripts/check-prereqs.ps1
```

2. Sign in and set subscription:

```powershell
az login --tenant b61bdd26-f7b1-4289-a1ed-8a84e24b7cb2 --use-device-code
az account set --subscription f35793ab-d83c-4be0-a1a5-d2da45f53bcc
```

3. Get Application Insights connection string:

```powershell
az monitor app-insights component show --app app-insights-demo --resource-group MonitoredAssets --query connectionString -o tsv
```

4. Update `local.settings.json` with the value from step 3:

- `Values.APPLICATIONINSIGHTS_CONNECTION_STRING`
- `Values.AzureWebJobsStorage` should remain valid (`UseDevelopmentStorage=true` for local with Azurite)

5. Start the function host:

```powershell
dotnet run
```

6. Generate demo telemetry in a second terminal:

```powershell
./scripts/generate-traffic.ps1
```

7. Validate telemetry using the KQL files in the `kql` folder.

## Endpoints in this demo

| Endpoint | Purpose | Telemetry generated |
| --- | --- | --- |
| `GET /api/HealthCheck` | Baseline availability check | requests, traces |
| `GET /api/demo/trace` | Manual trace generation | traces |
| `GET /api/demo/dependency` | Outbound HTTP dependency call | dependencies, traces |
| `GET /api/demo/failure` | Intentional exception for testing | exceptions, failed requests |

## KQL packs

Use these ready-made KQL files in Application Insights Logs:

1. `kql/01-requests-overview.kql`
2. `kql/02-request-trend.kql`
3. `kql/03-exceptions-details.kql`
4. `kql/04-dependencies-health.kql`
5. `kql/05-traces-by-operation.kql`
6. `kql/06-endpoint-performance-scorecard.kql`

## Detailed runbooks

- Full lab walkthrough: [docs/Lab-Complete-Guide.md](docs/Lab-Complete-Guide.md)
- Manual live-demo playbook: [docs/Manual-Demo-Runbook.md](docs/Manual-Demo-Runbook.md)

Recommended flow:

1. Follow [docs/Manual-Demo-Runbook.md](docs/Manual-Demo-Runbook.md) for live execution.
2. Use [docs/Lab-Complete-Guide.md](docs/Lab-Complete-Guide.md) for full narrative, dashboard, workbook, and alerting phases.

## Repository structure

```text
azure-functions-appinsights-labs
├── docs/
│   ├── Lab-Complete-Guide.md
│   └── Manual-Demo-Runbook.md
├── HealthCheck/
│   ├── HealthCheckFunction.cs
│   └── TelemetryDemoFunction.cs
├── kql/
├── scripts/
├── Program.cs
├── azure-functions-appinsights-labs.csproj
├── host.json
├── local.settings.json
└── README.md
```

## Notes

- `local.settings.json` stays local and should not contain production secrets in source control.
- If you see a local warning about `AzureWebJobsStorage`, start Azurite or point `AzureWebJobsStorage` to a valid storage account connection string.