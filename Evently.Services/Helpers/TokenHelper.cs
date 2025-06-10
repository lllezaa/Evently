using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Evently.Core.Models;
using Microsoft.IdentityModel.Tokens;

namespace Evently.Services.Helpers;

public class TokenHelper
{
    public static string GetAuthToken(User user)
    {
        var userId = user.Id.ToString();

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, userId),
            new(ClaimTypes.Role, user.Role.ToString())
        };

        var jwt = new JwtSecurityToken(
            claims: claims,
            issuer: AuthOptions.Issuer,
            audience: AuthOptions.Audience,
            expires: DateTime.UtcNow + AuthOptions.TokenLifetime,
            signingCredentials: new SigningCredentials(AuthOptions.SymmetricSecurityKey, SecurityAlgorithms.HmacSha256)
        );

        var token = new JwtSecurityTokenHandler().WriteToken(jwt);

        return token;
    }
}