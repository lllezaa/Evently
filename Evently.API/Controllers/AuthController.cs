using Evently.API.Contexts;
using Evently.API.DTOs.Auth;
using Evently.API.Mappers;
using Evently.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IAuthorizationService = Evently.Core.Services.IAuthorizationService;

namespace Evently.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly UserContext _userContext;
    private readonly IAuthorizationService _authorizationService;
    private readonly IUserService _userService;

    public AuthController(IAuthorizationService authorizationService, UserContext userContext, IUserService userService)
    {
        _authorizationService = authorizationService;
        _userContext = userContext;
        _userService = userService;
    }

    /// <summary>
    /// Авторизовать пользователя по почте и паролю
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] UserLoginDto dto)
    {
        var token = await _authorizationService.LogInUser(dto.Email, dto.Password);
        return Ok(new { token = token });
    }

    /// <summary>
    /// Регистрирует нового пользователя на основе предоставленных данных.
    /// </summary>
    /// <param name="dto">Объект, содержащий данные для регистрации пользователя, такие как Email, пароль и имя.</param>
    /// <returns>Возвращает результат действия с токеном доступа в случае успешной регистрации.</returns>
    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRegistrationDto dto)
    {
        var model = AuthMapper.RegistrationDtoToModel(dto);
        var token = await _authorizationService.RegisterUser(model);
        return Ok(new { token = token });
    }

    /// <summary>
    /// Получить информацию о текущем авторизованном пользователе
    /// </summary>
    /// <returns>Данные пользователя в формате DTO</returns>
    [Authorize]
    [HttpGet("me")]
    public async Task<IActionResult> GetMe()
    {
        var userId = _userContext.UserId;
        var model = await _userService.GetUserAsync(userId);
        var dto = UserMapper.ModelToOutputDto(model);
        return Ok(dto);
    }

    /// <summary>
    /// Изменяет пароль пользователя на основе предоставленных данных.
    /// </summary>
    /// <param name="dto">Объект, содержащий текущий пароль и новый пароль пользователя.</param>
    /// <returns>Возвращает результат действия в случае успешного изменения пароля.</returns>
    [Authorize]
    [HttpPost("changePassword")]
    public async Task<IActionResult> ChangePassword([FromBody] PasswordChangeDto dto)
    {
        var userId = _userContext.UserId;
        await _authorizationService.ChangePassword(userId, dto.OldPassword, dto.NewPassword);
        return Ok();
    }
}