using MediatR;
using Stockly.Application.Common.Exceptions;
using Stockly.Domain.Abstractions.Persistence;

namespace Stockly.Application.Products.Queries;

public record GetProductVariationsQuery(Guid ProductId) : IRequest<IReadOnlyList<ProductVariationDto>>;

public class GetProductVariationsQueryHandler : IRequestHandler<GetProductVariationsQuery, IReadOnlyList<ProductVariationDto>>
{
    private readonly IProductVariationRepository _repository;
    private readonly IProductRepository _productRepository;

    public GetProductVariationsQueryHandler(
        IProductVariationRepository repository,
        IProductRepository productRepository)
    {
        _repository = repository;
        _productRepository = productRepository;
    }

    public async Task<IReadOnlyList<ProductVariationDto>> Handle(GetProductVariationsQuery request, CancellationToken cancellationToken)
    {
        _ = await _productRepository.GetByIdAsync(request.ProductId, cancellationToken)
            ?? throw new NotFoundException("Produto", request.ProductId);

        var variations = await _repository.GetByProductAsync(request.ProductId, cancellationToken);

        return variations
            .Select(v => new ProductVariationDto(v.Id, v.ProductId, v.PackageDescription, v.QuantityPerPackage, v.Unit))
            .ToList();
    }
}
