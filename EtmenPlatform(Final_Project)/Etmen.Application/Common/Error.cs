namespace Etmen.Application.Common;

/// <summary>
/// Represents a structured error with a code, message, and HTTP-mapped type.
/// Used with Result&lt;T&gt; to avoid throwing exceptions for expected business failures.
/// </summary>
public sealed record Error
{
    public static readonly Error None = new(string.Empty, string.Empty, ErrorType.None);

    public string    Code    { get; }
    public string    Message { get; }
    public ErrorType Type    { get; }

    private Error(string code, string message, ErrorType type)
    {
        Code    = code;
        Message = message;
        Type    = type;
    }

    // ── Factory methods ───────────────────────────────────────────

    /// <summary>HTTP 404 — resource does not exist.</summary>
    public static Error NotFound(string resource, Guid id)
        => new($"{resource}.NotFound", $"{resource} with ID '{id}' was not found.", ErrorType.NotFound);

    /// <summary>HTTP 400 — caller sent invalid input.</summary>
    public static Error Validation(string field, string message)
        => new($"Validation.{field}", message, ErrorType.Validation);

    /// <summary>HTTP 409 — business rule conflict (e.g. duplicate email).</summary>
    public static Error Conflict(string code, string message)
        => new(code, message, ErrorType.Conflict);

    /// <summary>HTTP 401 — caller is not authenticated.</summary>
    public static Error Unauthorized(string message = "غير مصرح.")
        => new("Auth.Unauthorized", message, ErrorType.Unauthorized);

    /// <summary>HTTP 403 — caller is authenticated but not allowed.</summary>
    public static Error Forbidden(string message = "ليس لديك صلاحية.")
        => new("Auth.Forbidden", message, ErrorType.Forbidden);

    /// <summary>HTTP 500 — unexpected internal failure.</summary>
    public static Error Unexpected(string message)
        => new("Unexpected", message, ErrorType.Unexpected);

    public override string ToString() => $"[{Type}] {Code}: {Message}";
}

/// <summary>Maps to HTTP status codes in the exception handler.</summary>
public enum ErrorType
{
    None        = 0,
    Validation  = 400,
    Unauthorized= 401,
    Forbidden   = 403,
    NotFound    = 404,
    Conflict    = 409,
    Unexpected  = 500
}
