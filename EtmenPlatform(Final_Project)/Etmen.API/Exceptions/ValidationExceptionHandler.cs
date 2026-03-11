using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Etmen.API.Exceptions;

/// <summary>
/// Handles FluentValidation.ValidationException specifically.
/// Returns HTTP 422 with per-field error details in ProblemDetails format.
/// Must be registered BEFORE GlobalExceptionHandler in DI.
/// </summary>
public sealed class ValidationExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext context,
        Exception   exception,
        CancellationToken ct)
    {
        if (exception is not ValidationException validationEx)
            return false;

        var errors = validationEx.Errors
            .GroupBy(e => e.PropertyName)
            .ToDictionary(
                g => g.Key,
                g => g.Select(e => e.ErrorMessage).ToArray());

        var problem = new ValidationProblemDetails(errors)
        {
            Status   = StatusCodes.Status422UnprocessableEntity,
            Title    = "Validation Failed",
            Detail   = "One or more validation errors occurred.",
            Instance = context.Request.Path
        };

        context.Response.StatusCode  = StatusCodes.Status422UnprocessableEntity;
        context.Response.ContentType = "application/problem+json";

        await context.Response.WriteAsJsonAsync(problem, ct);
        return true;
    }
}
