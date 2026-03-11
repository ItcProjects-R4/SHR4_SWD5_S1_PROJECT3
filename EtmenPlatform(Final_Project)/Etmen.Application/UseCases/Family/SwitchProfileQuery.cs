using MediatR;

namespace Etmen.Application.UseCases.Family;

/// <summary>Generates a scoped JWT to view a linked family member's data.</summary>
public sealed record SwitchProfileQuery(Guid UserId) : IRequest<bool>;

public sealed class SwitchProfileQueryHandler : IRequestHandler<SwitchProfileQuery, bool>
{
    public Task<bool> Handle(SwitchProfileQuery request, CancellationToken ct)
        => throw new NotImplementedException();
}
