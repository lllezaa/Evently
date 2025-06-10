namespace Evently.Services.Exceptions;

public abstract class ServiceException : Exception
{
    public abstract int StatusCode { get; }

    protected ServiceException(string message) : base(message)
    {
    }
}