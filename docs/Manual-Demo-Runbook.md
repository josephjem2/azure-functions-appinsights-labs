# Manual Demo Runbook - Azure Functions + Application Insights

This runbook is designed for a live demo where you want to show end-to-end observability in a controlled way.

## What you will learn

At the end of the runbook, you will have demonstrated:

- healthy request telemetry
- trace telemetry with operation correlation
- dependency telemetry from outbound HTTP calls
- exception telemetry from controlled failures
- KQL-based troubleshooting and performance analysis

This runbook aligns with the full guide at [docs/Lab-Complete-Guide.md](docs/Lab-Complete-Guide.md).

## Phase 1 - Local Prerequisites

### Objective

Validate local tooling before starting Azure Functions telemetry validation.

Run:

```powershell
./scripts/check-prereqs.ps1
```

Verify:

- Azure Functions Core Tools v4
- .NET 8 SDK
- Azure CLI in PATH

## Phase 2 - Azure Sign-In and Subscription Context

### Objective

Establish the correct tenant and subscription context for all Azure operations.

```powershell
az login --tenant <tenant-id> --use-device-code
az account set --subscription <subscription-id>
az account show --output table
```

## Phase 3 - Configure Local Application Insights Connection

### Objective

Connect local .NET isolated Functions execution to the target Application Insights resource.

Retrieve connection string:

```powershell
az monitor app-insights component show `
  --app app-insights-demo `
  --resource-group MonitoredAssets `
  --query connectionString -o tsv
```

Open local.settings.json and set:

- `Values.APPLICATIONINSIGHTS_CONNECTION_STRING` to the retrieved value
- Ensure `Values.AzureWebJobsStorage` is valid for local execution

## Phase 4 - Start Local Host

### Objective

Start the local function host and confirm all demo endpoints are available.

```powershell
dotnet run
```

Expected endpoints:

- `GET /api/HealthCheck`
- `GET /api/demo/trace`
- `GET /api/demo/dependency`
- `GET /api/demo/failure`

## Phase 5 - Generate Demo Telemetry

### Objective

Produce representative request, trace, dependency, and exception telemetry.

In another terminal:

```powershell
./scripts/generate-traffic.ps1
```

## Phase 6 - Validate Telemetry in Application Insights

### Objective

Validate all telemetry categories using reusable KQL queries.

Use queries from the `kql` folder in this order:

1. `kql/01-requests-overview.kql`
2. `kql/02-request-trend.kql`
3. `kql/03-exceptions-details.kql`
4. `kql/04-dependencies-health.kql`
5. `kql/05-traces-by-operation.kql`
6. `kql/06-endpoint-performance-scorecard.kql`

Expected signal categories after script execution:

- requests
- traces
- dependencies
- exceptions

## Phase 7 - Suggested Narration for Live Demo

### Objective

Use consistent narration that maps each endpoint to a telemetry outcome.

1. "HealthCheck shows baseline request success and latency."
2. "GenerateTrace adds explicit application logs to traces."
3. "CallDependency produces dependency telemetry and duration metrics."
4. "SimulateFailure creates exception records used for root-cause analysis."
5. "KQL ties these signals together by operation ID for end-to-end diagnostics."

## Phase 8 - Cleanup

### Objective

Stop local execution cleanly and preserve local-only configuration safety.

- Stop the function host with `Ctrl+C`.
- Keep local.settings.json out of source control.
