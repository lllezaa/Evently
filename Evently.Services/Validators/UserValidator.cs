using Evently.Core.Repositories;

namespace Evently.Services.Validators;

public class UserValidator
{
    private readonly IUserRepository _userRepository;

    public UserValidator(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<bool> IsEmailTaken(string email)
    {
        var user = await _userRepository.FindByEmailAsync(email);
        return user is not null;
    }
}