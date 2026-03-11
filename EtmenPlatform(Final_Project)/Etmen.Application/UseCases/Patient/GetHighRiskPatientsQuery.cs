using Etmen.Application.Common;
using Etmen.Application.DTOs.Response;
using MediatR;

namespace Etmen.Application.UseCases.Patient;

/// <summary>Returns paginated list of high-risk patients for doctor/admin dashboard.</summary>
public sealed record GetHighRiskPatientsQuery(PaginationParams Pagination) : IRequest<PagedResult<PatientProfileResponse>>;

public sealed class GetHighRiskPatientsQueryHandler
    : IRequestHandler<GetHighRiskPatientsQuery, PagedResult<PatientProfileResponse>>
{
    public Task<PagedResult<PatientProfileResponse>> Handle(
        GetHighRiskPatientsQuery request, CancellationToken ct)
        => throw new NotImplementedException();
}
