using MediatR;
using Stockly.Application.Common.Exceptions;
using Stockly.Domain.Abstractions.Persistence;

namespace Stockly.Application.Brands.Commands;

public record DeleteBrandCommand(Guid Id) : IRequest;

public class DeleteBrandCommandHandler : IRequestHandler<DeleteBrandCommand>
{
    private readonly IBrandRepository _repository;

    public DeleteBrandCommandHandler(IBrandRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
    {
        var brand = await _repository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException("Marca", request.Id);

        await _repository.DeleteAsync(brand, cancellationToken);
    }
}
