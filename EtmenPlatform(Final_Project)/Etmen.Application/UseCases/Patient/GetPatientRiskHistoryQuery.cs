using MediatR;
using Etmen.Application.DTOs.Response;

namespace Etmen.Application.UseCases.Patient;

/// <summary>Returns the full risk score history timeline for a patient.</summary>
public sealed record GetPatientRiskHistoryQuery(Guid PatientId) : IRequest<PatientProfileResponse>;

public sealed class GetPatientRiskHistoryQueryHandler : IRequestHandler<GetPatientRiskHistoryQuery, PatientProfileResponse>
{
    public Task<PatientProfileResponse> Handle(GetPatientRiskHistoryQuery request, CancellationToken ct)
        => throw new NotImplementedException();
}
