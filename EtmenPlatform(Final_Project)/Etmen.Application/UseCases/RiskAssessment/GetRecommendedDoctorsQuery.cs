using MediatR;
using Etmen.Application.DTOs.Response;

namespace Etmen.Application.UseCases.RiskAssessment;

/// <summary>Returns specialist recommendations based on the patient's risk assessment.</summary>
public sealed record GetRecommendedDoctorsQuery(Guid Id) : IRequest<RiskAssessmentResponse>;

public sealed class GetRecommendedDoctorsQueryHandler : IRequestHandler<GetRecommendedDoctorsQuery, RiskAssessmentResponse>
{
    public Task<RiskAssessmentResponse> Handle(GetRecommendedDoctorsQuery request, CancellationToken ct)
        => throw new NotImplementedException();
}
