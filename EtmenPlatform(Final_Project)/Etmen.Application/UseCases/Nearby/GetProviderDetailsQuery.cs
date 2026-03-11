using MediatR;
using Etmen.Application.DTOs.Response;

namespace Etmen.Application.UseCases.Nearby;

/// <summary>Returns full provider details including rating, hours, and available slots.</summary>
public sealed record GetProviderDetailsQuery(Guid ProviderId) : IRequest<ProviderDetails>;

public sealed class GetProviderDetailsQueryHandler : IRequestHandler<GetProviderDetailsQuery, ProviderDetails>
{
    public Task<ProviderDetails> Handle(GetProviderDetailsQuery request, CancellationToken ct)
        => throw new NotImplementedException();
}
