using MediatR;
using Stockly.Domain.Abstractions.Persistence;

namespace Stockly.Application.Products.Queries;

public record GetAllProductsQuery(bool OnlyActive = false) : IRequest<IReadOnlyList<ProductDto>>;

public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, IReadOnlyList<ProductDto>>
{
    private readonly IProductRepository _repository;

    public GetAllProductsQueryHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var products = request.OnlyActive
            ? await _repository.GetActiveAsync(cancellationToken)
            : await _repository.GetAllAsync(cancellationToken);

        return products
            .Select(p => new ProductDto(
                p.Id, p.Name, p.Description, p.Price, p.CategoryId, p.BrandId, p.IsActive, p.CreatedAt))
            .ToList();
    }
}
