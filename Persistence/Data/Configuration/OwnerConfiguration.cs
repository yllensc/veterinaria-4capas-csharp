using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Persistence.Data.Configuration;
public class OwnerConfiguration : IEntityTypeConfiguration<Owner>
{
    public void Configure(EntityTypeBuilder<Owner> builder)
    {
        {
            builder.ToTable("owner");
            builder.Property(p => p.Name)
            .HasMaxLength(100)
            .IsRequired();
            builder.Property(p => p.Email)
            .HasMaxLength(150)
            .IsRequired();
            builder.HasIndex(p => p.Email)
            .IsUnique();
            builder.Property(p => p.PhoneNumber)
            .HasMaxLength(100)
            .IsRequired();
        }
    }
}