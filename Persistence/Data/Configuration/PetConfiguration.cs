using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Persistence.Data.Configuration;
public class PetConfiguration : IEntityTypeConfiguration<Pet>
{
    public void Configure(EntityTypeBuilder<Pet> builder)
    {
        {
            builder.ToTable("pet");
            builder.Property(p => p.Name)
            .HasMaxLength(100)
            .IsRequired();
            builder.Property(p => p.BirthDate)
            .IsRequired();
            builder.HasOne(p => p.Owner)
            .WithMany(f => f.Pets)
            .HasForeignKey(fk => fk.IdOwner)
            .IsRequired();
            builder.HasOne(p => p.Specie)
            .WithMany(f => f.Pets)
            .HasForeignKey(fk => fk.IdSpecie)
            .IsRequired();
            builder.HasOne(p => p.Race)
            .WithMany(f => f.Pets)
            .HasForeignKey(fk => fk.IdRace)
            .IsRequired();
        }
    }
}