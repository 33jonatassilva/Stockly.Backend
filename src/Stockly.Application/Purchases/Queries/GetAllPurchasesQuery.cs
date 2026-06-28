using MediatR;
using Stockly.Domain.Abstractions.Persistence;

namespace Stockly.Application.Purchases.Queries;

public record GetAllPurchasesQuery(DateTime? Start, DateTime? End) : IRequest<IReadOnlyList<PurchaseSummaryDto>>;

public class GetAllPurchasesQueryHandler : IRequestHandler<GetAllPurchasesQuery, IReadOnlyList<PurchaseSummaryDto>>
{
    private readonly IPurchaseRepository _repository;

    public GetAllPurchasesQueryHandler(IPurchaseRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<PurchaseSummaryDto>> Handle(GetAllPurchasesQuery request, CancellationToken cancellationToken)
    {
        var purchases = request.Start.HasValue && request.End.HasValue
            ? await _repository.GetByPeriodAsync(request.Start.Value, request.End.Value, cancellationToken)
            : await _repository.GetAllAsync(cancellationToken);

        return purchases
            .Select(p => new PurchaseSummaryDto(p.Id, p.PurchaseDate, p.StoreName, p.TotalValue, p.Notes))
            .ToList();
    }
}
