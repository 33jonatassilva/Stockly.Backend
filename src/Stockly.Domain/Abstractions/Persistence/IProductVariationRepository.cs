using Stockly.Domain.Entities;

namespace Stockly.Domain.Abstractions.Persistence
{
    public interface IProductVariationRepository : IBaseRepository<ProductVariation>
    {
        Task<ProductVariation?> GetWithProductAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<ProductVariation>> GetByProductAsync(Guid productId, CancellationToken cancellationToken = default);
    }
}
