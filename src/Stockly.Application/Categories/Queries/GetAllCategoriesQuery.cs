using MediatR;
using Stockly.Domain.Abstractions.Persistence;

namespace Stockly.Application.Categories.Queries;

public record GetAllCategoriesQuery : IRequest<IReadOnlyList<CategoryDto>>;

public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, IReadOnlyList<CategoryDto>>
{
    private readonly ICategoryRepository _repository;

    public GetAllCategoriesQueryHandler(ICategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<CategoryDto>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = await _repository.GetAllAsync(cancellationToken);

        return categories
            .Select(c => new CategoryDto(c.Id, c.Name, c.Description, c.CreatedAt))
            .ToList();
    }
}
