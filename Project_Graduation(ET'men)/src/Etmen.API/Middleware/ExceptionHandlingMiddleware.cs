using System.Net;
using System.Text.Json;

namespace Etmen.API.Middleware;

/// <summary>
/// Global exception handler — catches unhandled exceptions and returns
/// a consistent ProblemDetails JSON response. Prevents stack traces leaking to clients.
/// </summary>
public class ExceptionHandlingMiddleware
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
        throw new NotImplementedException();
    }
}
