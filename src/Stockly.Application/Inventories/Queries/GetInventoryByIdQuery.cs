using MediatR;
using Stockly.Application.Common.Exceptions;
using Stockly.Domain.Abstractions.Persistence;

namespace Stockly.Application.Inventories.Queries;

public record GetInventoryByIdQuery(Guid Id) : IRequest<InventoryDto>;

public class GetInventoryByIdQueryHandler : IRequestHandler<GetInventoryByIdQuery, InventoryDto>
{
    private readonly IInventoryRepository _repository;

    public GetInventoryByIdQueryHandler(IInventoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<InventoryDto> Handle(GetInventoryByIdQuery request, CancellationToken cancellationToken)
    {
        var inventory = await _repository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException("Estoque", request.Id);

        return new InventoryDto(
            inventory.Id,
            inventory.ProductVariationId,
            inventory.CurrentQuantity,
            inventory.MinimumQuantity,
            inventory.AcceptableQuantity,
            inventory.IdealQuantity,
            inventory.UpdatedAt);
    }
}
