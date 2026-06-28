namespace Stockly.Domain.Entities
{
    public class ProductVariation
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string PackageDescription { get; set; } = null!;
        public decimal QuantityPerPackage { get; set; }
        public string Unit { get; set; } = null!;

        public Product Product { get; set; } = null!;
    }
}
