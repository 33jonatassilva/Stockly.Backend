using Microsoft.EntityFrameworkCore;
using Stockly.Domain.Abstractions.Persistence;
using Stockly.Domain.Entities;

namespace Stockly.Infrastructure.Persistence.Repositories;

public class InventoryMovementRepository : BaseRepository<InventoryMovement>, IInventoryMovementRepository
{
    public InventoryMovementRepository(StocklyDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<InventoryMovement>> GetByProductVariationAsync(Guid productVariationId, CancellationToken cancellationToken = default) =>
        await DbSet.AsNoTracking()
            .Where(m => m.ProductVariationId == productVariationId)
            .OrderByDescending(m => m.MovementDate)
            .ToListAsync(cancellationToken);

    public async Task<IEnumerable<InventoryMovement>> GetByPeriodAsync(DateTime start, DateTime end, CancellationToken cancellationToken = default) =>
        await DbSet.AsNoTracking()
            .Where(m => m.MovementDate >= start && m.MovementDate <= end)
            .OrderByDescending(m => m.MovementDate)
            .ToListAsync(cancellationToken);
}
