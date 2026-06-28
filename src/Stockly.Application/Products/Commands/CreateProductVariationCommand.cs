using MediatR;
using Stockly.Application.Common.Exceptions;
using Stockly.Domain.Abstractions.Persistence;
using Stockly.Domain.Entities;

namespace Stockly.Application.Products.Commands;

public record CreateProductVariationCommand(
    Guid ProductId,
    string PackageDescription,
    decimal QuantityPerPackage,
    string Unit) : IRequest<ProductVariationDto>;

public class CreateProductVariationCommandHandler : IRequestHandler<CreateProductVariationCommand, ProductVariationDto>
{
    private readonly IProductVariationRepository _repository;
    private readonly IProductRepository _productRepository;

    public CreateProductVariationCommandHandler(
        IProductVariationRepository repository,
        IProductRepository productRepository)
    {
        _repository = repository;
        _productRepository = productRepository;
    }

    public async Task<ProductVariationDto> Handle(CreateProductVariationCommand request, CancellationToken cancellationToken)
    {
        if (request.QuantityPerPackage <= 0)
        {
            throw new ValidationException("A quantidade por embalagem deve ser maior que zero.");
        }

        _ = await _productRepository.GetByIdAsync(request.ProductId, cancellationToken)
            ?? throw new NotFoundException("Produto", request.ProductId);

        var variation = new ProductVariation
        {
            Id = Guid.NewGuid(),
            ProductId = request.ProductId,
            PackageDescription = request.PackageDescription,
            QuantityPerPackage = request.QuantityPerPackage,
            Unit = request.Unit
        };

        await _repository.AddAsync(variation, cancellationToken);

        return new ProductVariationDto(
            variation.Id,
            variation.ProductId,
            variation.PackageDescription,
            variation.QuantityPerPackage,
            variation.Unit);
    }
}
