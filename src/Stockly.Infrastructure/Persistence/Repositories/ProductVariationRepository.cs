using Microsoft.EntityFrameworkCore;
using Stockly.Domain.Abstractions.Persistence;
using Stockly.Domain.Entities;

namespace Stockly.Infrastructure.Persistence.Repositories;

public class ProductVariationRepository : BaseRepository<ProductVariation>, IProductVariationRepository
{
    public ProductVariationRepository(StocklyDbContext context) : base(context)
    {
    }

    public async Task<ProductVariation?> GetWithProductAsync(Guid id, CancellationToken cancellationToken = default) =>
        await DbSet.AsNoTracking()
            .Include(v => v.Product)
            .FirstOrDefaultAsync(v => v.Id == id, cancellationToken);

    public async Task<IEnumerable<ProductVariation>> GetByProductAsync(Guid productId, CancellationToken cancellationToken = default) =>
        await DbSet.AsNoTracking()
            .Where(v => v.ProductId == productId)
            .ToListAsync(cancellationToken);
}
