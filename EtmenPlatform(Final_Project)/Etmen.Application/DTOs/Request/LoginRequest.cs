namespace Etmen.Application.DTOs.Request;

/// <summary>Request DTO for the LoginRequest operation.</summary>
public sealed class LoginRequest
{
    public string Email    { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
