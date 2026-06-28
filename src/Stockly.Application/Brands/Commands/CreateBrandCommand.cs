using MediatR;
using Stockly.Application.Common.Exceptions;
using Stockly.Domain.Abstractions.Persistence;
using Stockly.Domain.Entities;

namespace Stockly.Application.Brands.Commands;

public record CreateBrandCommand(string Name) : IRequest<BrandDto>;

public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, BrandDto>
{
    private readonly IBrandRepository _repository;

    public CreateBrandCommandHandler(IBrandRepository repository)
    {
        _repository = repository;
    }

    public async Task<BrandDto> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
    {
        if (await _repository.ExistsByNameAsync(request.Name, cancellationToken))
        {
            throw new ConflictException($"Já existe uma marca com o nome '{request.Name}'.");
        }

        var brand = new Brand
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            CreatedAt = DateTime.UtcNow
        };

        await _repository.AddAsync(brand, cancellationToken);

        return new BrandDto(brand.Id, brand.Name, brand.CreatedAt);
    }
}
