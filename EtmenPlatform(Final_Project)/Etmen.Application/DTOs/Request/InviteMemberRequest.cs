namespace Etmen.Application.DTOs.Request;

/// <summary>Request DTO for the InviteMemberRequest operation.</summary>
public sealed class InviteMemberRequest
{
    public Guid   PrimaryUserId { get; set; }
    public string Email         { get; set; } = string.Empty;
    public string Relationship  { get; set; } = string.Empty;
}
