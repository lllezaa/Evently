namespace Evently.Services.Exceptions;

public class UnauthorizedException : ServiceException
{
    public override int StatusCode => 401;

    public UnauthorizedException(string message) : base(message)
    {
        
    }
}