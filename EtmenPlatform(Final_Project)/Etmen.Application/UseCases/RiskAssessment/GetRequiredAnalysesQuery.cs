using MediatR;
using Etmen.Application.DTOs.Response;

namespace Etmen.Application.UseCases.RiskAssessment;

/// <summary>Returns lab analyses recommended based on the patient's current risk profile.</summary>
public sealed record GetRequiredAnalysesQuery(Guid Id) : IRequest<RiskAssessmentResponse>;

public sealed class GetRequiredAnalysesQueryHandler : IRequestHandler<GetRequiredAnalysesQuery, RiskAssessmentResponse>
{
    public Task<RiskAssessmentResponse> Handle(GetRequiredAnalysesQuery request, CancellationToken ct)
        => throw new NotImplementedException();
}
