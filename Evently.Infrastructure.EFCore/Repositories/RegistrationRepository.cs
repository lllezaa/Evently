using Evently.Core.Models;
using Evently.Core.Repositories;
using Evently.Infrastructure.EFCore.Data;
using Microsoft.EntityFrameworkCore;

namespace Evently.Infrastructure.EFCore.Repositories;

public class RegistrationRepository : IRegistrationRepository
{
    private readonly ApplicationDbContext _context;

    public RegistrationRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Registration>> GetAllRegistrationsAsync()
    {
        return await _context.Registrations.ToListAsync();
    }

    public async Task<IEnumerable<Registration>> GetRegistrationsByUserIdAsync(int userId)
    {
        return await _context.Registrations.Where(x => x.UserId == userId).ToListAsync();
    }

    public async Task<IEnumerable<Registration>> GetRegistrationsByEventIdAsync(int eventId)
    {
        return await _context.Registrations.Where(x => x.EventId == eventId).ToListAsync();
    }

    public async Task<Registration?> GetRegistrationAsync(int userId, int eventId)
    {
        return await _context.Registrations.Where(x => x.UserId == userId && x.EventId == eventId)
            .FirstOrDefaultAsync();
    }

    public async Task AddAsync(Registration registration)
    {
        await _context.Registrations.AddAsync(registration);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Registration registration)
    {
        _context.Registrations.Update(registration);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int userId, int eventId)
    {
        await _context.Registrations.Where(x => x.UserId == userId && x.EventId == eventId).ExecuteDeleteAsync();
        await _context.SaveChangesAsync();
    }
}