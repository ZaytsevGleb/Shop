namespace Shop.Services.Catalog.Shared.Exceptions;

public class VariableNullException : Exception
{
    public VariableNullException()
    {
    }

    public VariableNullException(string message) : base(message)
    {
    }

    public VariableNullException(string message, Exception inner) : base(message, inner)
    {
    }
}