using MediatR;

namespace Etmen.Application.UseCases.History;

/// <summary>Query: returns risk score history for the patient timeline chart (date range filter).</summary>
public sealed record GetRiskHistoryQuery(Guid PatientId, DateTime From, DateTime To)
    : IRequest<IEnumerable<RiskAssessmentHistoryItem>>;

public sealed record RiskAssessmentHistoryItem(DateTime AssessmentDate, double RiskScore, string RiskLevel, string? PrimaryIssue);

public sealed class GetRiskHistoryQueryHandler : IRequestHandler<GetRiskHistoryQuery, IEnumerable<RiskAssessmentHistoryItem>>
{
    public GetRiskHistoryQueryHandler() { throw new NotImplementedException(); }
    public Task<IEnumerable<RiskAssessmentHistoryItem>> Handle(GetRiskHistoryQuery request, CancellationToken ct)
        => throw new NotImplementedException();
}
