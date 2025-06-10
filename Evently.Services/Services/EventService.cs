using Evently.Core.Models;
using Evently.Core.Repositories;
using Evently.Core.Services;

namespace Evently.Services.Services;

public class EventService : IEventService
{
    private readonly IEventRepository _eventRepository;

    public EventService(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }
    
    public async Task AddAsync(Event eventModel)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateAsync(Event eventModel)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(int eventId)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Event>> GetEventsAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<Event> GetEventAsync(int eventId)
    {
        throw new NotImplementedException();
    }
}