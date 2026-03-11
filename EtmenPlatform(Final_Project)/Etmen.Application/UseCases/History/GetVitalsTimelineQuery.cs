using MediatR;
using Etmen.Application.DTOs.Response;

namespace Etmen.Application.UseCases.History;

/// <summary>Returns time-series vitals data (blood pressure, sugar, BMI) for chart rendering.</summary>
public sealed record GetVitalsTimelineQuery(Guid PatientId) : IRequest<IEnumerable<RiskAssessmentResponse>>;

public sealed class GetVitalsTimelineQueryHandler : IRequestHandler<GetVitalsTimelineQuery, IEnumerable<RiskAssessmentResponse>>
{
    public Task<IEnumerable<RiskAssessmentResponse>> Handle(GetVitalsTimelineQuery request, CancellationToken ct)
        => throw new NotImplementedException();
}
