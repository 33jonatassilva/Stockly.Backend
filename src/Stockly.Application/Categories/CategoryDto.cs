namespace Stockly.Application.Categories;

public record CategoryDto(Guid Id, string Name, string? Description, DateTime CreatedAt);
