using Stockly.Domain.Entities;

namespace Stockly.Domain.Abstractions.Persistence
{
    public interface IInventoryMovementRepository : IBaseRepository<InventoryMovement>
    {
        Task<IEnumerable<InventoryMovement>> GetByProductVariationAsync(Guid productVariationId, CancellationToken cancellationToken = default);
        Task<IEnumerable<InventoryMovement>> GetByPeriodAsync(DateTime start, DateTime end, CancellationToken cancellationToken = default);
    }
}
