using CodeZone.DAL.Entities;
using CodeZone.DAL.Persistence;
using CodeZone.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CodeZone.DAL.Repositories.Implementations;

public class ProductRepository(AppDbContext context) : GenericRepository<Product>(context),IProductRepository
{
    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await context.Products.ToListAsync();
    }
    
    public IQueryable<Product> Query()
    {
        return context.Products.AsQueryable();
    }
    

    public async Task<Product?> GetByIdAsync(int id)
    {
        return await context.Products.FindAsync(id);
    }
    
    public async Task<Product?> GetBySkuAsync(string sku)
    {
        return await context.Products
            .FirstOrDefaultAsync(p => p.SKU == sku);
    }
    public async Task<int> GetProductsCountAsync( int warehouseId)
    {
       ;
        return await context.StockTransactions
        .Where(st => st.WarehouseId == warehouseId && st.Product !=null)
        .Select(st => st.ProductId)
        .Distinct()
        .CountAsync();
    }

    public async Task<Product> AddAsync(Product product)
    {
        await context.Products.AddAsync(product);
        return product;
    }
    
    public async Task UpdateAsync(Product product)
    {
        context.Products.Update(product);
    }
    
    public async Task DeleteAsync(Product product)
    {
        context.Products.Remove(product);
    }
    
    public async Task<int> SaveChangesAsync()
    {
        return await context.SaveChangesAsync();
    }
}

