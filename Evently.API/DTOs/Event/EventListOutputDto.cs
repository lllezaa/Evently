namespace Evently.API.DTOs.Event;

public class EventListOutputDto
{
    public required List<EventOutputDto> Items { get; set; }
    public int Total { get; set; }
}