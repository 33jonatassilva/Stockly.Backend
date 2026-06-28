using MediatR;
using Stockly.Application.Common.Exceptions;
using Stockly.Domain.Abstractions.Persistence;

namespace Stockly.Application.Brands.Queries;

public record GetBrandByIdQuery(Guid Id) : IRequest<BrandDto>;

public class GetBrandByIdQueryHandler : IRequestHandler<GetBrandByIdQuery, BrandDto>
{
    private readonly IBrandRepository _repository;

    public GetBrandByIdQueryHandler(IBrandRepository repository)
    {
        _repository = repository;
    }

    public async Task<BrandDto> Handle(GetBrandByIdQuery request, CancellationToken cancellationToken)
    {
        var brand = await _repository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException("Marca", request.Id);

        return new BrandDto(brand.Id, brand.Name, brand.CreatedAt);
    }
}
