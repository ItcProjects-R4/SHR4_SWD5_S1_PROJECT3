using Etmen.Application.Interfaces;
using Etmen.Domain.Entities;
using Etmen.Domain.Enums;
using MediatR;

namespace Etmen.Application.UseCases.Patient;

// ════════════════════════════════════════════════════════════════════════════
// GetPatientProfileQuery
// ════════════════════════════════════════════════════════════════════════════

/// <summary>Returns the full profile including demographics, latest risk score, and BMI.</summary>
public sealed record GetPatientProfileQuery(Guid PatientId) : IRequest<PatientProfileResult>;

public sealed class GetPatientProfileQueryHandler : IRequestHandler<GetPatientProfileQuery, PatientProfileResult>
{
    private readonly IPatientRepository _patientRepo;
    private readonly IRiskAssessmentRepository _assessmentRepo;

    public GetPatientProfileQueryHandler(IPatientRepository patientRepo, IRiskAssessmentRepository assessmentRepo)
    {
        _patientRepo = patientRepo;
        _assessmentRepo = assessmentRepo;
    }

    public async Task<PatientProfileResult> Handle(GetPatientProfileQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

// ════════════════════════════════════════════════════════════════════════════
// UpdatePatientProfileCommand
// ════════════════════════════════════════════════════════════════════════════

/// <summary>Updates mutable health metrics (height, weight, smoking, activity) and recalculates BMI.</summary>
public sealed record UpdatePatientProfileCommand(
    Guid PatientId,
    double Height,
    double Weight,
    bool IsSmoker,
    PhysicalActivityLevel ActivityLevel
) : IRequest<Unit>;

public sealed class UpdatePatientProfileCommandHandler : IRequestHandler<UpdatePatientProfileCommand, Unit>
{
    private readonly IPatientRepository _patientRepo;

    public UpdatePatientProfileCommandHandler(IPatientRepository patientRepo) => _patientRepo = patientRepo;

    public async Task<Unit> Handle(UpdatePatientProfileCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

// ════════════════════════════════════════════════════════════════════════════
// GetPatientRiskHistoryQuery
// ════════════════════════════════════════════════════════════════════════════

/// <summary>Returns a paginated list of all past RiskAssessments for a patient.</summary>
public sealed record GetPatientRiskHistoryQuery(Guid PatientId, int Page = 1, int PageSize = 20)
    : IRequest<IEnumerable<RiskAssessment>>;

public sealed class GetPatientRiskHistoryQueryHandler : IRequestHandler<GetPatientRiskHistoryQuery, IEnumerable<RiskAssessment>>
{
    private readonly IRiskAssessmentRepository _assessmentRepo;

    public GetPatientRiskHistoryQueryHandler(IRiskAssessmentRepository assessmentRepo) => _assessmentRepo = assessmentRepo;

    public async Task<IEnumerable<RiskAssessment>> Handle(GetPatientRiskHistoryQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

// ════════════════════════════════════════════════════════════════════════════
// GetHighRiskPatientsQuery
// ════════════════════════════════════════════════════════════════════════════

/// <summary>Returns all patients with RiskLevel.High, sorted by latest score descending. Used by the Admin Dashboard.</summary>
public sealed record GetHighRiskPatientsQuery : IRequest<IEnumerable<PatientProfile>>;

public sealed class GetHighRiskPatientsQueryHandler : IRequestHandler<GetHighRiskPatientsQuery, IEnumerable<PatientProfile>>
{
    private readonly IPatientRepository _patientRepo;

    public GetHighRiskPatientsQueryHandler(IPatientRepository patientRepo) => _patientRepo = patientRepo;

    public async Task<IEnumerable<PatientProfile>> Handle(GetHighRiskPatientsQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

// ── Supporting result record ─────────────────────────────────────────────────
public sealed record PatientProfileResult(
    Guid PatientId, string FullName, int Age, double BMI,
    bool IsSmoker, PhysicalActivityLevel ActivityLevel,
    double? LatestRiskScore, RiskLevel? LatestRiskLevel);
