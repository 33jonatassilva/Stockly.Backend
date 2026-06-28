using MediatR;
using Stockly.Application.Common.Exceptions;
using Stockly.Domain.Abstractions.Persistence;

namespace Stockly.Application.Products.Commands;

public record UpdateProductCommand(
    Guid Id,
    string Name,
    string? Description,
    decimal Price,
    Guid CategoryId,
    Guid BrandId,
    bool IsActive) : IRequest<ProductDto>;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductDto>
{
    private readonly IProductRepository _repository;
    private readonly IBrandRepository _brandRepository;
    private readonly ICategoryRepository _categoryRepository;

    public UpdateProductCommandHandler(
        IProductRepository repository,
        IBrandRepository brandRepository,
        ICategoryRepository categoryRepository)
    {
        _repository = repository;
        _brandRepository = brandRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task<ProductDto> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        if (request.Price < 0)
        {
            throw new ValidationException("O preço não pode ser negativo.");
        }

        var product = await _repository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException("Produto", request.Id);

        _ = await _brandRepository.GetByIdAsync(request.BrandId, cancellationToken)
            ?? throw new NotFoundException("Marca", request.BrandId);

        _ = await _categoryRepository.GetByIdAsync(request.CategoryId, cancellationToken)
            ?? throw new NotFoundException("Categoria", request.CategoryId);

        product.Name = request.Name;
        product.Description = request.Description;
        product.Price = request.Price;
        product.CategoryId = request.CategoryId;
        product.BrandId = request.BrandId;
        product.IsActive = request.IsActive;

        await _repository.UpdateAsync(product, cancellationToken);

        return new ProductDto(
            product.Id,
            product.Name,
            product.Description,
            product.Price,
            product.CategoryId,
            product.BrandId,
            product.IsActive,
            product.CreatedAt);
    }
}
