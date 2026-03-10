namespace Etmen.Domain.Common;

/// <summary>
/// Abstract base for all domain entities.
/// Provides soft-delete, audit timestamps, and a GUID primary key.
/// </summary>
public abstract class BaseEntity
{
    public Guid     Id        { get; protected set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; protected set; }
    public bool     IsDeleted { get; protected set; } = false;

    protected void MarkUpdated() => UpdatedAt = DateTime.UtcNow;
    protected void SoftDelete()  => IsDeleted  = true;
}
