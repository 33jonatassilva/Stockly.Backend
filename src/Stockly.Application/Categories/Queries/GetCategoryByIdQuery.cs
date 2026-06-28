using MediatR;
using Stockly.Application.Common.Exceptions;
using Stockly.Domain.Abstractions.Persistence;

namespace Stockly.Application.Categories.Queries;

public record GetCategoryByIdQuery(Guid Id) : IRequest<CategoryDto>;

public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, CategoryDto>
{
    private readonly ICategoryRepository _repository;

    public GetCategoryByIdQueryHandler(ICategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<CategoryDto> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var category = await _repository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException("Categoria", request.Id);

        return new CategoryDto(category.Id, category.Name, category.Description, category.CreatedAt);
    }
}
