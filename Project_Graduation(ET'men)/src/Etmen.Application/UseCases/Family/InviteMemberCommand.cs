using MediatR;

namespace Etmen.Application.UseCases.Family;

/// <summary>Command: generates a secure invite link and emails it to the family member.</summary>
public sealed record InviteMemberCommand(Guid PrimaryUserId, string Email, string Relationship) : IRequest<string>;

public sealed class InviteMemberCommandHandler : IRequestHandler<InviteMemberCommand, string>
{
    public InviteMemberCommandHandler() { throw new NotImplementedException(); }
    public Task<string> Handle(InviteMemberCommand request, CancellationToken ct) => throw new NotImplementedException();
}
