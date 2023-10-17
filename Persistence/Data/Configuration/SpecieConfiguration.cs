using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Persistence.Data.Configuration;
public class SpecieConfiguration : IEntityTypeConfiguration<Specie>
{
    public void Configure(EntityTypeBuilder<Specie> builder)
    {
        {
            builder.ToTable("specie");
            builder.Property(p => p.Name)
            .HasMaxLength(100)
            .IsRequired();
            builder.HasIndex(p => p.Name)
            .IsUnique();
        }
    }
}