namespace Stockly.Application.Common.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message)
    {
    }

    public NotFoundException(string entity, Guid id)
        : base($"{entity} com id '{id}' não foi encontrado.")
    {
    }
}
