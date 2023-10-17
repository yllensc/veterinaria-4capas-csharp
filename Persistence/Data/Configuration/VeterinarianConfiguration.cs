using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Persistence.Data.Configuration;
public class VeterinarianConfiguration : IEntityTypeConfiguration<Veterinarian>
{
    public void Configure(EntityTypeBuilder<Veterinarian> builder)
    {
        {
            builder.ToTable("veterinarian");
            builder.Property(p => p.Name)
            .HasMaxLength(100)
            .IsRequired();
            builder.Property(p => p.PhoneNumber)
            .HasMaxLength(15)
            .IsRequired();
            builder.Property(p => p.Specialty)
            .HasMaxLength(200)
            .IsRequired();
            builder.HasOne(p => p.User)
            .WithMany(f => f.Veterinarians)
            .HasForeignKey(fk => fk.IdUser)
            .IsRequired();
        }
    }
}