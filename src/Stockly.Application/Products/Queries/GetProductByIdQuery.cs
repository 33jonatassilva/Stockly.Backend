using MediatR;
using Stockly.Application.Common.Exceptions;
using Stockly.Domain.Abstractions.Persistence;

namespace Stockly.Application.Products.Queries;

public record GetProductByIdQuery(Guid Id) : IRequest<ProductDetailsDto>;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDetailsDto>
{
    private readonly IProductRepository _repository;

    public GetProductByIdQueryHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<ProductDetailsDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _repository.GetWithVariationsAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException("Produto", request.Id);

        var variations = product.ProductVariations
            .Select(v => new ProductVariationDto(v.Id, v.ProductId, v.PackageDescription, v.QuantityPerPackage, v.Unit))
            .ToList();

        return new ProductDetailsDto(
            product.Id,
            product.Name,
            product.Description,
            product.Price,
            product.CategoryId,
            product.BrandId,
            product.IsActive,
            product.CreatedAt,
            variations);
    }
}
