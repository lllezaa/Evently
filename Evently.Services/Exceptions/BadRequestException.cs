namespace Evently.Services.Exceptions;

public class BadRequestException : ServiceException
{
    public override int StatusCode => 400;

    public BadRequestException(string message) : base(message)
    {
    }
}