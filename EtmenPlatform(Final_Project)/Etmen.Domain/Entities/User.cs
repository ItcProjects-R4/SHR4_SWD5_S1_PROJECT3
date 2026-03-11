using Etmen.Domain.Common;
using Etmen.Domain.Enums;

namespace Etmen.Domain.Entities;

/// <summary>
/// Core authentication entity. Owns PatientProfile or DoctorProfile via navigation.
/// Supports both email/password and Google OAuth2 login.
/// </summary>
public class User : BaseEntity
{
    public string FullName            { get; private set; } = string.Empty;
    public string Email               { get; private set; } = string.Empty;
    public string PasswordHash        { get; private set; } = string.Empty;
    public string? GoogleId           { get; private set; }
    public UserRole Role              { get; private set; }
    public string? RefreshToken       { get; private set; }
    public DateTime? RefreshTokenExpiry { get; private set; }

    // Navigation
    public PatientProfile? PatientProfile { get; private set; }
    public DoctorProfile?  DoctorProfile  { get; private set; }

    protected User() { }

    /// <summary>Factory: create a new user with hashed password.</summary>
    public static User Create(string fullName, string email, string passwordHash, UserRole role)
        => new() { FullName = fullName, Email = email, PasswordHash = passwordHash, Role = role };

    /// <summary>Factory: create or update a user via Google OAuth.</summary>
    public static User CreateFromGoogle(string fullName, string email, string googleId)
        => new() { FullName = fullName, Email = email, GoogleId = googleId, Role = UserRole.Patient };

    public void SetRefreshToken(string token, DateTime expiry)
    {
        RefreshToken = token;
        RefreshTokenExpiry = expiry;
        MarkUpdated();
    }

    public void RevokeRefreshToken()
    {
        RefreshToken = null;
        RefreshTokenExpiry = null;
        MarkUpdated();
    }
}
