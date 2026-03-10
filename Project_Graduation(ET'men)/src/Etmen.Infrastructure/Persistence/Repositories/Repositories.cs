using Etmen.Application.Interfaces;
using Etmen.Domain.Entities;
using Etmen.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Etmen.Infrastructure.Persistence.Repositories;

// ════════════════════════════════════════════════════════════════════════════
// UserRepository
// ════════════════════════════════════════════════════════════════════════════

/// <summary>EF Core implementation of IUserRepository.</summary>
public class UserRepository : IUserRepository
{
    private readonly AppDbContext _db;
    public UserRepository(AppDbContext db) => _db = db;

    public async Task<User?> GetByIdAsync(Guid id, CancellationToken ct = default) =>
        await _db.Users.FindAsync(new object[] { id }, ct);

    public async Task<User?> GetByEmailAsync(string email, CancellationToken ct = default) =>
        await _db.Users.FirstOrDefaultAsync(u => u.Email == email, ct);

    public async Task<User?> GetByGoogleIdAsync(string googleId, CancellationToken ct = default) =>
        await _db.Users.FirstOrDefaultAsync(u => u.GoogleId == googleId, ct);

    public async Task<User> CreateAsync(User user, CancellationToken ct = default)
    {
        _db.Users.Add(user);
        await _db.SaveChangesAsync(ct);
        return user;
    }

    public async Task UpdateRefreshTokenAsync(Guid userId, string? token, DateTime? expiry, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }
}

// ════════════════════════════════════════════════════════════════════════════
// PatientRepository
// ════════════════════════════════════════════════════════════════════════════

/// <summary>EF Core implementation of IPatientRepository.</summary>
public class PatientRepository : IPatientRepository
{
    private readonly AppDbContext _db;
    public PatientRepository(AppDbContext db) => _db = db;

    public async Task<PatientProfile?> GetByIdAsync(Guid patientId, CancellationToken ct = default) =>
        await _db.PatientProfiles.FindAsync(new object[] { patientId }, ct);

    public async Task<PatientProfile?> GetByUserIdAsync(Guid userId, CancellationToken ct = default) =>
        await _db.PatientProfiles.FirstOrDefaultAsync(p => p.UserId == userId, ct);

    public async Task<IEnumerable<PatientProfile>> GetAllAsync(string? filter, int page, int pageSize, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<PatientProfile>> GetHighRiskAsync(CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateAsync(PatientProfile patient, CancellationToken ct = default)
    {
        _db.PatientProfiles.Update(patient);
        await _db.SaveChangesAsync(ct);
    }
}

// ════════════════════════════════════════════════════════════════════════════
// MedicalRecordRepository
// ════════════════════════════════════════════════════════════════════════════

/// <summary>EF Core implementation of IMedicalRecordRepository.</summary>
public class MedicalRecordRepository : IMedicalRecordRepository
{
    private readonly AppDbContext _db;
    public MedicalRecordRepository(AppDbContext db) => _db = db;

    public async Task<MedicalRecord> CreateAsync(MedicalRecord record, CancellationToken ct = default)
    {
        _db.MedicalRecords.Add(record);
        await _db.SaveChangesAsync(ct);
        return record;
    }

    public async Task<MedicalRecord?> GetByIdAsync(Guid id, CancellationToken ct = default) =>
        await _db.MedicalRecords.Include(r => r.RiskAssessment).FirstOrDefaultAsync(r => r.Id == id, ct);

    public async Task<IEnumerable<MedicalRecord>> GetByPatientIdAsync(Guid patientId, int page, int pageSize, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public async Task<MedicalRecord?> GetLatestByPatientIdAsync(Guid patientId, CancellationToken ct = default) =>
        await _db.MedicalRecords
            .Where(r => r.PatientId == patientId)
            .OrderByDescending(r => r.CreatedAt)
            .FirstOrDefaultAsync(ct);

    public async Task<IEnumerable<MedicalRecord>> GetVitalsTimelineAsync(Guid patientId, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateAsync(MedicalRecord record, CancellationToken ct = default)
    {
        _db.MedicalRecords.Update(record);
        await _db.SaveChangesAsync(ct);
    }
}

// ════════════════════════════════════════════════════════════════════════════
// RiskAssessmentRepository
// ════════════════════════════════════════════════════════════════════════════

/// <summary>EF Core implementation of IRiskAssessmentRepository.</summary>
public class RiskAssessmentRepository : IRiskAssessmentRepository
{
    private readonly AppDbContext _db;
    public RiskAssessmentRepository(AppDbContext db) => _db = db;

    public async Task<RiskAssessment> CreateAsync(RiskAssessment assessment, CancellationToken ct = default)
    {
        _db.RiskAssessments.Add(assessment);
        await _db.SaveChangesAsync(ct);
        return assessment;
    }

    public async Task<RiskAssessment?> GetByIdAsync(Guid id, CancellationToken ct = default) =>
        await _db.RiskAssessments.FindAsync(new object[] { id }, ct);

    public async Task<IEnumerable<RiskAssessment>> GetByPatientIdAsync(Guid patientId, int page, int pageSize, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public async Task<RiskAssessment?> GetLatestByPatientIdAsync(Guid patientId, CancellationToken ct = default) =>
        await _db.RiskAssessments
            .Where(a => a.PatientId == patientId)
            .OrderByDescending(a => a.CreatedAt)
            .FirstOrDefaultAsync(ct);

    public async Task<IEnumerable<RiskAssessment>> GetByPatientIdAsync(Guid patientId, DateTime from, DateTime to, CancellationToken ct = default) =>
        await _db.RiskAssessments
            .Where(a => a.PatientId == patientId && a.CreatedAt >= from && a.CreatedAt <= to)
            .OrderBy(a => a.CreatedAt)
            .ToListAsync(ct);
}

// ════════════════════════════════════════════════════════════════════════════
// AlertRepository
// ════════════════════════════════════════════════════════════════════════════

/// <summary>EF Core implementation of IAlertRepository.</summary>
public class AlertRepository : IAlertRepository
{
    private readonly AppDbContext _db;
    public AlertRepository(AppDbContext db) => _db = db;

    public async Task<Alert> CreateAsync(Alert alert, CancellationToken ct = default)
    {
        _db.Alerts.Add(alert);
        await _db.SaveChangesAsync(ct);
        return alert;
    }

    public async Task<IEnumerable<Alert>> GetByPatientIdAsync(Guid patientId, CancellationToken ct = default) =>
        await _db.Alerts.Where(a => a.RecipientId == patientId).OrderByDescending(a => a.CreatedAt).ToListAsync(ct);

    public async Task MarkAsReadAsync(Guid alertId, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public async Task<int> GetUnreadCountAsync(Guid userId, CancellationToken ct = default) =>
        await _db.Alerts.CountAsync(a => a.RecipientId == userId && a.Status == AlertStatus.Unread, ct);
}

// ════════════════════════════════════════════════════════════════════════════
// ChatRepository
// ════════════════════════════════════════════════════════════════════════════

/// <summary>EF Core implementation of IChatRepository.</summary>
public class ChatRepository : IChatRepository
{
    private readonly AppDbContext _db;
    public ChatRepository(AppDbContext db) => _db = db;

    public async Task<ChatMessage> SaveMessageAsync(ChatMessage message, CancellationToken ct = default)
    {
        _db.ChatMessages.Add(message);
        await _db.SaveChangesAsync(ct);
        return message;
    }

    public async Task<IEnumerable<ChatMessage>> GetConversationAsync(Guid patientId, Guid doctorId, CancellationToken ct = default) =>
        await _db.ChatMessages
            .Where(m => m.PatientId == patientId &&
                        (m.SenderId == doctorId || m.RecipientId == doctorId))
            .OrderBy(m => m.CreatedAt)
            .ToListAsync(ct);

    public async Task<IEnumerable<ChatMessage>> GetAllConversationsForDoctorAsync(Guid doctorId, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public async Task MarkAsReadAsync(Guid conversationPatientId, Guid readerId, CancellationToken ct = default)
    {
        var unread = await _db.ChatMessages
            .Where(m => m.PatientId == conversationPatientId && m.RecipientId == readerId && !m.IsRead)
            .ToListAsync(ct);
        unread.ForEach(m => m.MarkAsRead());
        await _db.SaveChangesAsync(ct);
    }
}

// ════════════════════════════════════════════════════════════════════════════
// HealthcareProviderRepository  (NEW — v3.0)
// ════════════════════════════════════════════════════════════════════════════

/// <summary>EF Core implementation of IHealthcareProviderRepository.</summary>
public class HealthcareProviderRepository : IHealthcareProviderRepository
{
    private readonly AppDbContext _db;
    public HealthcareProviderRepository(AppDbContext db) => _db = db;

    public async Task<HealthcareProvider?> GetByIdAsync(Guid id, CancellationToken ct = default) =>
        await _db.HealthcareProviders.Include(p => p.AvailableSlots).FirstOrDefaultAsync(p => p.Id == id, ct);

    public async Task<IEnumerable<HealthcareProvider>> SearchNearbyAsync(double lat, double lng, double radiusMeters, CancellationToken ct = default)
    {
        // TODO: Implement Haversine-based geospatial query or use SQL geography types
        throw new NotImplementedException();
    }

    public async Task<HealthcareProvider> CreateAsync(HealthcareProvider provider, CancellationToken ct = default)
    {
        _db.HealthcareProviders.Add(provider);
        await _db.SaveChangesAsync(ct);
        return provider;
    }

    public async Task UpdateAsync(HealthcareProvider provider, CancellationToken ct = default)
    {
        _db.HealthcareProviders.Update(provider);
        await _db.SaveChangesAsync(ct);
    }
}

// ════════════════════════════════════════════════════════════════════════════
// AppointmentRepository  (NEW — v3.0)
// ════════════════════════════════════════════════════════════════════════════

/// <summary>EF Core implementation of IAppointmentRepository.</summary>
public class AppointmentRepository : IAppointmentRepository
{
    private readonly AppDbContext _db;
    public AppointmentRepository(AppDbContext db) => _db = db;

    public async Task<Appointment> CreateAsync(Appointment appointment, CancellationToken ct = default)
    {
        _db.Appointments.Add(appointment);
        await _db.SaveChangesAsync(ct);
        return appointment;
    }

    public async Task<Appointment?> GetByIdAsync(Guid id, CancellationToken ct = default) =>
        await _db.Appointments.FindAsync(new object[] { id }, ct);

    public async Task<IEnumerable<Appointment>> GetByPatientIdAsync(Guid patientId, CancellationToken ct = default) =>
        await _db.Appointments.Where(a => a.PatientId == patientId).OrderByDescending(a => a.AppointmentAt).ToListAsync(ct);

    public async Task UpdateAsync(Appointment appointment, CancellationToken ct = default)
    {
        _db.Appointments.Update(appointment);
        await _db.SaveChangesAsync(ct);
    }
}

// ════════════════════════════════════════════════════════════════════════════
// LabResultRepository  (NEW — v3.0)
// ════════════════════════════════════════════════════════════════════════════

/// <summary>EF Core implementation of ILabResultRepository.</summary>
public class LabResultRepository : ILabResultRepository
{
    private readonly AppDbContext _db;
    public LabResultRepository(AppDbContext db) => _db = db;

    public async Task<LabResult> CreateAsync(LabResult labResult, CancellationToken ct = default)
    {
        _db.LabResults.Add(labResult);
        await _db.SaveChangesAsync(ct);
        return labResult;
    }

    public async Task<LabResult?> GetByIdAsync(Guid id, CancellationToken ct = default) =>
        await _db.LabResults.FindAsync(new object[] { id }, ct);

    public async Task<IEnumerable<LabResult>> GetByPatientIdAsync(Guid patientId, CancellationToken ct = default) =>
        await _db.LabResults.Where(l => l.PatientId == patientId).OrderByDescending(l => l.CreatedAt).ToListAsync(ct);

    public async Task UpdateAsync(LabResult labResult, CancellationToken ct = default)
    {
        _db.LabResults.Update(labResult);
        await _db.SaveChangesAsync(ct);
    }
}

// ════════════════════════════════════════════════════════════════════════════
// FamilyLinkRepository  (NEW — v3.0)
// ════════════════════════════════════════════════════════════════════════════

/// <summary>EF Core implementation of IFamilyLinkRepository.</summary>
public class FamilyLinkRepository : IFamilyLinkRepository
{
    private readonly AppDbContext _db;
    public FamilyLinkRepository(AppDbContext db) => _db = db;

    public async Task<FamilyLink> CreateAsync(FamilyLink link, CancellationToken ct = default)
    {
        _db.FamilyLinks.Add(link);
        await _db.SaveChangesAsync(ct);
        return link;
    }

    public async Task<FamilyLink?> GetByIdAsync(Guid id, CancellationToken ct = default) =>
        await _db.FamilyLinks.FindAsync(new object[] { id }, ct);

    public async Task<FamilyLink?> GetByInviteTokenAsync(string token, CancellationToken ct = default) =>
        await _db.FamilyLinks.FirstOrDefaultAsync(f => f.InviteToken == token, ct);

    public async Task<IEnumerable<FamilyLink>> GetByPrimaryUserIdAsync(Guid primaryUserId, CancellationToken ct = default) =>
        await _db.FamilyLinks.Where(f => f.PrimaryUserId == primaryUserId).ToListAsync(ct);

    public async Task UpdateAsync(FamilyLink link, CancellationToken ct = default)
    {
        _db.FamilyLinks.Update(link);
        await _db.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(Guid linkId, CancellationToken ct = default)
    {
        throw new NotImplementedException(); // Soft delete via FamilyLink.Delete()
    }
}
