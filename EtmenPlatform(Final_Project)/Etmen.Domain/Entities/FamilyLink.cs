using Etmen.Domain.Common;

namespace Etmen.Domain.Entities;

/// <summary>
/// Links a primary account owner to a family member's account.
/// CanManage = true allows the primary user to book appointments for the linked member.
/// </summary>
public sealed class FamilyLink : BaseEntity
{
    public Guid    PrimaryUserId  { get; private set; }
    public Guid    LinkedUserId   { get; private set; }
    public string  Relationship   { get; private set; } = string.Empty; // Parent/Child/Spouse/Sibling/Caregiver
    public bool    CanView        { get; private set; } = true;
    public bool    CanManage      { get; private set; } = false;
    public string? InviteToken    { get; private set; }

    protected FamilyLink() { }

    public static FamilyLink Create(Guid primaryUserId, Guid linkedUserId, string relationship)
        => new() { PrimaryUserId = primaryUserId, LinkedUserId = linkedUserId, Relationship = relationship };

    public static FamilyLink CreatePending(Guid primaryUserId, string relationship, string inviteToken)
        => new() { PrimaryUserId = primaryUserId, Relationship = relationship, InviteToken = inviteToken };

    public void Accept(Guid linkedUserId) { LinkedUserId = linkedUserId; InviteToken = null; MarkUpdated(); }
    public void GrantManagement()         { CanManage = true; MarkUpdated(); }
    public void RevokeManagement()        { CanManage = false; MarkUpdated(); }
}
