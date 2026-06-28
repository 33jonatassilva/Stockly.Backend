using MediatR;
using Stockly.Application.Common.Exceptions;
using Stockly.Domain.Abstractions.Persistence;

namespace Stockly.Application.Brands.Commands;

public record UpdateBrandCommand(Guid Id, string Name) : IRequest<BrandDto>;

public class UpdateBrandCommandHandler : IRequestHandler<UpdateBrandCommand, BrandDto>
{
    private readonly IBrandRepository _repository;

    public UpdateBrandCommandHandler(IBrandRepository repository)
    {
        _repository = repository;
    }

    public async Task<BrandDto> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
    {
        var brand = await _repository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException("Marca", request.Id);

        var existing = await _repository.GetByNameAsync(request.Name, cancellationToken);
        if (existing is not null && existing.Id != brand.Id)
        {
            throw new ConflictException($"Já existe uma marca com o nome '{request.Name}'.");
        }

        brand.Name = request.Name;
        await _repository.UpdateAsync(brand, cancellationToken);

        return new BrandDto(brand.Id, brand.Name, brand.CreatedAt);
    }
}
