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

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] UserLoginDto dto)
    {
        var token = await _authorizationService.LogInUser(dto.Email, dto.Password);
        return Ok(new { token = token });
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRegistrationDto dto)
    {
        var model = AuthMapper.RegistrationDtoToModel(dto);
        var token = await _authorizationService.RegisterUser(model);
        return Ok(new { token = token });
    }

    [Authorize]
    [HttpGet("me")]
    public async Task<IActionResult> GetMe()
    {
        var userId = _userContext.UserId;
        var model = await _userService.GetUserAsync(userId);
        var dto = UserMapper.ModelToOutputDto(model);
        return Ok(dto);
    }
}