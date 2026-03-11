using Etmen.Application.Common;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Etmen.API.Exceptions;

/// <summary>
/// Global exception handler using .NET 8+ IExceptionHandler interface.
/// Maps domain Error types to correct HTTP status codes automatically.
/// Registered via app.UseExceptionHandler() in Program.cs.
/// Replaces the old ExceptionHandlingMiddleware pattern.
/// </summary>
public sealed class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        => _logger = logger;

    public async ValueTask<bool> TryHandleAsync(
        HttpContext context,
        Exception   exception,
        CancellationToken ct)
    {
        _logger.LogError(exception, "Unhandled exception: {Message}", exception.Message);

        var (statusCode, title) = exception switch
        {
            NotImplementedException => (501, "Not Implemented"),
            ArgumentNullException   => (400, "Bad Request"),
            ArgumentException       => (400, "Bad Request"),
            UnauthorizedAccessException => (401, "Unauthorized"),
            KeyNotFoundException    => (404, "Not Found"),
            InvalidOperationException e when e.Message.Contains("not found", StringComparison.OrdinalIgnoreCase)
                                    => (404, "Not Found"),
            _                       => (500, "Internal Server Error")
        };

        var problem = new ProblemDetails
        {
            Status = statusCode,
            Title  = title,
            Detail = exception.Message,
            Instance = context.Request.Path
        };

        problem.Extensions["traceId"] = context.TraceIdentifier;

        context.Response.StatusCode  = statusCode;
        context.Response.ContentType = "application/problem+json";

        await context.Response.WriteAsJsonAsync(problem, ct);
        return true;
    }
}
