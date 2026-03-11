using MediatR;
using Etmen.Application.DTOs.Response;

namespace Etmen.Application.UseCases.RiskAssessment;

/// <summary>Returns a single risk assessment by ID.</summary>
public sealed record GetAssessmentQuery(Guid Id) : IRequest<RiskAssessmentResponse>;

public sealed class GetAssessmentQueryHandler : IRequestHandler<GetAssessmentQuery, RiskAssessmentResponse>
{
    public Task<RiskAssessmentResponse> Handle(GetAssessmentQuery request, CancellationToken ct)
        => throw new NotImplementedException();
}
