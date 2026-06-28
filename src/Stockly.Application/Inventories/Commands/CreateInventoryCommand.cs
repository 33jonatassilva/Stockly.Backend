using MediatR;
using Stockly.Application.Common.Exceptions;
using Stockly.Domain.Abstractions.Persistence;
using Stockly.Domain.Entities;

namespace Stockly.Application.Inventories.Commands;

public record CreateInventoryCommand(
    Guid ProductVariationId,
    decimal CurrentQuantity,
    decimal MinimumQuantity,
    decimal AcceptableQuantity,
    decimal IdealQuantity) : IRequest<InventoryDto>;

public class CreateInventoryCommandHandler : IRequestHandler<CreateInventoryCommand, InventoryDto>
{
    private readonly IInventoryRepository _repository;
    private readonly IProductVariationRepository _variationRepository;

    public CreateInventoryCommandHandler(
        IInventoryRepository repository,
        IProductVariationRepository variationRepository)
    {
        _repository = repository;
        _variationRepository = variationRepository;
    }

    public async Task<InventoryDto> Handle(CreateInventoryCommand request, CancellationToken cancellationToken)
    {
        _ = await _variationRepository.GetByIdAsync(request.ProductVariationId, cancellationToken)
            ?? throw new NotFoundException("Variação de produto", request.ProductVariationId);

        var existing = await _repository.GetByProductVariationAsync(request.ProductVariationId, cancellationToken);
        if (existing is not null)
        {
            throw new ConflictException("Já existe um estoque para essa variação de produto.");
        }

        var inventory = new Inventory
        {
            Id = Guid.NewGuid(),
            ProductVariationId = request.ProductVariationId,
            CurrentQuantity = request.CurrentQuantity,
            MinimumQuantity = request.MinimumQuantity,
            AcceptableQuantity = request.AcceptableQuantity,
            IdealQuantity = request.IdealQuantity,
            UpdatedAt = DateTime.UtcNow
        };

        await _repository.AddAsync(inventory, cancellationToken);

        return Map(inventory);
    }

    private static InventoryDto Map(Inventory i) => new(
        i.Id, i.ProductVariationId, i.CurrentQuantity, i.MinimumQuantity, i.AcceptableQuantity, i.IdealQuantity, i.UpdatedAt);
}
