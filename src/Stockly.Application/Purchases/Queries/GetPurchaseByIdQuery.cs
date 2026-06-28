using MediatR;
using Stockly.Application.Common.Exceptions;
using Stockly.Domain.Abstractions.Persistence;

namespace Stockly.Application.Purchases.Queries;

public record GetPurchaseByIdQuery(Guid Id) : IRequest<PurchaseDto>;

public class GetPurchaseByIdQueryHandler : IRequestHandler<GetPurchaseByIdQuery, PurchaseDto>
{
    private readonly IPurchaseRepository _repository;

    public GetPurchaseByIdQueryHandler(IPurchaseRepository repository)
    {
        _repository = repository;
    }

    public async Task<PurchaseDto> Handle(GetPurchaseByIdQuery request, CancellationToken cancellationToken)
    {
        var purchase = await _repository.GetWithItemsAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException("Compra", request.Id);

        var items = purchase.PurchaseItems
            .Select(i => new PurchaseItemDto(i.Id, i.ProductVariationId, i.Quantity, i.UnitPrice, i.TotalPrice))
            .ToList();

        return new PurchaseDto(
            purchase.Id,
            purchase.PurchaseDate,
            purchase.StoreName,
            purchase.TotalValue,
            purchase.Notes,
            items);
    }
}
