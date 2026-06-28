using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stockly.Domain.Entities;

namespace Stockly.Infrastructure.Persistence.Configurations;

public class InventoryConfiguration : IEntityTypeConfiguration<Inventory>
{
    public void Configure(EntityTypeBuilder<Inventory> builder)
    {
        builder.ToTable("Inventories");

        builder.HasKey(i => i.Id);

        builder.Property(i => i.CurrentQuantity)
            .HasPrecision(18, 3);

        builder.Property(i => i.MinimumQuantity)
            .HasPrecision(18, 3);

        builder.Property(i => i.AcceptableQuantity)
            .HasPrecision(18, 3);

        builder.Property(i => i.IdealQuantity)
            .HasPrecision(18, 3);

        builder.Property(i => i.UpdatedAt)
            .IsRequired();

        builder.HasOne<ProductVariation>()
            .WithOne()
            .HasForeignKey<Inventory>(i => i.ProductVariationId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(i => i.ProductVariationId)
            .IsUnique();
    }
}
