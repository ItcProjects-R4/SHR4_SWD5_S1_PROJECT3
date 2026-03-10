using Etmen.Domain.Common;
using Etmen.Domain.Enums;

namespace Etmen.Domain.Entities;

/// <summary>
/// Core identity entity — stores credentials, role, and OAuth data.
/// Owns navigation properties to PatientProfile and DoctorProfile.
/// </summary>
public class User : BaseEntity
{
    public string   FullName             { get; private set; } = default!;
    public string   Email                { get; private set; } = default!;
    public string   PasswordHash         { get; private set; } = default!;
    public string?  GoogleId             { get; private set; }
    public UserRole Role                 { get; private set; }
    public string?  RefreshToken         { get; private set; }
    public DateTime? RefreshTokenExpiry  { get; private set; }

    // Navigation
    public PatientProfile? PatientProfile { get; private set; }
    public DoctorProfile?  DoctorProfile  { get; private set; }

    private User() { }

    /// <summary>Factory method for email/password registration.</summary>
    public static User Create(string fullName, string email,
                              string passwordHash, UserRole role)
    {
        throw new NotImplementedException();
    }

    /// <summary>Factory method for Google OAuth2 registration.</summary>
    public static User CreateFromGoogle(string fullName, string email, string googleId)
    {
        throw new NotImplementedException();
    }

    /// <summary>Stores a new refresh token and its expiry date.</summary>
    public void SetRefreshToken(string token, DateTime expiry)
    {
        throw new NotImplementedException();
    }

    /// <summary>Invalidates the current refresh token (logout).</summary>
    public void RevokeRefreshToken()
    {
        throw new NotImplementedException();
    }
}
