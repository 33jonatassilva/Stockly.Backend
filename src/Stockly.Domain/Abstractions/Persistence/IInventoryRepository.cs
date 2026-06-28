using Stockly.Domain.Entities;

namespace Stockly.Domain.Abstractions.Persistence
{
    public interface IInventoryRepository : IBaseRepository<Inventory>
    {
        Task<Inventory?> GetByProductVariationAsync(Guid productVariationId, CancellationToken cancellationToken = default);
        Task<IEnumerable<Inventory>> GetBelowMinimumAsync(CancellationToken cancellationToken = default);
    }
}
