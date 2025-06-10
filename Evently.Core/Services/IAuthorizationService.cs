using System.Threading.Tasks;
using Evently.Core.Models;

namespace Evently.Core.Services;

public interface IAuthorizationService
{
    Task<string> LogInUser(string email, string password);
    Task<string> RegisterUser(User user);
    Task ChangePassword(int userId, string oldPassword, string newPassword);
}