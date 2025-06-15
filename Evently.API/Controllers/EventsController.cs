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

    /// <summary>
    /// Получить список ивентов
    /// </summary>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetEvents([FromQuery] int offset = 0, [FromQuery] int limit = 10)
    {
        var events = await _eventService.GetEventsAsync(offset, limit);
        var result = events.Select(EventMapper.ModelToOutputDto);
        return Ok(result);
    }

    /// <summary>
    /// Получить информацию об ивенте по его идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор ивента.</param>
    /// <returns>Возвращает информацию об ивенте.</returns>
    [AllowAnonymous]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetEvent([FromRoute] int id)
    {
        var eventModel = await _eventService.GetEventAsync(id);
        var result = EventMapper.ModelToOutputDto(eventModel);
        return Ok(result);
    }

    /// <summary>
    /// Регистрирует пользователя на указанный ивент.
    /// </summary>
    /// <param name="id">Идентификатор ивента, на который регистрируется пользователь.</param>
    /// <returns>Результат действия в виде HTTP-ответа.</returns>
    [Authorize]
    [HttpPost("{id:int}/register")]
    public async Task<IActionResult> RegisterForEvent([FromRoute] int id)
    {
        var userId = _userContext.UserId;
        var registration = new Registration { UserId = userId, EventId = id };
        await _registrationService.AddAsync(registration);
        return Ok();
    }

    /// <summary>
    /// Получить ивенты на которые записан пользователь
    /// </summary>
    /// <returns>A list of events associated with the user's registrations.</returns>
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

    /// <summary>
    /// Создать новый ивент.
    /// </summary>
    /// <param name="dto">Данные для создания ивента.</param>
    /// <returns>Возвращает успешный результат при успешном создании ивента.</returns>
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> CreateEvent([FromBody] EventCreateDto dto)
    {
        var eventModel = EventMapper.CreateDtoToModel(dto);
        await _eventService.AddAsync(eventModel);
        return Ok();
    }

    /// <summary>
    /// Обновить данные существующего ивента.
    /// </summary>
    /// <param name="id">Идентификатор ивента, который необходимо обновить.</param>
    /// <param name="dto">Объект с данными для обновления ивента.</param>
    /// <returns>Возвращает подтверждение успешного обновления.</returns>
    [Authorize(Roles = "Admin")]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateEvent([FromRoute] int id, [FromBody] EventUpdateDto dto)
    {
        var eventModel = EventMapper.UpdateDtoToModel(id, dto);
        await _eventService.UpdateAsync(eventModel);
        return Ok();
    }

    /// <summary>
    /// Удаляет ивент по указанному идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор удаляемого ивента.</param>
    /// <returns>Результат выполнения операции в виде HTTP-ответа.</returns>
    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteEvent([FromRoute] int id)
    {
        await _eventService.DeleteAsync(id);
        return Ok();
    }
}