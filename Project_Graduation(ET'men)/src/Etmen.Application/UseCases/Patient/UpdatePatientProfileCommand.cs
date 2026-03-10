using Etmen.Domain.Enums;
using MediatR;

namespace Etmen.Application.UseCases.Patient;

/// <summary>Command: updates mutable health metrics and recalculates BMI.</summary>
public sealed record UpdatePatientProfileCommand(
    Guid PatientId, double Height, double Weight,
    bool IsSmoker, PhysicalActivityLevel ActivityLevel) : IRequest<bool>;

public sealed class UpdatePatientProfileCommandHandler : IRequestHandler<UpdatePatientProfileCommand, bool>
{
    public UpdatePatientProfileCommandHandler() { throw new NotImplementedException(); }
    public Task<bool> Handle(UpdatePatientProfileCommand request, CancellationToken ct)
        => throw new NotImplementedException();
}
