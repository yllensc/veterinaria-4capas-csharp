using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Persistence.Data.Configuration;
public class RaceConfiguration : IEntityTypeConfiguration<Race>
{
    public void Configure(EntityTypeBuilder<Race> builder)
    {
        {
            builder.ToTable("race");
            builder.Property(p => p.Name)
            .HasMaxLength(100)
            .IsRequired();
            builder.HasIndex(p => p.Name)
            .IsUnique();
            builder.HasOne(p => p.Specie)
            .WithMany(f => f.Races)
            .HasForeignKey(fk => fk.IdSpecie)
            .IsRequired();
        }
    }
}