using Microsoft.EntityFrameworkCore;
using Stockly.Domain.Abstractions.Persistence;
using Stockly.Domain.Entities;

namespace Stockly.Infrastructure.Persistence.Repositories;

public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
{
    public CategoryRepository(StocklyDbContext context) : base(context)
    {
    }

    public async Task<Category?> GetByNameAsync(string name, CancellationToken cancellationToken = default) =>
        await DbSet.AsNoTracking()
            .FirstOrDefaultAsync(c => c.Name == name, cancellationToken);

    public async Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default) =>
        await DbSet.AnyAsync(c => c.Name == name, cancellationToken);
}
