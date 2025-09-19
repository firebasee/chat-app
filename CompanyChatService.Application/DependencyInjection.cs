using Mediator;
using Microsoft.Extensions.DependencyInjection;

namespace CompanyChatService.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediator(config => config.ServiceLifetime = ServiceLifetime.Scoped);

        return services;
    }
}
