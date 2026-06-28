using Stockly.Domain.Entities;

namespace Stockly.Domain.Abstractions.Persistence
{
    public interface IPurchaseRepository : IBaseRepository<Purchase>
    {
        Task<Purchase?> GetWithItemsAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<Purchase>> GetByPeriodAsync(DateTime start, DateTime end, CancellationToken cancellationToken = default);
    }
}
