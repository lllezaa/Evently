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

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _userService.GetUsersAsync();
        var result = users.Select(UserMapper.ModelToOutputDto);
        return Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> ChangeRole(int id, [FromBody] UserRoleChangeDto dto)
    {
        var user = await _userService.GetUserAsync(id);
        user.Role = dto.Role;
        await _userService.UpdateAsync(user);
        return Ok();
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        await _userService.DeleteAsync(id);
        return Ok();
    }
}