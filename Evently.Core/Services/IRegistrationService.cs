using System.Collections.Generic;
using System.Threading.Tasks;
using Evently.Core.Models;

namespace Evently.Core.Services;

public interface IRegistrationService
{
    Task AddAsync(Registration registration);
    Task DeleteAsync(int userId, int eventId);
    Task<IEnumerable<Registration>> GetRegistrationsByUserIdAsync(int userId);
    Task<IEnumerable<Registration>> GetRegistrationsByEventIdAsync(int eventId);
}