namespace Evently.API.DTOs.Event;

public class EventUpdateDto
{
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required string Address { get; set; }
    public required DateTime Date { get; set; }
}