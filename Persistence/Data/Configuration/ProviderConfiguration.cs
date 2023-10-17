using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Persistence.Data.Configuration;
public class ProviderConfiguration : IEntityTypeConfiguration<Provider>
{
    public void Configure(EntityTypeBuilder<Provider> builder)
    {
        {
            builder.ToTable("provider");
            builder.Property(p => p.Name)
            .HasMaxLength(100)
            .IsRequired();
            builder.HasIndex(p => p.Name)
            .IsUnique();
            builder.Property(p => p.Address)
            .HasMaxLength(150)
            .IsRequired();
            builder.Property(p => p.PhoneNumber)
            .HasMaxLength(15)
            .IsRequired();
        }
    }
}