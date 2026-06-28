using MediatR;
using Stockly.Application.Common.Exceptions;
using Stockly.Domain.Abstractions.Persistence;
using Stockly.Domain.Entities;

namespace Stockly.Application.Purchases.Commands;

public record CreatePurchaseItemInput(Guid ProductVariationId, decimal Quantity, decimal UnitPrice);

public record CreatePurchaseCommand(
    string StoreName,
    DateTime? PurchaseDate,
    string? Notes,
    IReadOnlyList<CreatePurchaseItemInput> Items) : IRequest<PurchaseDto>;

public class CreatePurchaseCommandHandler : IRequestHandler<CreatePurchaseCommand, PurchaseDto>
{
    private readonly IPurchaseRepository _repository;
    private readonly IProductVariationRepository _variationRepository;

    public CreatePurchaseCommandHandler(
        IPurchaseRepository repository,
        IProductVariationRepository variationRepository)
    {
        _repository = repository;
        _variationRepository = variationRepository;
    }

    public async Task<PurchaseDto> Handle(CreatePurchaseCommand request, CancellationToken cancellationToken)
    {
        if (request.Items is null || request.Items.Count == 0)
        {
            throw new ValidationException("A compra deve ter pelo menos um item.");
        }

        var purchaseId = Guid.NewGuid();
        var items = new List<PurchaseItem>();

        foreach (var input in request.Items)
        {
            if (input.Quantity <= 0)
            {
                throw new ValidationException("A quantidade de cada item deve ser maior que zero.");
            }

            if (input.UnitPrice < 0)
            {
                throw new ValidationException("O preço unitário não pode ser negativo.");
            }

            _ = await _variationRepository.GetByIdAsync(input.ProductVariationId, cancellationToken)
                ?? throw new NotFoundException("Variação de produto", input.ProductVariationId);

            items.Add(new PurchaseItem
            {
                Id = Guid.NewGuid(),
                PurchaseId = purchaseId,
                ProductVariationId = input.ProductVariationId,
                Quantity = input.Quantity,
                UnitPrice = input.UnitPrice,
                TotalPrice = input.Quantity * input.UnitPrice
            });
        }

        var purchase = new Purchase
        {
            Id = purchaseId,
            PurchaseDate = request.PurchaseDate ?? DateTime.UtcNow,
            StoreName = request.StoreName,
            Notes = request.Notes,
            TotalValue = items.Sum(i => i.TotalPrice),
            PurchaseItems = items
        };

        await _repository.AddAsync(purchase, cancellationToken);

        return new PurchaseDto(
            purchase.Id,
            purchase.PurchaseDate,
            purchase.StoreName,
            purchase.TotalValue,
            purchase.Notes,
            items.Select(i => new PurchaseItemDto(i.Id, i.ProductVariationId, i.Quantity, i.UnitPrice, i.TotalPrice)).ToList());
    }
}
