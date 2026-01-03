using CodeZone.BLL.Services.Interfaces;
using CodeZone.BLL.Services.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace CodeZone.BLL.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddBusinessLogicLayer(this IServiceCollection services)
    {
        // Register BLL services
        services.AddScoped<IWarehouseService, WarehouseService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IStockService, StockService>();
        
        return services;
    }
}