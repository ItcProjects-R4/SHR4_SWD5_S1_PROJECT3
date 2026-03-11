namespace Etmen.Application.DTOs.Request;

/// <summary>Request DTO for the RegisterRequest operation.</summary>
public sealed class RegisterRequest
{
    public string FullName    { get; set; } = string.Empty;
    public string Email       { get; set; } = string.Empty;
    public string Password    { get; set; } = string.Empty;
    public string Role        { get; set; } = "Patient"; // Patient | Doctor | Admin
    public DateTime DateOfBirth { get; set; }
    public string Gender      { get; set; } = string.Empty;
}
