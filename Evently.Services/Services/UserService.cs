using Evently.Core.Models;
using Evently.Core.Repositories;
using Evently.Core.Services;
using Evently.Services.Exceptions;

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
        await _userRepository.AddAsync(user);
    }

    public async Task UpdateAsync(User user)
    {
        await CheckUserByIdOrThrow(user.Id);
        await _userRepository.UpdateAsync(user);
    }

    public async Task DeleteAsync(int userId)
    {
        await CheckUserByIdOrThrow(userId);
        await _userRepository.DeleteAsync(userId);
    }

    public async Task<IEnumerable<User>> GetUsersAsync()
    {
        return await _userRepository.GetAllAsync();
    }

    public async Task<User> GetUserAsync(int userId)
    {
        return await GetUserByIdOrThrow(userId);
    }

    public async Task<User> GetUserByEmailAsync(string email)
    {
        var user = await _userRepository.FindByEmailAsync(email);
        if (user is null)
            throw new NotFoundException("User not found");
        return user;
    }

    private async Task<User> GetUserByIdOrThrow(int userId)
    {
        var user = await _userRepository.GetByIdAsNoTrackingAsync(userId);
        if (user is null)
        {
            throw new NotFoundException("User not found");
        }

        return user;
    }
    
    private async Task CheckUserByIdOrThrow(int userId)
    {
        var user = await GetUserByIdOrThrow(userId);
    }
}