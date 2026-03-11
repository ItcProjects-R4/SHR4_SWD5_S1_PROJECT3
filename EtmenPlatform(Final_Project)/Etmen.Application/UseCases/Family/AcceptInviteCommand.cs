using MediatR;

namespace Etmen.Application.UseCases.Family;

/// <summary>Validates an invite token and creates the family link between two accounts.</summary>
public sealed record AcceptInviteCommand(Guid UserId) : IRequest<bool>;

public sealed class AcceptInviteCommandHandler : IRequestHandler<AcceptInviteCommand, bool>
{
    public Task<bool> Handle(AcceptInviteCommand request, CancellationToken ct)
        => throw new NotImplementedException();
}
