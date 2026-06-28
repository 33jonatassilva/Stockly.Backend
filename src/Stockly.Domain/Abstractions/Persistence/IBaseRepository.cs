namespace Stockly.Domain.Abstractions.Persistence
{
    public interface IBaseRepository<T> where T : class
    {
        public Task AddAsync(T entity);
        public Task<T> GetByIdAsync(Guid id);
        public Task<IEnumerable<T>> GetAllAsync();
        public Task UpdateAsync(T entity);
        public Task DeleteAsync(T entity);

    }
}
