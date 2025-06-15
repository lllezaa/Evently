using System.Collections.Generic;
using System.Threading.Tasks;
using Evently.Core.Models;

namespace Evently.Core.Services;

public interface IEventService
{
    Task AddAsync(Event eventModel);
    Task UpdateAsync(Event eventModel);
    Task DeleteAsync(int eventId);
    Task<IEnumerable<Event>> GetEventsAsync(int offset, int limit);
    Task<Event> GetEventAsync(int eventId);
    Task<IEnumerable<Event>> GetEventsByQueryAsync(string query, int offset, int limit);
}