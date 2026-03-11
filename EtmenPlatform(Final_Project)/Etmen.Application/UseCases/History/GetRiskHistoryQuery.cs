using MediatR;
using Etmen.Application.DTOs.Response;

namespace Etmen.Application.UseCases.History;

/// <summary>Returns time-series risk score history for a patient's health trend chart.</summary>
public sealed record GetRiskHistoryQuery(Guid PatientId) : IRequest<IEnumerable<RiskAssessmentResponse>>;

public sealed class GetRiskHistoryQueryHandler : IRequestHandler<GetRiskHistoryQuery, IEnumerable<RiskAssessmentResponse>>
{
    public Task<IEnumerable<RiskAssessmentResponse>> Handle(GetRiskHistoryQuery request, CancellationToken ct)
        => throw new NotImplementedException();
}
