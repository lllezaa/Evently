using System.Collections.Generic;
using System.Threading.Tasks;
using Evently.Core.Models;

namespace Evently.Core.Repositories;

public interface IRegistrationRepository
{
    Task<IEnumerable<Registration>> GetAllRegistrationsAsync();
    Task<IEnumerable<Registration>> GetRegistrationsByUserIdAsync(int userId);
    Task<IEnumerable<Registration>> GetRegistrationsByEventIdAsync(int eventId);
    Task<Registration?> GetRegistrationAsync(int userId, int eventId);
    Task AddAsync(Registration registration);
    Task UpdateAsync(Registration registration);
    Task DeleteAsync(int userId, int eventId);
}