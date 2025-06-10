using Evently.Core.Repositories;
using Evently.Infrastructure.EFCore.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Evently.Infrastructure.EFCore;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IEventRepository, EventRepository>();
        services.AddScoped<IRegistrationRepository, RegistrationRepository>();
        
        return services;
    }
}