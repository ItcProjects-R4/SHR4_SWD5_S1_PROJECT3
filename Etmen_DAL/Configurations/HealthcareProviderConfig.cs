using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Etmen_Domain.Entities;

namespace Etmen_DAL.Configurations
{
    public class HealthcareProviderConfig : IEntityTypeConfiguration<HealthcareProvider>
    {
        public void Configure(EntityTypeBuilder<HealthcareProvider> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(x => x.Type)
                .IsRequired();

            builder.Property(x => x.Latitude)
                .HasColumnType("decimal(9,6)")
                .IsRequired();

            builder.Property(x => x.Longitude)
                .HasColumnType("decimal(9,6)")
                .IsRequired();

            builder.Property(x => x.Address)
                .HasMaxLength(300);

            builder.Property(x => x.Phone)
                .HasMaxLength(20);

            builder.Property(x => x.CreatedAt)
                .IsRequired();

            builder.HasIndex(x => x.Name);
        }
    }
}