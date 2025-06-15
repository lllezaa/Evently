using Evently.Core.Models;
using Evently.Core.Repositories;
using Evently.Core.Services;
using Evently.Services.Exceptions;

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
        await _eventRepository.AddAsync(eventModel);
    }

    public async Task UpdateAsync(Event eventModel)
    {
        await CheckEventByIdOrThrow(eventModel.Id);
        await _eventRepository.UpdateAsync(eventModel);
    }

    public async Task DeleteAsync(int eventId)
    {
        await CheckEventByIdOrThrow(eventId);
        await _eventRepository.DeleteAsync(eventId);
    }

    public async Task<IEnumerable<Event>> GetEventsAsync(int offset, int limit)
    {
        return await _eventRepository.GetAllAsync(offset, limit);
    }

    public async Task<Event> GetEventAsync(int eventId)
    {
        return await GetEventByIdOrThrow(eventId);
    }

    public async Task<IEnumerable<Event>> GetEventsByQueryAsync(string query, int offset, int limit)
    {
        return await _eventRepository.GetEventsByQueryAsync(query, offset, limit);
    }

    private async Task<Event> GetEventByIdOrThrow(int eventId)
    {
        var eventModel = await _eventRepository.GetByIdAsNoTrackingAsync(eventId);
        if (eventModel is null)
        {
            throw new NotFoundException("Event not found");
        }

        return eventModel;
    }

    private async Task CheckEventByIdOrThrow(int eventId)
    {
        var eventModel = await GetEventByIdOrThrow(eventId);
    }
}