namespace Evently.Services.Exceptions;

public class NotFoundException : ServiceException
{
    public override int StatusCode => 404;

    public NotFoundException(string message) : base(message)
    {
    }
}