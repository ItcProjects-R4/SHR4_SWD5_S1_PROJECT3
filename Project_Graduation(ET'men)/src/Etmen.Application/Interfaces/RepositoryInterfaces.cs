using Etmen.Domain.Entities;
using Etmen.Domain.Enums;

namespace Etmen.Application.Interfaces;

// ════════════════════════════════════════════════════════════════════════════
// IUserRepository
// ════════════════════════════════════════════════════════════════════════════

/// <summary>
/// Data access contract for the User aggregate root.
/// Implemented by Infrastructure/Persistence/Repositories/UserRepository.cs.
/// </summary>
public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<User?> GetByEmailAsync(string email, CancellationToken ct = default);
    Task<User?> GetByGoogleIdAsync(string googleId, CancellationToken ct = default);
    Task<User> CreateAsync(User user, CancellationToken ct = default);
    Task UpdateRefreshTokenAsync(Guid userId, string? token, DateTime? expiry, CancellationToken ct = default);
}

// ════════════════════════════════════════════════════════════════════════════
// IPatientRepository
// ════════════════════════════════════════════════════════════════════════════

/// <summary>
/// Data access contract for PatientProfile entities.
/// Implemented by Infrastructure/Persistence/Repositories/PatientRepository.cs.
/// </summary>
public interface IPatientRepository
{
    Task<PatientProfile?> GetByIdAsync(Guid patientId, CancellationToken ct = default);
    Task<PatientProfile?> GetByUserIdAsync(Guid userId, CancellationToken ct = default);

    /// <summary>Returns a paged list of patient profiles, optionally filtered by search term or risk level.</summary>
    Task<IEnumerable<PatientProfile>> GetAllAsync(string? filter, int page, int pageSize, CancellationToken ct = default);

    /// <summary>Returns patients with RiskLevel.High, sorted by latest score descending.</summary>
    Task<IEnumerable<PatientProfile>> GetHighRiskAsync(CancellationToken ct = default);

    Task UpdateAsync(PatientProfile patient, CancellationToken ct = default);
}

// ════════════════════════════════════════════════════════════════════════════
// IMedicalRecordRepository
// ════════════════════════════════════════════════════════════════════════════

/// <summary>
/// Data access contract for MedicalRecord entities.
/// Implemented by Infrastructure/Persistence/Repositories/MedicalRecordRepository.cs.
/// </summary>
public interface IMedicalRecordRepository
{
    Task<MedicalRecord> CreateAsync(MedicalRecord record, CancellationToken ct = default);
    Task<MedicalRecord?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<IEnumerable<MedicalRecord>> GetByPatientIdAsync(Guid patientId, int page, int pageSize, CancellationToken ct = default);
    Task<MedicalRecord?> GetLatestByPatientIdAsync(Guid patientId, CancellationToken ct = default);

    /// <summary>Returns vitals (BP, blood sugar, BMI) over time for the Health History Dashboard.</summary>
    Task<IEnumerable<MedicalRecord>> GetVitalsTimelineAsync(Guid patientId, CancellationToken ct = default);

    Task UpdateAsync(MedicalRecord record, CancellationToken ct = default);
}

// ════════════════════════════════════════════════════════════════════════════
// IRiskAssessmentRepository
// ════════════════════════════════════════════════════════════════════════════

/// <summary>
/// Data access contract for RiskAssessment entities.
/// Implemented by Infrastructure/Persistence/Repositories/RiskAssessmentRepository.cs.
/// </summary>
public interface IRiskAssessmentRepository
{
    Task<RiskAssessment> CreateAsync(RiskAssessment assessment, CancellationToken ct = default);
    Task<RiskAssessment?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<IEnumerable<RiskAssessment>> GetByPatientIdAsync(Guid patientId, int page, int pageSize, CancellationToken ct = default);
    Task<RiskAssessment?> GetLatestByPatientIdAsync(Guid patientId, CancellationToken ct = default);

    /// <summary>Returns risk score history within a date range for the Health History line chart.</summary>
    Task<IEnumerable<RiskAssessment>> GetByPatientIdAsync(Guid patientId, DateTime from, DateTime to, CancellationToken ct = default);
}

// ════════════════════════════════════════════════════════════════════════════
// IAlertRepository
// ════════════════════════════════════════════════════════════════════════════

/// <summary>
/// Data access contract for Alert entities.
/// Implemented by Infrastructure/Persistence/Repositories/AlertRepository.cs.
/// </summary>
public interface IAlertRepository
{
    Task<Alert> CreateAsync(Alert alert, CancellationToken ct = default);
    Task<IEnumerable<Alert>> GetByPatientIdAsync(Guid patientId, CancellationToken ct = default);
    Task MarkAsReadAsync(Guid alertId, CancellationToken ct = default);
    Task<int> GetUnreadCountAsync(Guid userId, CancellationToken ct = default);
}

// ════════════════════════════════════════════════════════════════════════════
// IChatRepository
// ════════════════════════════════════════════════════════════════════════════

/// <summary>
/// Data access contract for Doctor–Patient ChatMessage entities.
/// Implemented by Infrastructure/Persistence/Repositories/ChatRepository.cs.
/// </summary>
public interface IChatRepository
{
    Task<ChatMessage> SaveMessageAsync(ChatMessage message, CancellationToken ct = default);
    Task<IEnumerable<ChatMessage>> GetConversationAsync(Guid patientId, Guid doctorId, CancellationToken ct = default);
    Task<IEnumerable<ChatMessage>> GetAllConversationsForDoctorAsync(Guid doctorId, CancellationToken ct = default);
    Task MarkAsReadAsync(Guid conversationPatientId, Guid readerId, CancellationToken ct = default);
}

// ════════════════════════════════════════════════════════════════════════════
// IHealthcareProviderRepository  (NEW — v3.0)
// ════════════════════════════════════════════════════════════════════════════

/// <summary>
/// Data access contract for platform-registered HealthcareProvider entities.
/// Complements the Google Places live search in IGeoSearchService.
/// </summary>
public interface IHealthcareProviderRepository
{
    Task<HealthcareProvider?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<IEnumerable<HealthcareProvider>> SearchNearbyAsync(double lat, double lng, double radiusMeters, CancellationToken ct = default);
    Task<HealthcareProvider> CreateAsync(HealthcareProvider provider, CancellationToken ct = default);
    Task UpdateAsync(HealthcareProvider provider, CancellationToken ct = default);
}

// ════════════════════════════════════════════════════════════════════════════
// IAppointmentRepository  (NEW — v3.0)
// ════════════════════════════════════════════════════════════════════════════

/// <summary>
/// Data access contract for Appointment bookings.
/// </summary>
public interface IAppointmentRepository
{
    Task<Appointment> CreateAsync(Appointment appointment, CancellationToken ct = default);
    Task<Appointment?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<IEnumerable<Appointment>> GetByPatientIdAsync(Guid patientId, CancellationToken ct = default);
    Task UpdateAsync(Appointment appointment, CancellationToken ct = default);
}

// ════════════════════════════════════════════════════════════════════════════
// ILabResultRepository  (NEW — v3.0)
// ════════════════════════════════════════════════════════════════════════════

/// <summary>Data access contract for LabResult entities produced by the OCR pipeline.</summary>
public interface ILabResultRepository
{
    Task<LabResult> CreateAsync(LabResult labResult, CancellationToken ct = default);
    Task<LabResult?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<IEnumerable<LabResult>> GetByPatientIdAsync(Guid patientId, CancellationToken ct = default);
    Task UpdateAsync(LabResult labResult, CancellationToken ct = default);
}

// ════════════════════════════════════════════════════════════════════════════
// IFamilyLinkRepository  (NEW — v3.0)
// ════════════════════════════════════════════════════════════════════════════

/// <summary>Data access contract for FamilyLink entities.</summary>
public interface IFamilyLinkRepository
{
    Task<FamilyLink> CreateAsync(FamilyLink link, CancellationToken ct = default);
    Task<FamilyLink?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<FamilyLink?> GetByInviteTokenAsync(string token, CancellationToken ct = default);
    Task<IEnumerable<FamilyLink>> GetByPrimaryUserIdAsync(Guid primaryUserId, CancellationToken ct = default);
    Task UpdateAsync(FamilyLink link, CancellationToken ct = default);
    Task DeleteAsync(Guid linkId, CancellationToken ct = default);
}
