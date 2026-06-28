namespace Stockly.Application.Inventories;

public record InventoryDto(
    Guid Id,
    Guid ProductVariationId,
    decimal CurrentQuantity,
    decimal MinimumQuantity,
    decimal AcceptableQuantity,
    decimal IdealQuantity,
    DateTime UpdatedAt);
