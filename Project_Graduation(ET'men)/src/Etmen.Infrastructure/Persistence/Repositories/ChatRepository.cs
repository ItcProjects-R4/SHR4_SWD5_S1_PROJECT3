using Etmen.Application.Interfaces;
using Etmen.Domain.Entities;
using Etmen.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Etmen.Infrastructure.Persistence.Repositories;

/// <summary>EF Core implementation of IChatRepository. Handles message persistence and read-status.</summary>
public class ChatRepository : IChatRepository
{
    private readonly AppDbContext _db;
    public ChatRepository(AppDbContext db) => _db = db;

    public async Task<ChatMessage> SaveMessageAsync(ChatMessage msg, CancellationToken ct = default)
    {
        _db.ChatMessages.Add(msg);
        await _db.SaveChangesAsync(ct);
        return msg;
    }

    public async Task<IEnumerable<ChatMessage>> GetConversationAsync(Guid patientId, Guid doctorId, CancellationToken ct = default)
        => await _db.ChatMessages
            .Where(m => m.PatientId == patientId &&
                       (m.SenderId == doctorId || m.RecipientId == doctorId))
            .OrderBy(m => m.CreatedAt)
            .ToListAsync(ct);

    public async Task<IEnumerable<object>> GetAllConversationsForDoctorAsync(Guid doctorId, CancellationToken ct = default)
        => throw new NotImplementedException();

    public async Task MarkAsReadAsync(Guid conversationPatientId, Guid readerId, CancellationToken ct = default)
    {
        var unread = await _db.ChatMessages
            .Where(m => m.PatientId == conversationPatientId
                     && m.RecipientId == readerId && !m.IsRead)
            .ToListAsync(ct);
        unread.ForEach(m => m.MarkAsRead());
        await _db.SaveChangesAsync(ct);
    }
}
