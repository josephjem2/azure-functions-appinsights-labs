[CmdletBinding()]
param(
    [string]$BaseUrl = "http://localhost:7071/api",
    [int]$HealthCheckCount = 25,
    [int]$TraceCount = 5,
    [int]$DependencyCount = 3,
    [int]$FailureCount = 2
)

$ErrorActionPreference = "Stop"

function Invoke-DemoCall {
    param(
        [Parameter(Mandatory = $true)]
        [string]$Url,
        [switch]$ExpectFailure
    )

    try {
        $response = Invoke-RestMethod -Uri $Url -Method Get -TimeoutSec 30
        Write-Host "OK  $Url" -ForegroundColor Green
        return $response
    }
    catch {
        if ($ExpectFailure) {
            Write-Host "EXPECTED FAILURE  $Url" -ForegroundColor Yellow
            return $null
        }

        Write-Host "FAILED  $Url" -ForegroundColor Red
        throw
    }
}

Write-Host "Generating HealthCheck traffic..." -ForegroundColor Cyan
1..$HealthCheckCount | ForEach-Object {
    Invoke-DemoCall -Url "$BaseUrl/HealthCheck" | Out-Null
}

Write-Host "Generating trace telemetry..." -ForegroundColor Cyan
1..$TraceCount | ForEach-Object {
    Invoke-DemoCall -Url "$BaseUrl/demo/trace?message=trace-$($_)" | Out-Null
}

Write-Host "Generating dependency telemetry..." -ForegroundColor Cyan
1..$DependencyCount | ForEach-Object {
    Invoke-DemoCall -Url "$BaseUrl/demo/dependency" | Out-Null
}

Write-Host "Generating controlled failures..." -ForegroundColor Cyan
1..$FailureCount | ForEach-Object {
    Invoke-DemoCall -Url "$BaseUrl/demo/failure" -ExpectFailure | Out-Null
}

Write-Host "Demo telemetry generation completed." -ForegroundColor Green
