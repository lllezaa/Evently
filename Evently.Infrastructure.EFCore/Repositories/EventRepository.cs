using Evently.Core.Models;
using Evently.Core.Repositories;
using Evently.Infrastructure.EFCore.Data;
using Microsoft.EntityFrameworkCore;

namespace Evently.Infrastructure.EFCore.Repositories;

public class EventRepository : IEventRepository
{
    private readonly ApplicationDbContext _context;

    public EventRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Event?> GetByIdAsync(int id)
    {
        return await _context.Events.FindAsync(id);
    }

    public async Task<Event?> GetByIdAsNoTrackingAsync(int id)
    {
        return await _context.Events.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<IEnumerable<Event>> GetAllAsync(int offset = 0, int limit = 10)
    {
        return await _context.Events.Skip(offset).Take(limit).ToListAsync();
    }

    public async Task AddAsync(Event entity)
    {
        await _context.Events.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Event entity)
    {
        _context.Events.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        await _context.Events.Where(e => e.Id == id).ExecuteDeleteAsync();
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Event>> GetEventsByQueryAsync(string query, int offset, int limit)
    {
        var queryLower = query.ToLower();
        return await _context.Events.Where(e =>
            EF.Functions.Like(e.Title.ToLower(), $"%{queryLower}%") ||
            EF.Functions.Like(e.Description.ToLower(), $"%{queryLower}%"))
            .Skip(offset)
            .Take(limit)
            .ToListAsync();
    }
}