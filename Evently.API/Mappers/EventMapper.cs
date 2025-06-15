using Evently.API.DTOs.Event;
using Evently.Core.Models;

namespace Evently.API.Mappers;

public static class EventMapper
{
    public static EventOutputDto ModelToOutputDto(Event eventModel) => new EventOutputDto
    {
        Id = eventModel.Id,
        Title = eventModel.Title,
        Description = eventModel.Description,
        Date = eventModel.Date,
    };

    public static Event CreateDtoToModel(EventCreateDto dto) => new Event
    {
        Title = dto.Title,
        Description = dto.Description,
        Date = dto.Date,
    };

    public static Event UpdateDtoToModel(int id, EventUpdateDto dto) => new Event
    {
        Id = id,
        Title = dto.Title,
        Description = dto.Description,
        Date = dto.Date,
    };

    public static EventListOutputDto ModelToOutputDto(IEnumerable<Event> events)
    {
        var enumerable = events.ToList();
        return new EventListOutputDto
        {
            Items = enumerable.Select(ModelToOutputDto).ToList(),
            Total = enumerable.Count
        };
    }
}