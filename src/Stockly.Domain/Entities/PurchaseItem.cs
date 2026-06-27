using System;
using System.Collections.Generic;
using System.Text;

namespace Stockly.Domain.Entities
{
    public class PurchaseItem
    {
        public Guid Id { get; set; }
        public Guid PurchaseId { get; set; }
        public Guid ProductVariationId { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }

        public Purchase Purchase { get; set; }
        public ProductVariation ProductVariation { get; set; }
    }
}
