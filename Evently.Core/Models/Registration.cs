namespace Evently.Core.Models;

public class Registration
{
    public int UserId { get; set; }
    public int EventId { get; set; }
    
    public User User { get; set; }
    public Event Event { get; set; }
}