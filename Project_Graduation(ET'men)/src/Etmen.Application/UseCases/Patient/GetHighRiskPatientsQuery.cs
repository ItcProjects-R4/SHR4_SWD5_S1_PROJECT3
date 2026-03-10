using MediatR;

namespace Etmen.Application.UseCases.Patient;

/// <summary>Query: returns all patients with RiskLevel.High, sorted by score descending.</summary>
public sealed record GetHighRiskPatientsQuery : IRequest<IEnumerable<PatientProfileDto>>;

public sealed class GetHighRiskPatientsQueryHandler : IRequestHandler<GetHighRiskPatientsQuery, IEnumerable<PatientProfileDto>>
{
    public GetHighRiskPatientsQueryHandler() { throw new NotImplementedException(); }
    public Task<IEnumerable<PatientProfileDto>> Handle(GetHighRiskPatientsQuery request, CancellationToken ct)
        => throw new NotImplementedException();
}
