namespace Evently.API.DTOs.Auth;

public class UserRegistrationDto
{
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string FirstName { get; set; }
    public string? LastName { get; set; }
}