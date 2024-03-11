namespace Shop.Services.Catalog.BusinessLogic.Common.Exceptions;

public class BadRequestException : Exception
{
    public BadRequestException()
    {
    }

    public BadRequestException(string message)
        : base(message)
    {
    }

    public BadRequestException(string message, Exception exception)
        : base(message, exception)
    {
    }
}