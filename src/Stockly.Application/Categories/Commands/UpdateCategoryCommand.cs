using MediatR;
using Stockly.Application.Common.Exceptions;
using Stockly.Domain.Abstractions.Persistence;

namespace Stockly.Application.Categories.Commands;

public record UpdateCategoryCommand(Guid Id, string Name, string? Description) : IRequest<CategoryDto>;

public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, CategoryDto>
{
    private readonly ICategoryRepository _repository;

    public UpdateCategoryCommandHandler(ICategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<CategoryDto> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _repository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException("Categoria", request.Id);

        var existing = await _repository.GetByNameAsync(request.Name, cancellationToken);
        if (existing is not null && existing.Id != category.Id)
        {
            throw new ConflictException($"Já existe uma categoria com o nome '{request.Name}'.");
        }

        category.Name = request.Name;
        category.Description = request.Description;
        await _repository.UpdateAsync(category, cancellationToken);

        return new CategoryDto(category.Id, category.Name, category.Description, category.CreatedAt);
    }
}
