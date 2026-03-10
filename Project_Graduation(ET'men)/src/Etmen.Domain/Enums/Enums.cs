namespace Etmen.Domain.Enums;

/// <summary>Categorises the computed ML risk probability into a discrete clinical level.</summary>
public enum RiskLevel
{
    Low = 0,
    Medium = 1,
    High = 2
}

/// <summary>Defines the role of a registered user, controlling access to API endpoints.</summary>
public enum UserRole
{
    Patient = 0,
    Doctor = 1,
    Admin = 2
}

/// <summary>Self-reported physical activity used as an input feature for the ML model.</summary>
public enum PhysicalActivityLevel
{
    Sedentary = 0,
    Light = 1,
    Moderate = 2,
    Active = 3,
    VeryActive = 4
}

/// <summary>Lifecycle state of a system-generated alert sent to patients or doctors.</summary>
public enum AlertStatus
{
    Unread = 0,
    Read = 1,
    Dismissed = 2
}

/// <summary>Type of healthcare provider returned by the Nearby Finder feature.</summary>
public enum ProviderType
{
    Hospital = 0,
    Clinic = 1,
    Pharmacy = 2,
    Laboratory = 3
}

/// <summary>Status of a patient appointment with a healthcare provider.</summary>
public enum AppointmentStatus
{
    Pending = 0,
    Confirmed = 1,
    Cancelled = 2,
    Completed = 3
}

/// <summary>Relationship type used by the Family Account Linking feature.</summary>
public enum FamilyRelationship
{
    Parent = 0,
    Child = 1,
    Spouse = 2,
    Sibling = 3,
    Caregiver = 4
}
