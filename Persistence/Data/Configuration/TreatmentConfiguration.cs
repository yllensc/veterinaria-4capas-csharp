using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Persistence.Data.Configuration;
public class TreatmentConfiguration : IEntityTypeConfiguration<Treatment>
{
    public void Configure(EntityTypeBuilder<Treatment> builder)
    {
        {
            builder.ToTable("Treatment");
            builder.Property(p => p.Dosage)
            .HasMaxLength(100)
            .IsRequired();
            builder.Property(p => p.DateAdministration)
            .IsRequired();
            builder.Property(p => p.Observations)
            .HasMaxLength(100)
            .IsRequired();
            builder.HasOne(p => p.Appointment)
            .WithMany(f => f.Treatments)
            .HasForeignKey(fk => fk.IdAppointment)
            .IsRequired();
            builder.HasOne(p => p.Medicine)
            .WithMany(f => f.Treatments)
            .HasForeignKey(fk => fk.IdMedicine)
            .IsRequired();
        }
    }
}