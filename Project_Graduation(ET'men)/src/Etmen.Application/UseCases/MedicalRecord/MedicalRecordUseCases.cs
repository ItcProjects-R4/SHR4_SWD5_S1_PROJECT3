using Etmen.Application.Interfaces;
using Etmen.Application.Services;
using Etmen.Domain.Entities;
using MediatR;

namespace Etmen.Application.UseCases.MedicalRecord;

// ════════════════════════════════════════════════════════════════════════════
// CreateMedicalRecordCommand  (Core AI Pipeline Entry Point)
// ════════════════════════════════════════════════════════════════════════════

/// <summary>
/// The primary write command. Saves vitals → calls ML model → saves RiskAssessment
/// → triggers high-risk alert if Level = High.
/// </summary>
public sealed record CreateMedicalRecordCommand(
    Guid PatientId,
    double BloodPressureSystolic,
    double BloodPressureDiastolic,
    double BloodSugar,
    double BMI,
    string? Symptoms
) : IRequest<CreateMedicalRecordResult>;

public sealed class CreateMedicalRecordCommandHandler : IRequestHandler<CreateMedicalRecordCommand, CreateMedicalRecordResult>
{
    private readonly IMedicalRecordRepository _recordRepo;
    private readonly IRiskAssessmentRepository _assessmentRepo;
    private readonly IPatientRepository _patientRepo;
    private readonly IRiskCalculationService _riskService;
    private readonly INotificationService _notificationService;

    public CreateMedicalRecordCommandHandler(
        IMedicalRecordRepository recordRepo,
        IRiskAssessmentRepository assessmentRepo,
        IPatientRepository patientRepo,
        IRiskCalculationService riskService,
        INotificationService notificationService)
    {
        _recordRepo = recordRepo;
        _assessmentRepo = assessmentRepo;
        _patientRepo = patientRepo;
        _riskService = riskService;
        _notificationService = notificationService;
    }

    /// <summary>
    /// Pipeline: 1) Validate + save MedicalRecord
    ///           2) Build RiskFeatures from record + patient demographics
    ///           3) Call IRiskCalculationService.CalculateRiskAsync
    ///           4) Classify level + generate analyses, doctors, recommendations
    ///           5) Save RiskAssessment + attach to record
    ///           6) If High → send alerts to patient + assigned doctor
    /// </summary>
    public async Task<CreateMedicalRecordResult> Handle(CreateMedicalRecordCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

// ════════════════════════════════════════════════════════════════════════════
// GetMedicalRecordQuery
// ════════════════════════════════════════════════════════════════════════════

/// <summary>Returns a single medical record with its embedded RiskAssessment.</summary>
public sealed record GetMedicalRecordQuery(Guid RecordId) : IRequest<Domain.Entities.MedicalRecord?>;

public sealed class GetMedicalRecordQueryHandler : IRequestHandler<GetMedicalRecordQuery, Domain.Entities.MedicalRecord?>
{
    private readonly IMedicalRecordRepository _recordRepo;

    public GetMedicalRecordQueryHandler(IMedicalRecordRepository recordRepo) => _recordRepo = recordRepo;

    public async Task<Domain.Entities.MedicalRecord?> Handle(GetMedicalRecordQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

// ════════════════════════════════════════════════════════════════════════════
// AddDoctorNoteCommand
// ════════════════════════════════════════════════════════════════════════════

/// <summary>Appends a doctor's clinical note to an existing record and writes an audit log entry.</summary>
public sealed record AddDoctorNoteCommand(Guid RecordId, Guid DoctorId, string Note) : IRequest<Unit>;

public sealed class AddDoctorNoteCommandHandler : IRequestHandler<AddDoctorNoteCommand, Unit>
{
    private readonly IMedicalRecordRepository _recordRepo;

    public AddDoctorNoteCommandHandler(IMedicalRecordRepository recordRepo) => _recordRepo = recordRepo;

    public async Task<Unit> Handle(AddDoctorNoteCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

/// <summary>Result DTO returned after the full create-record pipeline completes.</summary>
public sealed record CreateMedicalRecordResult(
    Guid RecordId, Guid AssessmentId, double Score,
    Domain.Enums.RiskLevel Level, bool AlertSent);
