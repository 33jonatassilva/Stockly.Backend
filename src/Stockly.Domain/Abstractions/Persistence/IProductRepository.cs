using Stockly.Domain.Entities;

namespace Stockly.Domain.Abstractions.Persistence
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<Product?> GetWithVariationsAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<Product>> GetByCategoryAsync(Guid categoryId, CancellationToken cancellationToken = default);
        Task<IEnumerable<Product>> GetByBrandAsync(Guid brandId, CancellationToken cancellationToken = default);
        Task<IEnumerable<Product>> GetActiveAsync(CancellationToken cancellationToken = default);
    }
}
