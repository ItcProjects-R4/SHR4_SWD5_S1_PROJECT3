using MediatR;

namespace Etmen.Application.UseCases.History;

/// <summary>Query: returns BP, Blood Sugar, and BMI readings over time for the vitals sub-charts.</summary>
public sealed record GetVitalsTimelineQuery(Guid PatientId) : IRequest<IEnumerable<VitalTimelineItem>>;

public sealed record VitalTimelineItem(DateTime RecordDate, double BloodPressureSystolic, double BloodPressureDiastolic, double BloodSugar, double BMI);

public sealed class GetVitalsTimelineQueryHandler : IRequestHandler<GetVitalsTimelineQuery, IEnumerable<VitalTimelineItem>>
{
    public GetVitalsTimelineQueryHandler() { throw new NotImplementedException(); }
    public Task<IEnumerable<VitalTimelineItem>> Handle(GetVitalsTimelineQuery request, CancellationToken ct)
        => throw new NotImplementedException();
}
