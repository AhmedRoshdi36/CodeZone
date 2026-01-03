using CodeZone.DAL.Entities;

namespace CodeZone.DAL.Repositories.Interfaces;

public interface IProductRepository : IGenericRepository<Product>
{
    Task<IEnumerable<Product>> GetAllAsync();
    IQueryable<Product> Query();
    Task<int> GetProductsCountAsync(int warehouseId);
    Task<Product?> GetByIdAsync(int id);
    Task<Product?> GetBySkuAsync(string sku);
    Task<Product> AddAsync(Product product);
    Task UpdateAsync(Product product);
    Task DeleteAsync(Product product);
    Task<int> SaveChangesAsync();
}

