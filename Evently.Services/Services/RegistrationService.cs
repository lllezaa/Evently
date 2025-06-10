using Evently.Core.Models;
using Evently.Core.Repositories;
using Evently.Core.Services;
using Evently.Services.Exceptions;

namespace Evently.Services.Services;

public class RegistrationService : IRegistrationService
{
    private readonly IRegistrationRepository _registrationRepository;
    private readonly IEventRepository _eventRepository;
    private readonly IUserRepository _userRepository;

    public RegistrationService(IRegistrationRepository registrationRepository, IEventRepository eventRepository,
        IUserRepository userRepository)
    {
        _registrationRepository = registrationRepository;
        _eventRepository = eventRepository;
        _userRepository = userRepository;
    }

    public async Task AddAsync(Registration registration)
    {
        await CheckIfUserAndEventExists(registration.UserId, registration.EventId);
        await _registrationRepository.AddAsync(registration);
    }

    public async Task DeleteAsync(int userId, int eventId)
    {
        await CheckIfUserAndEventExists(userId, eventId);
        await _registrationRepository.DeleteAsync(userId, eventId);
    }

    public async Task<IEnumerable<Registration>> GetRegistrationsByUserIdAsync(int userId)
    {
        await CheckIfUserExists(userId);
        return await _registrationRepository.GetRegistrationsByUserIdAsync(userId);
    }

    public async Task<IEnumerable<Registration>> GetRegistrationsByEventIdAsync(int eventId)
    {
        await CheckIfEventExists(eventId);
        return await _registrationRepository.GetRegistrationsByEventIdAsync(eventId);
    }

    private async Task CheckIfUserExists(int userId)
    {
        var user = await _userRepository.GetByIdAsNoTrackingAsync(userId);
        if (user is null)
        {
            throw new NotFoundException("User not found");
        }
    }

    private async Task CheckIfEventExists(int eventId)
    {
        var eventModel = await _eventRepository.GetByIdAsNoTrackingAsync(eventId);
        if (eventModel is null)
        {
            throw new NotFoundException("Event not found");
        }
    }

    private async Task CheckIfUserAndEventExists(int userId, int eventId)
    {
        await CheckIfEventExists(eventId);
        await CheckIfUserExists(userId);
    }
}