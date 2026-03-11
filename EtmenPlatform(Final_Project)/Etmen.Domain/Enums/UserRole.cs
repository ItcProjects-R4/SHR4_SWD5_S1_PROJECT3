namespace Etmen.Domain.Enums;

/// <summary>Roles used in JWT claims and [Authorize] attributes.</summary>
public enum UserRole
{
    Patient = 0,
    Doctor  = 1,
    Admin   = 2
}
