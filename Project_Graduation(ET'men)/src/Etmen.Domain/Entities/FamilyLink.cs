using Etmen.Domain.Common;

namespace Etmen.Domain.Entities;

/// <summary>
/// Join entity linking a primary account holder to a family member's account.
/// Enables the Profile Switcher feature — primary user can view/manage linked profiles.
/// </summary>
public sealed class FamilyLink : BaseEntity
{
    public Guid   PrimaryUserId { get; private set; } // account owner
    public Guid   LinkedUserId  { get; private set; } // family member
    public string Relationship  { get; private set; } = default!; // Parent, Child, Spouse, Sibling, Caregiver
    public bool   CanView       { get; private set; } = true;
    public bool   CanManage     { get; private set; } = false; // book appointments on behalf
    public string? InviteToken  { get; private set; } // cleared after acceptance

    private FamilyLink() { }

    public static FamilyLink Create(Guid primaryUserId, Guid linkedUserId, string relationship)
    {
        throw new NotImplementedException();
    }

    /// <summary>Grants the primary account holder the ability to book appointments for the linked member.</summary>
    public void GrantManagement() { throw new NotImplementedException(); }

    /// <summary>Clears the invite token once the link is accepted.</summary>
    public void AcceptInvite()    { throw new NotImplementedException(); }
}
