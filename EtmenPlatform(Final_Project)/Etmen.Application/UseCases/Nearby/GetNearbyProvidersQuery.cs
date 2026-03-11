using MediatR;
using Etmen.Application.DTOs.Response;

namespace Etmen.Application.UseCases.Nearby;

/// <summary>Searches for nearby healthcare providers by location, specialty, and availability.</summary>
public sealed record GetNearbyProvidersQuery(Guid PatientId, double Lat, double Lng) : IRequest<IEnumerable<NearbyProviderResponse>>;

public sealed class GetNearbyProvidersQueryHandler : IRequestHandler<GetNearbyProvidersQuery, IEnumerable<NearbyProviderResponse>>
{
    public Task<IEnumerable<NearbyProviderResponse>> Handle(GetNearbyProvidersQuery request, CancellationToken ct)
        => throw new NotImplementedException();
}
