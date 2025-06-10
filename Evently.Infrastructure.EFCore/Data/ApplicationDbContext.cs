using Evently.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Evently.Infrastructure.EFCore.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<Registration> Registrations { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Registration>().HasKey(r => new {r.UserId, r.EventId});
    }

}