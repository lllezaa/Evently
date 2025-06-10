using System.Collections.Generic;
using System.Threading.Tasks;
using Evently.Core.Models;

namespace Evently.Core.Services;

public interface IUserService
{
    Task AddAsync(User user);
    Task UpdateAsync(User user);
    Task DeleteAsync(int userId);
    Task<IEnumerable<User>> GetUsersAsync();
    Task<User> GetUserAsync(int userId);
    Task<User> GetUserByEmailAsync(string email);
}