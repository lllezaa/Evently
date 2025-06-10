using Evently.Core.Models;

namespace Evently.API.DTOs.User;

public class UserRoleChangeDto
{
    public required Role Role { get; set; }
}