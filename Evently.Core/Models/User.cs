using System.Collections.Generic;

namespace Evently.Core.Models;

public class User
{
    public int Id { get; set; }

    public required string FirstName { get; set; }
    public string? LastName { get; set; }

    public required string Email { get; set; }
    public required string PasswordHash { get; set; }
    
    public required Role Role { get; set; }
    
    public ICollection<Registration> Registrations { get; set; } = new List<Registration>();
}