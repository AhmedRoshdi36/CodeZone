using CodeZone.DAL.Entities;
using CodeZone.DAL.Persistence;

namespace CodeZone.DAL.Seed;

public static class DbInitializer
{
    public static void Seed(AppDbContext context)
    {
        // Ensure database is created
        context.Database.EnsureCreated();

        // Check if data already exists
        if (context.Warehouses.Any() || context.Products.Any())
        {
            return; // Database has been seeded
        }

        // Seed warehouses
        var warehouses = new List<Warehouse>
        { 
            new Warehouse { Name = "Main Warehouse" },
            new Warehouse { Name = "Secondary Warehouse" }


        };

        context.Warehouses.AddRange(warehouses);

        // Seed Products
        // seed stock transactions       

        // Save all changes
        context.SaveChanges();
    }
}

