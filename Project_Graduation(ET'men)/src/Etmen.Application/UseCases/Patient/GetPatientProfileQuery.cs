using MediatR;

namespace Etmen.Application.UseCases.Patient;

/// <summary>Query: returns the patient profile with their latest risk score embedded.</summary>
public sealed record GetPatientProfileQuery(Guid PatientId) : IRequest<PatientProfileDto>;

public sealed record PatientProfileDto(
    Guid PatientId, string FullName, int Age, double BMI,
    bool IsSmoker, double LatestRiskScore, string RiskLevel);

public sealed class GetPatientProfileQueryHandler : IRequestHandler<GetPatientProfileQuery, PatientProfileDto>
{
    public GetPatientProfileQueryHandler() { throw new NotImplementedException(); }
    public Task<PatientProfileDto> Handle(GetPatientProfileQuery request, CancellationToken ct)
        => throw new NotImplementedException();
}
