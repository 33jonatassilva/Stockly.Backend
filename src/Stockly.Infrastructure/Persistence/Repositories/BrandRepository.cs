using Microsoft.EntityFrameworkCore;
using Stockly.Domain.Abstractions.Persistence;
using Stockly.Domain.Entities;

namespace Stockly.Infrastructure.Persistence.Repositories;

public class BrandRepository : BaseRepository<Brand>, IBrandRepository
{
    public BrandRepository(StocklyDbContext context) : base(context)
    {
    }

    public async Task<Brand?> GetByNameAsync(string name, CancellationToken cancellationToken = default) =>
        await DbSet.AsNoTracking()
            .FirstOrDefaultAsync(b => b.Name == name, cancellationToken);

    public async Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default) =>
        await DbSet.AnyAsync(b => b.Name == name, cancellationToken);
}
