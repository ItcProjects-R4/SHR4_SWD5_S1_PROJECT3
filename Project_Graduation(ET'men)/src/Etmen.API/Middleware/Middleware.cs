using Etmen.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Net;
using System.Text.Json;

namespace Etmen.API.Middleware;

// ════════════════════════════════════════════════════════════════════════════
// ExceptionHandlingMiddleware
// ════════════════════════════════════════════════════════════════════════════

/// <summary>
/// Global exception handler. Catches unhandled exceptions, logs them,
/// and returns a consistent ErrorResponse JSON envelope with an appropriate HTTP status.
/// Registered in Program.cs before all other middleware.
/// </summary>
public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        throw new NotImplementedException();
    }

    /// <summary>Maps exception types to HTTP status codes and writes the JSON error envelope.</summary>
    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        throw new NotImplementedException();
    }
}

// ════════════════════════════════════════════════════════════════════════════
// RequestLoggingMiddleware
// ════════════════════════════════════════════════════════════════════════════

/// <summary>
/// Logs each incoming HTTP request (method, path, status code, duration).
/// Registered in Program.cs after ExceptionHandlingMiddleware.
/// </summary>
public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;

    public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        throw new NotImplementedException();
    }
}
