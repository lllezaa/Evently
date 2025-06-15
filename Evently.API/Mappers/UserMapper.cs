using Evently.API.DTOs.User;
using Evently.Core.Models;

namespace Evently.API.Mappers;

public static class UserMapper
{
    public static UserOutputDto ModelToOutputDto(User user) => new UserOutputDto
    {
        Id = user.Id,
        Email = user.Email,
        FirstName = user.FirstName,
        LastName = user.LastName,
        Role = user.Role,
        IsBlocked = user.IsBlocked
    };
}