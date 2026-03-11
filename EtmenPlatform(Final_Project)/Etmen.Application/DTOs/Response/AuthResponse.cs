namespace Etmen.Application.DTOs.Response;

/// <summary>Response DTO returned by the AuthResponse operation.</summary>
public sealed class AuthResponse
{
    public string AccessToken  { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public DateTime ExpiresAt  { get; set; }
    public Guid   UserId       { get; set; }
    public string Role         { get; set; } = string.Empty;
    public string FullName     { get; set; } = string.Empty;
    public List<Guid> ManagedProfiles { get; set; } = new(); // Family linked profiles
}
