using Etmen.Domain.Common;
using Etmen.Domain.Enums;

namespace Etmen.Domain.Entities;

// ════════════════════════════════════════════════════════════════════════════
// HealthcareProvider  (NEW — v3.0 Nearby Finder)
// ════════════════════════════════════════════════════════════════════════════

/// <summary>
/// Represents a registered or Google-Places-sourced healthcare provider
/// (hospital, clinic, pharmacy, or lab). Used by the Nearby Doctor & Hospital Finder feature.
/// IsRegistered = true means the provider signed up on the platform.
/// </summary>
public class HealthcareProvider : BaseEntity
{
    public string Name { get; private set; } = default!;
    public ProviderType Type { get; private set; }
    public string? Specialty { get; private set; }
    public string Address { get; private set; } = default!;
    public double Latitude { get; private set; }
    public double Longitude { get; private set; }
    public string? Phone { get; private set; }

    /// <summary>Google Places ID — null for platform-registered providers.</summary>
    public string? GooglePlaceId { get; private set; }
    public double Rating { get; private set; }

    /// <summary>True if the provider has an account on the platform (preferred in match score).</summary>
    public bool IsRegistered { get; private set; }

    // Navigation
    public ICollection<AvailableSlot> AvailableSlots { get; private set; } = new List<AvailableSlot>();
    public ICollection<Appointment> Appointments { get; private set; } = new List<Appointment>();

    public static HealthcareProvider Create(
        string name, ProviderType type, string? specialty, string address,
        double latitude, double longitude, string? phone,
        string? googlePlaceId, double rating, bool isRegistered)
    {
        throw new NotImplementedException();
    }
}

// ════════════════════════════════════════════════════════════════════════════
// AvailableSlot  (NEW — v3.0)
// ════════════════════════════════════════════════════════════════════════════

/// <summary>
/// A bookable time slot belonging to a HealthcareProvider.
/// IsBooked is set to true when a patient confirms an appointment.
/// </summary>
public class AvailableSlot : BaseEntity
{
    public Guid ProviderId { get; private set; }
    public DateTime SlotStart { get; private set; }
    public DateTime SlotEnd { get; private set; }
    public bool IsBooked { get; private set; }

    // Navigation
    public HealthcareProvider Provider { get; private set; } = default!;

    public static AvailableSlot Create(Guid providerId, DateTime slotStart, DateTime slotEnd)
    {
        throw new NotImplementedException();
    }

    /// <summary>Marks the slot as booked. Throws if already booked.</summary>
    public void Book()
    {
        throw new NotImplementedException();
    }
}

// ════════════════════════════════════════════════════════════════════════════
// Appointment  (NEW — v3.0)
// ════════════════════════════════════════════════════════════════════════════

/// <summary>
/// Represents a patient booking with a healthcare provider,
/// optionally linked to the RiskAssessment that triggered the referral.
/// </summary>
public class Appointment : BaseEntity
{
    public Guid PatientId { get; private set; }
    public Guid ProviderId { get; private set; }
    public Guid? AssessmentId { get; private set; }
    public DateTime AppointmentAt { get; private set; }
    public AppointmentStatus Status { get; private set; } = AppointmentStatus.Pending;

    /// <summary>True if the appointment was triggered by a HIGH risk alert.</summary>
    public bool IsEmergency { get; private set; }

    // Navigation
    public PatientProfile Patient { get; private set; } = default!;
    public HealthcareProvider Provider { get; private set; } = default!;
    public RiskAssessment? Assessment { get; private set; }

    public static Appointment Create(
        Guid patientId, Guid providerId, Guid? assessmentId,
        DateTime appointmentAt, bool isEmergency)
    {
        throw new NotImplementedException();
    }

    public void Cancel()
    {
        throw new NotImplementedException();
    }

    public void Confirm()
    {
        throw new NotImplementedException();
    }
}

// ════════════════════════════════════════════════════════════════════════════
// LabResult  (NEW — v3.0 OCR Upload)
// ════════════════════════════════════════════════════════════════════════════

/// <summary>
/// Stores the output of the OCR pipeline after a patient uploads a lab test document.
/// Extracted key values (HbA1c, blood sugar, lipid panel, kidney function) are saved here.
/// If values differ from the last manual entry, a new MedicalRecord + RiskAssessment is triggered.
/// </summary>
public class LabResult : BaseEntity
{
    public Guid PatientId { get; private set; }

    /// <summary>Azure Blob / local storage URL of the uploaded file.</summary>
    public string FileUrl { get; private set; } = default!;
    public string? FileName { get; private set; }

    // Extracted values (nullable — OCR may not detect all fields)
    public double? HbA1c { get; private set; }
    public double? BloodSugar { get; private set; }
    public double? Cholesterol { get; private set; }
    public double? Triglycerides { get; private set; }
    public double? Creatinine { get; private set; }

    /// <summary>Raw OCR text returned by the OCR engine, stored for audit/debugging.</summary>
    public string? RawOcrText { get; private set; }

    /// <summary>True if the extracted values were different enough to trigger a re-assessment.</summary>
    public bool TriggeredReassessment { get; private set; }

    /// <summary>Linked assessment if TriggeredReassessment = true.</summary>
    public Guid? LinkedAssessmentId { get; private set; }

    public static LabResult Create(Guid patientId, string fileUrl, string? fileName)
    {
        throw new NotImplementedException();
    }

    /// <summary>Populates extracted values after the OCR engine completes.</summary>
    public void ApplyOcrResults(
        string rawText,
        double? hbA1c, double? bloodSugar,
        double? cholesterol, double? triglycerides, double? creatinine)
    {
        throw new NotImplementedException();
    }

    /// <summary>Marks this lab result as having triggered a new risk assessment.</summary>
    public void MarkTriggeredReassessment(Guid assessmentId)
    {
        throw new NotImplementedException();
    }
}

// ════════════════════════════════════════════════════════════════════════════
// FamilyLink  (NEW — v3.0 Family Account Linking)
// ════════════════════════════════════════════════════════════════════════════

/// <summary>
/// Represents a verified link between two user accounts.
/// The PrimaryUser can view (and optionally manage) the LinkedUser's health data.
/// An InviteToken is generated on invite and cleared upon acceptance.
/// </summary>
public class FamilyLink : BaseEntity
{
    public Guid PrimaryUserId { get; private set; }
    public Guid LinkedUserId { get; private set; }
    public FamilyRelationship Relationship { get; private set; }

    /// <summary>Primary user can view the linked member's risk data.</summary>
    public bool CanView { get; private set; } = true;

    /// <summary>Primary user can book appointments on behalf of the linked member.</summary>
    public bool CanManage { get; private set; } = false;

    /// <summary>One-time invite token. Null after acceptance.</summary>
    public string? InviteToken { get; private set; }

    // Navigation
    public User PrimaryUser { get; private set; } = default!;
    public User LinkedUser { get; private set; } = default!;

    public static FamilyLink CreateInvite(
        Guid primaryUserId,
        FamilyRelationship relationship,
        bool canManage,
        string inviteToken)
    {
        throw new NotImplementedException();
    }

    /// <summary>Accepts the invite, sets the LinkedUserId, and clears the token.</summary>
    public void Accept(Guid linkedUserId)
    {
        throw new NotImplementedException();
    }

    public void Delete() => SoftDelete();
}
