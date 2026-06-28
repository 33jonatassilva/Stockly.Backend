using Stockly.Domain.Entities;

namespace Stockly.Domain.Abstractions.Persistence
{
    public interface IBrandRepository : IBaseRepository<Brand>
    {
        Task<Brand?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
        Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default);
    }
}
