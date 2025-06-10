using Evently.Core.Helpers;
using Evently.Core.Models;
using Evently.Core.Services;
using Evently.Services.Exceptions;
using Evently.Services.Helpers;
using Evently.Services.Validators;

namespace Evently.Services.Services;

public class AuthorizationService
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
}