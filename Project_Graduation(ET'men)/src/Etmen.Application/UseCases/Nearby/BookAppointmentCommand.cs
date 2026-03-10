using MediatR;

namespace Etmen.Application.UseCases.Nearby;

/// <summary>Command: creates Appointment, reserves slot, sends confirmation via SMS+email.</summary>
public sealed record BookAppointmentCommand(
    Guid PatientId, Guid ProviderId, Guid AssessmentId,
    DateTime AppointmentAt, bool IsEmergency = false, string? Notes = null)
    : IRequest<AppointmentDto>;

public sealed record AppointmentDto(Guid AppointmentId, string ProviderName, string ProviderAddress, DateTime AppointmentAt, string Status);

public sealed class BookAppointmentCommandHandler : IRequestHandler<BookAppointmentCommand, AppointmentDto>
{
    public BookAppointmentCommandHandler() { throw new NotImplementedException(); }
    public Task<AppointmentDto> Handle(BookAppointmentCommand request, CancellationToken ct)
        => throw new NotImplementedException();
}
