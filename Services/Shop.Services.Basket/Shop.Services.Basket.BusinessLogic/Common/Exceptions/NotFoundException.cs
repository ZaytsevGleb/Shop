namespace Shop.Services.Catalog.BusinessLogic.Common.Exceptions;

public class NotFoundException: Exception
{
    public NotFoundException(string name, object key)
    :base($"Entity \"{name}\" by ({key}) was not found.")
    {
    }
}