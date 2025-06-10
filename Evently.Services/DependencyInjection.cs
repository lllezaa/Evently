using Evently.Core.Helpers;
using Evently.Core.Services;
using Evently.Services.Helpers;
using Evently.Services.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Evently.Services;

public static class DependencyInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IPasswordHasher, Sha256PasswordHasher>();
        
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IEventService, EventService>();
        services.AddScoped<IRegistrationService, RegistrationService>();
        
        return services;
    }
}