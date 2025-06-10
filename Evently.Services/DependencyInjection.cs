using Evently.Core.Helpers;
using Evently.Core.Services;
using Evently.Services.Helpers;
using Evently.Services.Services;
using Evently.Services.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace Evently.Services;

public static class DependencyInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IPasswordHasher, Sha256PasswordHasher>();

        services.AddScoped<UserValidator>();
        
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IEventService, EventService>();
        services.AddScoped<IRegistrationService, RegistrationService>();
        services.AddScoped<IAuthorizationService, AuthorizationService>();
        
        return services;
    }
}