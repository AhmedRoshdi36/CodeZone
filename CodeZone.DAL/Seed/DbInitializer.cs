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
            new Warehouse { Name = "Secondary Warehouse" },


        };

        context.Warehouses.AddRange(warehouses);
        context.SaveChanges();

        // Seed Products
        var products = new List<Product>
        {
            new Product { Name = "Laptop", SKU = "LAP-001", Description = "High-performance laptop" },
            new Product { Name = "Mouse", SKU = "MOU-001", Description = "Wireless optical mouse" },
            new Product { Name = "Keyboard", SKU = "KEY-001", Description = "Mechanical keyboard" },
            new Product { Name = "Monitor", SKU = "MON-001", Description = "27-inch 4K monitor" },
           
        };

        context.Products.AddRange(products);
        context.SaveChanges();

        // Seed Stock Transactions
        var transactions = new List<StockTransaction>
        {
            new StockTransaction { WarehouseId = warehouses[0].Id, ProductId = products[0].Id, Quantity = 10},
            new StockTransaction { WarehouseId = warehouses[0].Id, ProductId = products[1].Id, Quantity = 25},
            new StockTransaction { WarehouseId = warehouses[1].Id, ProductId = products[0].Id, Quantity = 5 },
            new StockTransaction { WarehouseId = warehouses[0].Id, ProductId = products[0].Id, Quantity = -2  } // Remove 2 laptops,

        };

        context.StockTransactions.AddRange(transactions);

        // Save all changes
        context.SaveChanges();
    }
}

