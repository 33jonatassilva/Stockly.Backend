namespace Stockly.Domain.Entities
{
    public class Purchase
    {
        public Guid Id { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string StoreName { get; set; }
        public decimal TotalValue { get; set; }
        public string Notes { get; set; }

        public ICollection<PurchaseItem> PurchaseItems { get; set; }
    }
}
