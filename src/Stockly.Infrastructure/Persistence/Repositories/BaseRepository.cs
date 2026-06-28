using Microsoft.EntityFrameworkCore;
using Stockly.Domain.Abstractions.Persistence;

namespace Stockly.Infrastructure.Persistence.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : class
{
    protected readonly StocklyDbContext Context;
    protected readonly DbSet<T> DbSet;

    public BaseRepository(StocklyDbContext context)
    {
        Context = context;
        DbSet = context.Set<T>();
    }

    public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        await DbSet.AddAsync(entity, cancellationToken);
        await Context.SaveChangesAsync(cancellationToken);
    }

    public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        await DbSet.FindAsync([id], cancellationToken);

    public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default) =>
        await DbSet.AsNoTracking().ToListAsync(cancellationToken);

    public async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        DbSet.Update(entity);
        await Context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
    {
        DbSet.Remove(entity);
        await Context.SaveChangesAsync(cancellationToken);
    }
}
