using Stockly.Domain.Entities;

namespace Stockly.Domain.Abstractions.Persistence
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        Task<Category?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
        Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default);
    }
}
