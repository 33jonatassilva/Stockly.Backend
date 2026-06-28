using Microsoft.EntityFrameworkCore;
using Stockly.Domain.Abstractions.Persistence;
using Stockly.Domain.Entities;

namespace Stockly.Infrastructure.Persistence.Repositories;

public class ProductRepository : BaseRepository<Product>, IProductRepository
{
    public ProductRepository(StocklyDbContext context) : base(context)
    {
    }

    public async Task<Product?> GetWithVariationsAsync(Guid id, CancellationToken cancellationToken = default) =>
        await DbSet.AsNoTracking()
            .Include(p => p.ProductVariations)
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

    public async Task<IEnumerable<Product>> GetByCategoryAsync(Guid categoryId, CancellationToken cancellationToken = default) =>
        await DbSet.AsNoTracking()
            .Where(p => p.CategoryId == categoryId)
            .ToListAsync(cancellationToken);

    public async Task<IEnumerable<Product>> GetByBrandAsync(Guid brandId, CancellationToken cancellationToken = default) =>
        await DbSet.AsNoTracking()
            .Where(p => p.BrandId == brandId)
            .ToListAsync(cancellationToken);

    public async Task<IEnumerable<Product>> GetActiveAsync(CancellationToken cancellationToken = default) =>
        await DbSet.AsNoTracking()
            .Where(p => p.IsActive)
            .ToListAsync(cancellationToken);
}
