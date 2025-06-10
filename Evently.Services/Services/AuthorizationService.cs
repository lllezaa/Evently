using Evently.Core.Helpers;
using Evently.Core.Models;
using Evently.Core.Services;
using Evently.Services.Exceptions;
using Evently.Services.Helpers;
using Evently.Services.Validators;

namespace Evently.Services.Services;

public partial class AuthorizationService : IAuthorizationService
{
    private readonly IUserService _userService;
    private readonly IPasswordHasher _passwordHasher;
    private readonly UserValidator _userValidator;

    public AuthorizationService(IUserService userService, IPasswordHasher passwordHasher, UserValidator userValidator)
    {
        _userService = userService;
        _passwordHasher = passwordHasher;
        _userValidator = userValidator;
    }

    public async Task<string> LogInUser(string email, string password)
    {
        var user = await _userService.GetUserByEmailAsync(email);
        if (!_passwordHasher.Verify(password, user.PasswordHash))
        {
            throw new UnauthorizedException("Password is incorrect");
        }

        var token = TokenHelper.GetAuthToken(user);
        return token;
    }

    public async Task<string> RegisterUser(User user)
    {
        if (!EmailRegex().IsMatch(user.Email))
        {
            throw new BadRequestException("Invalid email format");
        }
    
        var isEmailTaken = await _userValidator.IsEmailTaken(user.Email);
        if (isEmailTaken)
        {
            throw new ConflictException("Email is already taken");
        }

        user.PasswordHash = _passwordHasher.HashPassword(user.PasswordHash);

        await _userService.AddAsync(user);
        var token = TokenHelper.GetAuthToken(user);
        return token;
    }

    public async Task ChangePassword(int userId, string oldPassword, string newPassword)
    {
        var user = await _userService.GetUserAsync(userId);
        if (!_passwordHasher.Verify(oldPassword, user.PasswordHash))
        {
            throw new UnauthorizedException("Password is incorrect");
        }

        user.PasswordHash = _passwordHasher.HashPassword(newPassword);
        await _userService.UpdateAsync(user);
    }

    [System.Text.RegularExpressions.GeneratedRegex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")]
    private static partial System.Text.RegularExpressions.Regex EmailRegex();
}