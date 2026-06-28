using MediatR;
using Stockly.Application.Common.Exceptions;
using Stockly.Domain.Abstractions.Persistence;

namespace Stockly.Application.Inventories.Commands;

public record UpdateInventoryLevelsCommand(
    Guid Id,
    decimal MinimumQuantity,
    decimal AcceptableQuantity,
    decimal IdealQuantity) : IRequest<InventoryDto>;

public class UpdateInventoryLevelsCommandHandler : IRequestHandler<UpdateInventoryLevelsCommand, InventoryDto>
{
    private readonly IInventoryRepository _repository;

    public UpdateInventoryLevelsCommandHandler(IInventoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<InventoryDto> Handle(UpdateInventoryLevelsCommand request, CancellationToken cancellationToken)
    {
        var inventory = await _repository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException("Estoque", request.Id);

        inventory.MinimumQuantity = request.MinimumQuantity;
        inventory.AcceptableQuantity = request.AcceptableQuantity;
        inventory.IdealQuantity = request.IdealQuantity;
        inventory.UpdatedAt = DateTime.UtcNow;

        await _repository.UpdateAsync(inventory, cancellationToken);

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
