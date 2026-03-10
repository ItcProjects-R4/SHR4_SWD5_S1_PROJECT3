using MediatR;

namespace Etmen.Application.UseCases.MedicalRecord;

/// <summary>
/// Command: saves a new health record, invokes the AI engine, persists the RiskAssessment,
/// and triggers alert notifications if RiskLevel is High.
/// </summary>
public sealed record CreateMedicalRecordCommand(
    Guid    PatientId,
    double  BloodPressureSystolic,
    double  BloodPressureDiastolic,
    double  BloodSugar,
    double  BMI,
    string? Symptoms) : IRequest<RiskAssessmentResultDto>;

public sealed record RiskAssessmentResultDto(
    Guid   AssessmentId, double Score, string RiskLevel,
    List<string> RequiredAnalyses, List<string> RecommendedDoctors,
    List<string> Recommendations);

public sealed class CreateMedicalRecordCommandHandler : IRequestHandler<CreateMedicalRecordCommand, RiskAssessmentResultDto>
{
    public CreateMedicalRecordCommandHandler() { throw new NotImplementedException(); }

    /// <summary>
    /// 1) Validate data  2) Create MedicalRecord  3) Save to DB
    /// 4) Call AI → RiskAssessment  5) Trigger alert if High  6) Return result
    /// </summary>
    public Task<RiskAssessmentResultDto> Handle(CreateMedicalRecordCommand request, CancellationToken ct)
        => throw new NotImplementedException();
}
