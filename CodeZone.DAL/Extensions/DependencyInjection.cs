using CodeZone.DAL.Persistence;
using CodeZone.DAL.Repositories.Implementations;
using CodeZone.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


namespace CodeZone.DAL.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddDataAccessLayer(this IServiceCollection services)
    {


        // Configure Entity Framework Core with In-Memory Database
        services.AddDbContext<AppDbContext>(options =>
            options.UseInMemoryDatabase(databaseName: "CodeZoneDb"));

        // Register Repositories
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IWarehouseRepository, WarehouseRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IStockTransactionRepository, StockTransactionRepository>();

        return services;
    }
}
