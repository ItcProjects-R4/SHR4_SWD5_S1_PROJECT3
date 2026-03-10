using MediatR;

namespace Etmen.Application.UseCases.Family;

/// <summary>Query: issues a scoped JWT for the linked family member's profile.</summary>
public sealed record SwitchProfileQuery(Guid PrimaryUserId, Guid LinkedUserId) : IRequest<ScopedTokenDto>;
public sealed record ScopedTokenDto(string ScopedToken, Guid LinkedPatientId, string LinkedPatientName);

public sealed class SwitchProfileQueryHandler : IRequestHandler<SwitchProfileQuery, ScopedTokenDto>
{
    public SwitchProfileQueryHandler() { throw new NotImplementedException(); }
    public Task<ScopedTokenDto> Handle(SwitchProfileQuery request, CancellationToken ct) => throw new NotImplementedException();
}
