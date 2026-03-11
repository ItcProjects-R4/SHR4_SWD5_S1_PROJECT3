using MediatR;
using Etmen.Application.DTOs.Response;

namespace Etmen.Application.UseCases.Patient;

/// <summary>Updates patient vitals and triggers a new risk assessment.</summary>
public sealed record UpdatePatientProfileCommand(Guid PatientId) : IRequest<PatientProfileResponse>;

public sealed class UpdatePatientProfileCommandHandler : IRequestHandler<UpdatePatientProfileCommand, PatientProfileResponse>
{
    public Task<PatientProfileResponse> Handle(UpdatePatientProfileCommand request, CancellationToken ct)
        => throw new NotImplementedException();
}
