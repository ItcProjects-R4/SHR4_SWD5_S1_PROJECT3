using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Etmen_Domain.Entities;

namespace Etmen_DAL.Configurations
{
    public class CrisisConfigurationConfig : IEntityTypeConfiguration<CrisisConfiguration>
    {
        public void Configure(EntityTypeBuilder<CrisisConfiguration> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.CrisisName)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(x => x.Description)
                .HasMaxLength(1000);

            builder.Property(x => x.EmergencyThreshold)
                .HasColumnType("decimal(18,2)");

            builder.Property(x => x.HighRiskThreshold)
                .HasColumnType("decimal(18,2)");

            builder.Property(x => x.MediumRiskThreshold)
                .HasColumnType("decimal(18,2)");

            builder.Property(x => x.StartDate)
                .IsRequired();

            builder.Property(x => x.CreatedAt)
                .IsRequired();

            builder.HasMany(x => x.SymptomWeights)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.OutbreakZones)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(x => x.CrisisName);
        }
    }
}