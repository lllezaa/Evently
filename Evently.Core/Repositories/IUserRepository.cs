using System.Threading.Tasks;
using Evently.Core.Models;

namespace Evently.Core.Repositories;

public interface IUserRepository : IGenericRepository<User>
{
    Task<User?> FindByEmailAsync(string email);
}