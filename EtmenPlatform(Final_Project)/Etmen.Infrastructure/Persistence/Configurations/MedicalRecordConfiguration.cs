using Etmen.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Etmen.Infrastructure.Persistence.Configurations;

/// <summary>EF Core fluent configuration for the MedicalRecord entity.</summary>
public sealed class MedicalRecordConfiguration : IEntityTypeConfiguration<MedicalRecord>
{
    public void Configure(EntityTypeBuilder<MedicalRecord> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.CreatedAt).IsRequired();
        builder.Property(e => e.IsDeleted).HasDefaultValue(false);
        // TODO: Add specific column mappings, indexes, and FK relationships
    }
}
