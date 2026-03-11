using Etmen.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Etmen.Infrastructure.Persistence.Configurations;

/// <summary>EF Core fluent configuration for the Alert entity.</summary>
public sealed class AlertConfiguration : IEntityTypeConfiguration<Alert>
{
    public void Configure(EntityTypeBuilder<Alert> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.CreatedAt).IsRequired();
        builder.Property(e => e.IsDeleted).HasDefaultValue(false);
        // TODO: Add specific column mappings, indexes, and FK relationships
    }
}
