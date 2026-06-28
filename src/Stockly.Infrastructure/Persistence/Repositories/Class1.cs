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

    public async Task AddAsync(T entity)
    {
        await DbSet.AddAsync(entity);
        await Context.SaveChangesAsync();
    }

    public async Task<T> GetByIdAsync(Guid id) =>
        await DbSet.FindAsync(id) ?? throw new KeyNotFoundException();

    public async Task<IEnumerable<T>> GetAllAsync() =>
        await DbSet.ToListAsync();

    public async Task UpdateAsync(T entity)
    {
        DbSet.Update(entity);
        await Context.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        DbSet.Remove(entity);
        await Context.SaveChangesAsync();
    }
}