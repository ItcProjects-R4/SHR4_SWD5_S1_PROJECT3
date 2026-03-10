using MediatR;

namespace Etmen.Application.UseCases.Family;

/// <summary>Command: soft-deletes a FamilyLink record.</summary>
public sealed record RemoveLinkCommand(Guid LinkId, Guid RequestingUserId) : IRequest<bool>;

public sealed class RemoveLinkCommandHandler : IRequestHandler<RemoveLinkCommand, bool>
{
    public RemoveLinkCommandHandler() { throw new NotImplementedException(); }
    public Task<bool> Handle(RemoveLinkCommand request, CancellationToken ct) => throw new NotImplementedException();
}
