using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Persistence.Data.Configuration;
public class TypeMovementConfiguration : IEntityTypeConfiguration<TypeMovement>
{
    public void Configure(EntityTypeBuilder<TypeMovement> builder)
    {
        {
            builder.ToTable("typeMovement");
            builder.Property(p => p.Name)
            .HasMaxLength(100)
            .IsRequired();
            builder.HasIndex(p => p.Name)
            .IsUnique();
        }
    }
}