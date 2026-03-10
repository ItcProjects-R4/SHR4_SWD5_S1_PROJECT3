using MediatR;

namespace Etmen.Application.UseCases.MedicalRecord;

/// <summary>Query: returns a single medical record with its embedded RiskAssessment.</summary>
public sealed record GetMedicalRecordQuery(Guid RecordId) : IRequest<MedicalRecordDto>;

public sealed record MedicalRecordDto(
    Guid RecordId, Guid PatientId, double BloodPressureSystolic,
    double BloodPressureDiastolic, double BloodSugar, double BMI,
    string? Symptoms, string? DoctorNote, DateTime CreatedAt,
    RiskAssessmentResultDto? RiskAssessment);

public sealed class GetMedicalRecordQueryHandler : IRequestHandler<GetMedicalRecordQuery, MedicalRecordDto>
{
    public GetMedicalRecordQueryHandler() { throw new NotImplementedException(); }
    public Task<MedicalRecordDto> Handle(GetMedicalRecordQuery request, CancellationToken ct)
        => throw new NotImplementedException();
}
