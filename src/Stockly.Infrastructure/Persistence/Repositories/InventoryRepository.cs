using Microsoft.EntityFrameworkCore;
using Stockly.Domain.Abstractions.Persistence;
using Stockly.Domain.Entities;

namespace Stockly.Infrastructure.Persistence.Repositories;

public class InventoryRepository : BaseRepository<Inventory>, IInventoryRepository
{
    public InventoryRepository(StocklyDbContext context) : base(context)
    {
    }

    public async Task<Inventory?> GetByProductVariationAsync(Guid productVariationId, CancellationToken cancellationToken = default) =>
        await DbSet.AsNoTracking()
            .FirstOrDefaultAsync(i => i.ProductVariationId == productVariationId, cancellationToken);

    public async Task<IEnumerable<Inventory>> GetBelowMinimumAsync(CancellationToken cancellationToken = default) =>
        await DbSet.AsNoTracking()
            .Where(i => i.CurrentQuantity < i.MinimumQuantity)
            .ToListAsync(cancellationToken);
}
