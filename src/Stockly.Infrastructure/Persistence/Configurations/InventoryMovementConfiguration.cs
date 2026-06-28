using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stockly.Domain.Entities;

namespace Stockly.Infrastructure.Persistence.Configurations;

public class InventoryMovementConfiguration : IEntityTypeConfiguration<InventoryMovement>
{
    public void Configure(EntityTypeBuilder<InventoryMovement> builder)
    {
        builder.ToTable("InventoryMovements");

        builder.HasKey(m => m.Id);

        builder.Property(m => m.Movement)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.Property(m => m.Quantity)
            .HasPrecision(18, 3);

        builder.Property(m => m.MovementDate)
            .IsRequired();

        builder.Property(m => m.Notes)
            .HasMaxLength(500);

        builder.HasOne(m => m.ProductVariation)
            .WithMany()
            .HasForeignKey(m => m.ProductVariationId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(m => m.PurchaseItem)
            .WithMany()
            .HasForeignKey(m => m.PurchaseItemId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(m => m.ProductVariationId);
        builder.HasIndex(m => m.PurchaseItemId);
        builder.HasIndex(m => m.MovementDate);
    }
}
