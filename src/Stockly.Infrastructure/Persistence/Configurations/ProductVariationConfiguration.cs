using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stockly.Domain.Entities;

namespace Stockly.Infrastructure.Persistence.Configurations;

public class ProductVariationConfiguration : IEntityTypeConfiguration<ProductVariation>
{
    public void Configure(EntityTypeBuilder<ProductVariation> builder)
    {
        builder.ToTable("ProductVariations");

        builder.HasKey(v => v.Id);

        builder.Property(v => v.PackageDescription)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(v => v.QuantityPerPackage)
            .HasPrecision(18, 3);

        builder.Property(v => v.Unit)
            .IsRequired()
            .HasMaxLength(20);

        builder.Ignore(v => v.Brand);

        builder.HasOne(v => v.Product)
            .WithMany(p => p.ProductVariations)
            .HasForeignKey(v => v.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(v => v.ProductId);
    }
}
