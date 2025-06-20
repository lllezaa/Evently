using Evently.API.Contexts;
using Evently.API.DTOs.User;
using Evently.API.Mappers;
using Evently.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Evently.API.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly UserContext _userContext;

    public UsersController(IUserService userService, UserContext userContext)
    {
        _userService = userService;
        _userContext = userContext;
    }

    /// <summary>
    /// Получить список пользователей
    /// </summary>
    /// <returns></returns>
    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> GetUsers([FromQuery] int offset = 0, [FromQuery] int limit = 10)
    {
        var users = await _userService.GetUsersAsync(offset, limit);
        var result = users.Select(UserMapper.ModelToOutputDto);
        return Ok(result);
    }

    /// <summary>
    /// Сменить роль пользователю (0 - юзер, 1 - админ)
    /// </summary>
    /// <param name="id"></param>
    /// <param name="dto"></param>
    /// <returns></returns>
    [Authorize(Roles = "Admin")]
    [HttpPatch("{id:int}/role")]
    public async Task<IActionResult> ChangeRole(int id, [FromBody] UserRoleChangeDto dto)
    {
        var user = await _userService.GetUserAsync(id);
        user.Role = dto.Role;
        await _userService.UpdateAsync(user);
        return Ok();
    }

    [Authorize(Roles = "Admin")]
    [HttpPatch("{id:int}/block")]
    public async Task<IActionResult> ToggleUserBlock(int id)
    {
        var user = await _userService.GetUserAsync(id);
        user.IsBlocked = !user.IsBlocked;
        await _userService.UpdateAsync(user);
        return Ok(new { blocked = user.IsBlocked });
    }

    /// <summary>
    /// Обновить информацию об авторизованном аккаунте.
    /// </summary>
    /// <param name="dto">Объект DTO, содержащий обновленные данные пользователя.</param>
    /// <returns>Результат выполнения операции.</returns>
    [Authorize]
    [HttpPut]
    public async Task<IActionResult> UpdateAccount([FromBody] UserUpdateDto dto)
    {
        var userId = _userContext.UserId;
        var user = await _userService.GetUserAsync(userId);
        user.Email = dto.Email;
        user.FirstName = dto.FirstName;
        user.LastName = dto.LastName;
        await _userService.UpdateAsync(user);
        return Ok();
    }


    /// <summary>
    /// Удаление пользователя по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор пользователя, подлежащего удалению.</param>
    /// <returns>Результат выполнения операции.</returns>
    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        await _userService.DeleteAsync(id);
        return Ok();
    }
}