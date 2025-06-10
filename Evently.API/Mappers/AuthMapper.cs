using Evently.API.DTOs.Auth;
using Evently.Core.Models;

namespace Evently.API.Mappers;

public static class AuthMapper
{
    public static User RegistrationDtoToModel(UserRegistrationDto dto) => new User
    {
        Email = dto.Email,
        PasswordHash = dto.Password,
        FirstName = dto.FirstName,
        LastName = dto.LastName,
        Role = Role.User
    };
}