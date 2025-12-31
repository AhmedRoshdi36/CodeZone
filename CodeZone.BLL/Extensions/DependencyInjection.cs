using Microsoft.Extensions.DependencyInjection;

namespace CodeZone.BLL.Extensions;


public static class DependencyInjection
{
    public static IServiceCollection AddBusinessLogicLayer(this IServiceCollection services)
    {
        // Register BLL services here in the future
        return services;
    }
}