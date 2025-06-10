using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Evently.Services;

public class AuthOptions
{
    public static string Issuer;
    public static string Audience;
    public static TimeSpan TokenLifetime;
    private static string? SecurityKey { get; set; }

    public static SymmetricSecurityKey SymmetricSecurityKey
    {
        get
        {
            ArgumentNullException.ThrowIfNull(SecurityKey);

            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecurityKey));
        }
    }

    public static void MakeOptions(IConfigurationManager configurationManager, string? securityKey = null)
    {
        var jwtSettings = configurationManager.GetSection("JwtSettings");
        Issuer = jwtSettings["Issuer"] ?? "DefaultIssuer";
        Audience = jwtSettings["Audience"] ?? "DefaultAudience";
        TokenLifetime = TimeSpan.FromMinutes(int.Parse(jwtSettings["TokenLifetime"] ?? "60"));
        SecurityKey = securityKey ?? jwtSettings["SecretKey"];
    }

}