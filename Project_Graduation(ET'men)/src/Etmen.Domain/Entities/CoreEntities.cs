using Etmen.Domain.Common;
using Etmen.Domain.Enums;

namespace Etmen.Domain.Entities;

// ════════════════════════════════════════════════════════════════════════════
// DoctorProfile
// ════════════════════════════════════════════════════════════════════════════

/// <summary>
/// Profile for users with Role=Doctor. Stores medical speciality and
/// is referenced as the assigned clinician on patient records.
/// </summary>
public class DoctorProfile : BaseEntity
{
    public Guid UserId { get; private set; }
    public string Speciality { get; private set; } = default!;
    public string? LicenseNumber { get; private set; }
    public string? HospitalAffiliation { get; private set; }

    /// <summary>Creates a new doctor profile for an existing User.</summary>
    public static DoctorProfile Create(Guid userId, string speciality, string? licenseNumber, string? hospitalAffiliation)
    {
        throw new NotImplementedException();
    }

    public void UpdateProfile(string speciality, string? licenseNumber, string? hospitalAffiliation)
    {
        throw new NotImplementedException();
    }
}

// ════════════════════════════════════════════════════════════════════════════
// MedicalRecord
// ════════════════════════════════════════════════════════════════════════════

/// <summary>
/// A single snapshot of a patient's health vitals submitted for AI risk scoring.
/// Each record triggers CreateMedicalRecordCommand which calls the ML model
/// and saves a linked RiskAssessment.
/// </summary>
public class MedicalRecord : BaseEntity
{
    public Guid PatientId { get; private set; }
    public double BloodPressureSystolic { get; private set; }
    public double BloodPressureDiastolic { get; private set; }

    /// <summary>Blood glucose level in mg/dL.</summary>
    public double BloodSugar { get; private set; }
    public double BMI { get; private set; }
    public string? Symptoms { get; private set; }

    // Doctor note — set asynchronously via AddDoctorNoteCommand
    public string? DoctorNote { get; private set; }
    public Guid? DoctorNoteByDoctorId { get; private set; }

    // Navigation
    public RiskAssessment? RiskAssessment { get; private set; }

    // ── Factory ───────────────────────────────────────────────────────────────
    /// <summary>Creates a new medical record for submission to the AI scoring pipeline.</summary>
    public static MedicalRecord Create(
        Guid patientId,
        double systolic,
        double diastolic,
        double bloodSugar,
        double bmi,
        string? symptoms)
    {
        throw new NotImplementedException();
    }

    // ── Domain methods ────────────────────────────────────────────────────────
    /// <summary>Appends a doctor's clinical note and writes an audit trail entry.</summary>
    public void AddDoctorNote(string note, Guid doctorId)
    {
        throw new NotImplementedException();
    }

    /// <summary>Links the ML-generated assessment back to this record.</summary>
    public void AttachAssessment(RiskAssessment assessment)
    {
        throw new NotImplementedException();
    }
}

// ════════════════════════════════════════════════════════════════════════════
// RiskAssessment
// ════════════════════════════════════════════════════════════════════════════

/// <summary>
/// Stores the output of the ML model for a given MedicalRecord.
/// Contains the raw score (0–1), the classified level, required analyses,
/// recommended doctors, and personalised recommendations.
/// </summary>
public class RiskAssessment : BaseEntity
{
    public Guid MedicalRecordId { get; private set; }
    public Guid PatientId { get; private set; }

    /// <summary>Raw ML probability score [0, 1].</summary>
    public double Score { get; private set; }
    public RiskLevel Level { get; private set; }
    public string? PrimaryIssue { get; private set; }

    /// <summary>JSON-serialised list of required analyses (e.g., HbA1c, lipid panel).</summary>
    public string RequiredAnalysesJson { get; private set; } = "[]";

    /// <summary>JSON-serialised list of recommended doctor specialities.</summary>
    public string RecommendedDoctorsJson { get; private set; } = "[]";

    /// <summary>JSON-serialised list of lifestyle/medical recommendations.</summary>
    public string RecommendationsJson { get; private set; } = "[]";

    public static RiskAssessment Create(
        Guid medicalRecordId,
        Guid patientId,
        double score,
        RiskLevel level,
        string? primaryIssue,
        string requiredAnalysesJson,
        string recommendedDoctorsJson,
        string recommendationsJson)
    {
        throw new NotImplementedException();
    }
}

// ════════════════════════════════════════════════════════════════════════════
// Alert
// ════════════════════════════════════════════════════════════════════════════

/// <summary>
/// System-generated notification triggered when a patient's risk level is High.
/// Delivered to both the patient and their assigned doctor via INotificationService.
/// </summary>
public class Alert : BaseEntity
{
    public Guid RecipientId { get; private set; }
    public string Title { get; private set; } = default!;
    public string Body { get; private set; } = default!;
    public AlertStatus Status { get; private set; } = AlertStatus.Unread;
    public Guid? RelatedAssessmentId { get; private set; }

    public static Alert Create(Guid recipientId, string title, string body, Guid? relatedAssessmentId)
    {
        throw new NotImplementedException();
    }

    /// <summary>Marks the alert as read when the recipient views it.</summary>
    public void MarkAsRead()
    {
        throw new NotImplementedException();
    }
}

// ════════════════════════════════════════════════════════════════════════════
// ChatMessage
// ════════════════════════════════════════════════════════════════════════════

/// <summary>
/// A single message exchanged between a Doctor and a Patient via the real-time SignalR chat.
/// NOT related to the AI Chat Assistant (AIChatController) — that is a separate channel.
/// </summary>
public class ChatMessage : BaseEntity
{
    public Guid SenderId { get; private set; }
    public Guid RecipientId { get; private set; }

    /// <summary>The patient in the conversation, used as the conversation identifier.</summary>
    public Guid PatientId { get; private set; }

    public string Text { get; private set; } = default!;
    public string SenderRole { get; private set; } = default!; // "Doctor" | "Patient"
    public bool IsRead { get; private set; }

    public static ChatMessage Create(Guid senderId, Guid recipientId, Guid patientId, string text, string senderRole)
    {
        throw new NotImplementedException();
    }

    /// <summary>Marks the message as read by the recipient.</summary>
    public void MarkAsRead()
    {
        throw new NotImplementedException();
    }
}
