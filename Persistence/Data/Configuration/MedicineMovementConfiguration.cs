using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Persistence.Data.Configuration;
public class MedicineMovementConfiguration : IEntityTypeConfiguration<MedicineMovement>
{
    public void Configure(EntityTypeBuilder<MedicineMovement> builder)
    {
        {
            builder.ToTable("medicineMovement");
            builder.Property(p => p.Quantity)
            .IsRequired();
            builder.Property(p => p.DateMovement)
            .IsRequired();
            builder.Property(p => p.PriceUnit)
            .IsRequired();
            builder.HasOne(p => p.Medicine)
            .WithMany(f => f.MedicineMovements)
            .HasForeignKey(fk => fk.IdMedicine)
            .IsRequired();
            builder.HasOne(p => p.TypeMovement)
            .WithMany(f => f.MedicineMovements)
            .HasForeignKey(fk => fk.IdTypeMovement)
            .IsRequired();
        }
    }
}