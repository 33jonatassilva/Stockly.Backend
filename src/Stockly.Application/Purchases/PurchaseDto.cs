namespace Stockly.Application.Purchases;

public record PurchaseItemDto(
    Guid Id,
    Guid ProductVariationId,
    decimal Quantity,
    decimal UnitPrice,
    decimal TotalPrice);

public record PurchaseDto(
    Guid Id,
    DateTime PurchaseDate,
    string StoreName,
    decimal TotalValue,
    string? Notes,
    IReadOnlyList<PurchaseItemDto> Items);

public record PurchaseSummaryDto(
    Guid Id,
    DateTime PurchaseDate,
    string StoreName,
    decimal TotalValue,
    string? Notes);
