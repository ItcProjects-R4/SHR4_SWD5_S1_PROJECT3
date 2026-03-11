using MediatR;
using Etmen.Application.DTOs.Response;

namespace Etmen.Application.UseCases.Patient;

/// <summary>Returns the complete profile and current vitals for a patient.</summary>
public sealed record GetPatientProfileQuery(Guid PatientId) : IRequest<PatientProfileResponse>;

public sealed class GetPatientProfileQueryHandler : IRequestHandler<GetPatientProfileQuery, PatientProfileResponse>
{
    public Task<PatientProfileResponse> Handle(GetPatientProfileQuery request, CancellationToken ct)
        => throw new NotImplementedException();
}
