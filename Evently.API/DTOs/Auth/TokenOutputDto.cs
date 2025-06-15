namespace Evently.API.DTOs.Auth;

public class TokenOutputDto
{
    public required string Token { get; set; }
    public DateTime Expires { get; set; }
}