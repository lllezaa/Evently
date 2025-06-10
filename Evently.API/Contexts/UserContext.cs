using System.Security.Claims;
using Evently.Services.Exceptions;

namespace Evently.API.Contexts;

public class UserContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    /// <summary>
    /// Добавьте HttpContextAccessor в DI и будет счастье
    /// </summary>
    /// <param name="httpContextAccessor"></param>
    public UserContext(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    /// <summary>
    /// Возвращает UserId из токена, при отсуствии кидает Unauthorized
    /// </summary>
    /// <exception cref="UnauthorizedAccessException"></exception>
    //public int UserId =>
    //   int.Parse(
    //       _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? throw new UnauthorizedException("Where's your token mister"));
    private string? ClaimValue
        => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

    public int UserId
        => int.TryParse(ClaimValue, out var id)
            ? id
            : throw new UnauthorizedException("User ID claim missing or malformed");

}