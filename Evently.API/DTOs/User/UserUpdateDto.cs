namespace Evently.API.DTOs.User;

public class UserUpdateDto
{
    public required string Email { get; set; }
    public required string FirstName { get; set; }
    public string? LastName { get; set; }
}