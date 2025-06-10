using Evently.Core.Models;
using Evently.Core.Repositories;
using Evently.Core.Services;

namespace Evently.Services.Services;

public class RegistrationService : IRegistrationService
{
    private readonly IRegistrationRepository _registrationRepository;

    public RegistrationService(IRegistrationRepository registrationRepository)
    {
        _registrationRepository = registrationRepository;
    }
    
    public async Task AddAsync(Registration registration)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(int userId, int eventId)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Registration>> GetRegistrationsByUserIdAsync(int userId)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Registration>> GetRegistrationsByEventIdAsync(int eventId)
    {
        throw new NotImplementedException();
    }
}