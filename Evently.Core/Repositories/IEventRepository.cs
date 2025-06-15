using System.Collections.Generic;
using System.Threading.Tasks;
using Evently.Core.Models;

namespace Evently.Core.Repositories;

public interface IEventRepository : IGenericRepository<Event>
{
    Task<IEnumerable<Event>> GetEventsByQueryAsync(string query, int offset, int limit);
    Task<IEnumerable<Event>> GetUpcomingEventsAsync(int offset, int limit);
    Task<IEnumerable<Event>> GetPastEventsAsync(int offset, int limit);
}