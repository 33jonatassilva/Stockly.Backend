namespace Stockly.Domain.Entities
{
    public class Brand
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }
}
