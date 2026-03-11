using MediatR;

namespace Etmen.Application.UseCases.MedicalRecord;

/// <summary>Adds or updates the doctor's clinical note on an existing medical record.</summary>
public sealed record AddDoctorNoteCommand(Guid RecordId, Guid DoctorId, string Note) : IRequest<bool>;

public sealed class AddDoctorNoteCommandHandler : IRequestHandler<AddDoctorNoteCommand, bool>
{
    public Task<bool> Handle(AddDoctorNoteCommand request, CancellationToken ct)
        => throw new NotImplementedException();
}
