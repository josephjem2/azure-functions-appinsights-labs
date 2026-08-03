using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace AzureFunctionsAppInsightsLabs.HealthCheck;

public sealed class TelemetryDemoFunction
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<TelemetryDemoFunction> _logger;

    public TelemetryDemoFunction(IHttpClientFactory httpClientFactory, ILogger<TelemetryDemoFunction> logger)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }

    [Function("GenerateTrace")]
    public IActionResult GenerateTrace([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "demo/trace")] HttpRequest request)
    {
        var message = request.Query["message"].ToString();
        if (string.IsNullOrWhiteSpace(message))
        {
            message = "Manual trace generated for Application Insights validation.";
        }

        _logger.LogInformation("Trace demo invoked. Message: {Message}", message);

        return new OkObjectResult(new
        {
            Status = "TraceCreated",
            Message = message,
            OperationId = Activity.Current?.RootId ?? Activity.Current?.Id,
            Timestamp = DateTime.UtcNow.ToString("o")
        });
    }

    [Function("CallDependency")]
    public async Task<IActionResult> CallDependencyAsync([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "demo/dependency")] HttpRequest request)
    {
        var dependencyUrl = request.Query["url"].ToString();
        if (string.IsNullOrWhiteSpace(dependencyUrl))
        {
            dependencyUrl = "https://httpbin.org/get";
        }

        var stopwatch = Stopwatch.StartNew();
        var client = _httpClientFactory.CreateClient();

        using var response = await client.GetAsync(dependencyUrl);
        stopwatch.Stop();

        _logger.LogInformation(
            "Dependency call completed. Url: {DependencyUrl}, StatusCode: {StatusCode}, DurationMs: {DurationMs}",
            dependencyUrl,
            (int)response.StatusCode,
            stopwatch.ElapsedMilliseconds);

        return new OkObjectResult(new
        {
            Status = "DependencyCallCompleted",
            Url = dependencyUrl,
            HttpStatusCode = (int)response.StatusCode,
            DurationMs = stopwatch.ElapsedMilliseconds,
            Timestamp = DateTime.UtcNow.ToString("o")
        });
    }

    [Function("SimulateFailure")]
    public IActionResult SimulateFailure([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "demo/failure")] HttpRequest request)
    {
        var scenario = request.Query["scenario"].ToString();
        if (string.Equals(scenario, "timeout", StringComparison.OrdinalIgnoreCase))
        {
            throw new TimeoutException("Intentional timeout exception for telemetry demo.");
        }

        throw new InvalidOperationException("Intentional demo exception for Application Insights validation.");
    }
}
