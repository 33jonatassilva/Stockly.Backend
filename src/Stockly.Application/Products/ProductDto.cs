namespace Stockly.Application.Products;

public record ProductDto(
    Guid Id,
    string Name,
    string? Description,
    decimal Price,
    Guid CategoryId,
    Guid BrandId,
    bool IsActive,
    DateTime CreatedAt);

public record ProductVariationDto(
    Guid Id,
    Guid ProductId,
    string PackageDescription,
    decimal QuantityPerPackage,
    string Unit);

public record ProductDetailsDto(
    Guid Id,
    string Name,
    string? Description,
    decimal Price,
    Guid CategoryId,
    Guid BrandId,
    bool IsActive,
    DateTime CreatedAt,
    IReadOnlyList<ProductVariationDto> Variations);
