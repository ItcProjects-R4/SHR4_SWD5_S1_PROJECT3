using Etmen.Application.Services;
using MediatR;

namespace Etmen.Application.UseCases.Nearby;

/// <summary>Query: merges registered DB providers with Google Places, scores by specialty/distance/rating.</summary>
public sealed record GetNearbyProvidersQuery(
    double Lat, double Lng, double RadiusMeters,
    string? Specialty, string? RiskLevel, bool OpenNow)
    : IRequest<IEnumerable<NearbyProvider>>;

public sealed class GetNearbyProvidersQueryHandler : IRequestHandler<GetNearbyProvidersQuery, IEnumerable<NearbyProvider>>
{
    public GetNearbyProvidersQueryHandler() { throw new NotImplementedException(); }
    public Task<IEnumerable<NearbyProvider>> Handle(GetNearbyProvidersQuery request, CancellationToken ct)
        => throw new NotImplementedException();
}
