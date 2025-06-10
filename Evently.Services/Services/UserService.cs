using Evently.Core.Models;
using Evently.Core.Repositories;
using Evently.Core.Services;

namespace Evently.Services.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task AddAsync(User user)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateAsync(User user)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(int userId)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<User>> GetUsersAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<User> GetUserAsync(int userId)
    {
        throw new NotImplementedException();
    }
}