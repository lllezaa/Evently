using Evently.Core.Models;

namespace Evently.API.DTOs.User;

public class UserOutputDto
{
    public int Id { get; set; }
    public required string Email { get; set; }
    public required string FirstName { get; set; }
    public string? LastName { get; set; }
    public required Role Role { get; set; }
}