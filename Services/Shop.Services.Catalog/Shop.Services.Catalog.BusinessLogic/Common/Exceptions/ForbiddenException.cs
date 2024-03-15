namespace Shop.Services.Catalog.BusinessLogic.Common.Exceptions;

public class ForbiddenException : Exception
{
    public ForbiddenException(string name, object key)
        :base($"Access to Entity \"{name}\" ({key}) is forbidden.")
    {
    }

    public ForbiddenException(string name)
    :base($"Access to Entity  \"{name}\" is forbidden.")
    {
    }
}