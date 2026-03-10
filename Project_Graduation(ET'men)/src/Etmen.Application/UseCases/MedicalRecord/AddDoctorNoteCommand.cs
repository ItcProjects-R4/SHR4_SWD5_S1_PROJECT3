using MediatR;

namespace Etmen.Application.UseCases.MedicalRecord;

/// <summary>Command: appends a clinical note to a record and writes an audit log entry.</summary>
public sealed record AddDoctorNoteCommand(Guid RecordId, Guid DoctorId, string Note) : IRequest<bool>;

public sealed class AddDoctorNoteCommandHandler : IRequestHandler<AddDoctorNoteCommand, bool>
{
    public AddDoctorNoteCommandHandler() { throw new NotImplementedException(); }
    public Task<bool> Handle(AddDoctorNoteCommand request, CancellationToken ct)
        => throw new NotImplementedException();
}
