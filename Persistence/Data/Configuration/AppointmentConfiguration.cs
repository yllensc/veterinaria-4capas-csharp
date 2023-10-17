using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;
public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
{
    public void Configure(EntityTypeBuilder<Appointment> builder)
    {
        {
            builder.ToTable("appointment");
            builder.Property(p => p.DateAppointment)
            .HasColumnType("Date")
            .IsRequired();
            builder.Property(p => p.Hour)
            .HasColumnType("Time")
            .IsRequired();
            builder.Property(p => p.Cause)
            .IsRequired();
            builder.HasOne(p => p.Veterinarian)
            .WithMany(f => f.Appointments)
            .HasForeignKey(fk => fk.IdVeterinarian)
            .IsRequired();
            builder.HasOne(p => p.Pet)
            .WithMany(f => f.Appointments)
            .HasForeignKey(fk => fk.IdPet)
            .IsRequired();
        }
    }
}