using Microsoft.EntityFrameworkCore;
using Stockly.Domain.Abstractions.Persistence;
using Stockly.Domain.Entities;

namespace Stockly.Infrastructure.Persistence.Repositories;

public class PurchaseRepository : BaseRepository<Purchase>, IPurchaseRepository
{
    public PurchaseRepository(StocklyDbContext context) : base(context)
    {
    }

    public async Task<Purchase?> GetWithItemsAsync(Guid id, CancellationToken cancellationToken = default) =>
        await DbSet.AsNoTracking()
            .Include(p => p.PurchaseItems)
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

    public async Task<IEnumerable<Purchase>> GetByPeriodAsync(DateTime start, DateTime end, CancellationToken cancellationToken = default) =>
        await DbSet.AsNoTracking()
            .Where(p => p.PurchaseDate >= start && p.PurchaseDate <= end)
            .OrderByDescending(p => p.PurchaseDate)
            .ToListAsync(cancellationToken);
}
