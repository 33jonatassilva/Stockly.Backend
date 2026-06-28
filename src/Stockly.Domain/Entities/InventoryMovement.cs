using Stockly.Domain.Enums;

namespace Stockly.Domain.Entities
{
    public class InventoryMovement
    {
        public Guid Id { get; set; }
        public Guid ProductVariationId { get; set; }
        public MovementType Movement { get; set; }
        public decimal Quantity { get; set; }
        public DateTime MovementDate { get; set; }
        public Guid? PurchaseItemId { get; set; }
        public string? Notes { get; set; }


        public ProductVariation ProductVariation { get; set; } = null!;
        public PurchaseItem? PurchaseItem { get; set; }
    }
}
