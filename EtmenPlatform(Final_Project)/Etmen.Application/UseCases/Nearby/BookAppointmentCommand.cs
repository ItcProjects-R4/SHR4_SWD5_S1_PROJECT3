using MediatR;
using Etmen.Application.DTOs.Request;

namespace Etmen.Application.UseCases.Nearby;

/// <summary>Books an appointment slot for a patient with a healthcare provider.</summary>
public sealed record BookAppointmentCommand(BookAppointmentRequest Request) : IRequest<Guid>;

public sealed class BookAppointmentCommandHandler : IRequestHandler<BookAppointmentCommand, Guid>
{
    public Task<Guid> Handle(BookAppointmentCommand request, CancellationToken ct)
        => throw new NotImplementedException();
}
