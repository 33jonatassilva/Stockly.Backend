using MediatR;
using Stockly.Domain.Abstractions.Persistence;

namespace Stockly.Application.Brands.Queries;

public record GetAllBrandsQuery : IRequest<IReadOnlyList<BrandDto>>;

public class GetAllBrandsQueryHandler : IRequestHandler<GetAllBrandsQuery, IReadOnlyList<BrandDto>>
{
    private readonly IBrandRepository _repository;

    public GetAllBrandsQueryHandler(IBrandRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<BrandDto>> Handle(GetAllBrandsQuery request, CancellationToken cancellationToken)
    {
        var brands = await _repository.GetAllAsync(cancellationToken);

        return brands
            .Select(b => new BrandDto(b.Id, b.Name, b.CreatedAt))
            .ToList();
    }
}
