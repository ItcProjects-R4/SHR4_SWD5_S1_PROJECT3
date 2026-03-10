namespace Etmen.API.Middleware;

/// <summary>
/// Logs every incoming HTTP request with method, path, status code, and duration.
/// Uses structured logging for compatibility with Serilog / Application Insights.
/// </summary>
public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;

    public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
    {
        _next   = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        throw new NotImplementedException();
    }
}
