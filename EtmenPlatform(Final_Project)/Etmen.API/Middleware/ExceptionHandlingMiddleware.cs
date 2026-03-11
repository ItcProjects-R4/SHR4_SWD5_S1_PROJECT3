using System.Net;
using System.Text.Json;

namespace Etmen.API.Middleware;

/// <summary>
/// Global exception handler middleware.
/// Catches unhandled exceptions, logs them, and returns a structured JSON error response.
/// Prevents stack traces from leaking to clients in production.
/// </summary>
public sealed class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next   = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (NotImplementedException ex)
        {
            _logger.LogWarning(ex, "Not implemented endpoint hit.");
            context.Response.StatusCode = (int)HttpStatusCode.NotImplemented;
            await WriteJsonAsync(context, 501, "This endpoint is not yet implemented.");
        }
        catch (ArgumentException ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            await WriteJsonAsync(context, 400, ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception.");
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await WriteJsonAsync(context, 500, "An unexpected error occurred.");
        }
    }

    private static Task WriteJsonAsync(HttpContext ctx, int status, string message)
    {
        ctx.Response.ContentType = "application/json";
        var payload = JsonSerializer.Serialize(new { status, message });
        return ctx.Response.WriteAsync(payload);
    }
}
