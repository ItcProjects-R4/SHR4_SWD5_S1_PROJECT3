using MediatR;

namespace Etmen.Application.UseCases.Family;

/// <summary>Returns all linked family members and their consent status.</summary>
public sealed record GetFamilyMembersQuery(Guid UserId) : IRequest<bool>;

public sealed class GetFamilyMembersQueryHandler : IRequestHandler<GetFamilyMembersQuery, bool>
{
    public Task<bool> Handle(GetFamilyMembersQuery request, CancellationToken ct)
        => throw new NotImplementedException();
}
