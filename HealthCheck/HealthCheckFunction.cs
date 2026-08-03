using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace AzureFunctionsAppInsightsLabs.HealthCheck;

public sealed class HealthCheckFunction
{
    private readonly ILogger<HealthCheckFunction> _logger;

    public HealthCheckFunction(ILogger<HealthCheckFunction> logger)
    {
        _logger = logger;
    }

    [Function("HealthCheck")]
    public IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest request)
    {
        _logger.LogInformation("HealthCheck Function Executed");

        return new OkObjectResult(new
        {
            Status = "Healthy",
            Message = "Telemetry validation successful",
            Timestamp = DateTime.UtcNow.ToString("o")
        });
    }
}