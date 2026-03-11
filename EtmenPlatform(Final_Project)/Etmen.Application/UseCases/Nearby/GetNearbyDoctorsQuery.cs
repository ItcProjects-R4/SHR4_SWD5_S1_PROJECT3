using MediatR;
using Etmen.Application.DTOs.Response;

namespace Etmen.Application.UseCases.Nearby;

/// <summary>Searches for nearby doctors filtered by specialty and availability slots.</summary>
public sealed record GetNearbyDoctorsQuery(Guid PatientId, double Lat, double Lng) : IRequest<IEnumerable<NearbyProviderResponse>>;

public sealed class GetNearbyDoctorsQueryHandler : IRequestHandler<GetNearbyDoctorsQuery, IEnumerable<NearbyProviderResponse>>
{
    public Task<IEnumerable<NearbyProviderResponse>> Handle(GetNearbyDoctorsQuery request, CancellationToken ct)
        => throw new NotImplementedException();
}
