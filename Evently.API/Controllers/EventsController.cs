using Evently.API.Contexts;
using Evently.API.DTOs.Event;
using Evently.API.Mappers;
using Evently.Core.Models;
using Evently.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Evently.API.Controllers;

[ApiController]
[Route("api/events")]
public class EventsController : ControllerBase
{
    private readonly IEventService _eventService;
    private readonly UserContext _userContext;
    private readonly IRegistrationService _registrationService;

    public EventsController(IEventService eventService, UserContext userContext,
        IRegistrationService registrationService)
    {
        _eventService = eventService;
        _userContext = userContext;
        _registrationService = registrationService;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetEvents()
    {
        var events = await _eventService.GetEventsAsync();
        var result = events.Select(EventMapper.ModelToOutputDto);
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetEvent([FromRoute] int id)
    {
        var eventModel = await _eventService.GetEventAsync(id);
        var result = EventMapper.ModelToOutputDto(eventModel);
        return Ok(result);
    }

    [Authorize]
    [HttpPost("{id:int}/register")]
    public async Task<IActionResult> RegisterForEvent([FromRoute] int id)
    {
        var userId = _userContext.UserId;
        var registration = new Registration { UserId = userId, EventId = id };
        await _registrationService.AddAsync(registration);
        return Ok();
    }

    [Authorize]
    [HttpGet("my")]
    public async Task<IActionResult> GetMyEvents()
    {
        var userId = _userContext.UserId;
        var registrations = await _registrationService.GetRegistrationsByUserIdAsync(userId);
        var events = await Task.WhenAll(
            registrations.Select(r => _eventService.GetEventAsync(r.EventId)));
        var result = events.Select(EventMapper.ModelToOutputDto);
        return Ok(result);
    }
    
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> CreateEvent([FromBody] EventCreateDto dto)
    {
        var eventModel = EventMapper.CreateDtoToModel(dto);
        await _eventService.AddAsync(eventModel);
        return Ok();
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateEvent([FromRoute] int id, [FromBody] EventUpdateDto dto)
    {
        var eventModel = EventMapper.UpdateDtoToModel(id, dto);
        await _eventService.UpdateAsync(eventModel);
        return Ok();
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteEvent([FromRoute] int id)
    {
        await _eventService.DeleteAsync(id);
        return Ok();
    }
}