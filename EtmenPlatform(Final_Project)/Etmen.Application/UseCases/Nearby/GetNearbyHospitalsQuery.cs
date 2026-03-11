using MediatR;
using Etmen.Application.DTOs.Response;

namespace Etmen.Application.UseCases.Nearby;

/// <summary>Searches for nearby hospitals within a configurable radius.</summary>
public sealed record GetNearbyHospitalsQuery(Guid PatientId, double Lat, double Lng) : IRequest<IEnumerable<NearbyProviderResponse>>;

public sealed class GetNearbyHospitalsQueryHandler : IRequestHandler<GetNearbyHospitalsQuery, IEnumerable<NearbyProviderResponse>>
{
    public Task<IEnumerable<NearbyProviderResponse>> Handle(GetNearbyHospitalsQuery request, CancellationToken ct)
        => throw new NotImplementedException();
}
