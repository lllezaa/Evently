using System;
using System.Collections.Generic;

namespace Evently.Core.Models;

public class Event
{
    public int Id { get; set; }
    
    public required string Title { get; set; }
    public required string Description { get; set; }
    
    public required DateTime Date { get; set; }
    
    public ICollection<Registration> Registrations { get; set; } = new List<Registration>();
}