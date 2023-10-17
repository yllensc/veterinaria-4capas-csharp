using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Persistence.Data.Configuration;
public class MedicineConfiguration : IEntityTypeConfiguration<Medicine>
{
    public void Configure(EntityTypeBuilder<Medicine> builder)
    {
        {
            builder.ToTable("medicine");
            builder.Property(p => p.Name)
            .HasMaxLength(100)
            .IsRequired();
            builder.Property(p => p.QuantityDisp)
            .IsRequired();
            builder.HasOne(p => p.Laboratory)
            .WithMany(f => f.Medicines)
            .HasForeignKey(fk => fk.IdLaboratory)
            .IsRequired();
            builder.HasOne(p => p.Provider)
            .WithMany(f => f.Medicines)
            .HasForeignKey(fk => fk.IdProvider)
            .IsRequired();
        }
    }
}