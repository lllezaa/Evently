namespace Evently.API.DTOs.Event;

public class EventOutputDto
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required DateTime Date { get; set; }
}