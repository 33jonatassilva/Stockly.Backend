using Microsoft.EntityFrameworkCore;
using Stockly.Domain.Entities;

namespace Stockly.Infrastructure.Persistence
{
    public class StocklyDbContext : DbContext
    {
        public StocklyDbContext(DbContextOptions<StocklyDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<PurchaseItem> PurchaseItems { get; set; }
        public DbSet<ProductVariation> ProductVariations { get; set; }
        public DbSet<InventoryMovement> InventoryMovements { get; set; }
    }
}
