using MediatR;

namespace Etmen.Application.UseCases.Family;

/// <summary>Removes a family link. Both sides lose access to each other's data.</summary>
public sealed record RemoveLinkCommand(Guid UserId) : IRequest<bool>;

public sealed class RemoveLinkCommandHandler : IRequestHandler<RemoveLinkCommand, bool>
{
    public Task<bool> Handle(RemoveLinkCommand request, CancellationToken ct)
        => throw new NotImplementedException();
}
