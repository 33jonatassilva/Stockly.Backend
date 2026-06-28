using MediatR;
using Stockly.Domain.Abstractions.Persistence;

namespace Stockly.Application.Inventories.Queries;

public record GetAllInventoriesQuery(bool OnlyBelowMinimum = false) : IRequest<IReadOnlyList<InventoryDto>>;

public class GetAllInventoriesQueryHandler : IRequestHandler<GetAllInventoriesQuery, IReadOnlyList<InventoryDto>>
{
    private readonly IInventoryRepository _repository;

    public GetAllInventoriesQueryHandler(IInventoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<InventoryDto>> Handle(GetAllInventoriesQuery request, CancellationToken cancellationToken)
    {
        var inventories = request.OnlyBelowMinimum
            ? await _repository.GetBelowMinimumAsync(cancellationToken)
            : await _repository.GetAllAsync(cancellationToken);

        return inventories
            .Select(i => new InventoryDto(
                i.Id, i.ProductVariationId, i.CurrentQuantity, i.MinimumQuantity, i.AcceptableQuantity, i.IdealQuantity, i.UpdatedAt))
            .ToList();
    }
}
