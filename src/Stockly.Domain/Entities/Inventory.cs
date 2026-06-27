namespace Stockly.Domain.Entities
{
    public class Inventory
    {
        public Guid Id { get; set; }
        public Guid ProductVariationId { get; set; }
        public decimal CurrentQuantity { get; set; }
        public decimal MinimumQuantity { get; set; }
        public decimal AcceptableQuantity { get; set; }
        public decimal IdealQuantity { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
