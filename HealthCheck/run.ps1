using namespace System.Net

param(
    $Request,
    $TriggerMetadata
)

Write-Host "HealthCheck Function Executed"

$body = @{
    Status = "Healthy"
    Message = "Telemetry validation successful"
    Timestamp = (Get-Date).ToUniversalTime().ToString("o")
}

Push-OutputBinding -Name Response -Value (
    [HttpResponseContext]@{
        StatusCode = [HttpStatusCode]::OK
        Body = ($body | ConvertTo-Json)
    }
)