namespace Evently.Services.Exceptions;

public class ConflictException : ServiceException
{
    public override int StatusCode => 409;

    public ConflictException(string message) : base(message)
    {
        
    }
}