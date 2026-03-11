using MediatR;

namespace Etmen.Application.UseCases.Family;

/// <summary>Generates a scoped invite token for a family member to link their account.</summary>
public sealed record InviteMemberCommand(Guid UserId) : IRequest<bool>;

public sealed class InviteMemberCommandHandler : IRequestHandler<InviteMemberCommand, bool>
{
    public Task<bool> Handle(InviteMemberCommand request, CancellationToken ct)
        => throw new NotImplementedException();
}
