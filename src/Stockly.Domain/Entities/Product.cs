namespace Stockly.Domain.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public Guid CategoryId { get; set; }
        public Guid BrandId { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }
        public Brand Brand { get; set; }
        public Category Category { get; set; }

        public virtual ICollection<ProductVariation> ProductVariations { get; set; }


    }
}
