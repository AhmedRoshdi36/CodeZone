using CodeZone.DAL.Entities;

namespace CodeZone.DAL.Repositories.Interfaces;

public interface IStockTransactionRepository 
{
    Task<IEnumerable<StockTransaction>> GetAllAsync();
    Task<IEnumerable<StockTransaction>> GetPaginatedAsync(int pageNumber, int pageSize);
    Task<int> GetCountAsync();
    IQueryable<StockTransaction> Query();
    Task<StockTransaction?> GetByIdAsync(int id);
    Task<IEnumerable<StockTransaction>> GetByWarehouseAndProductAsync(int warehouseId, int productId);
    Task<int> GetCurrentStockAsync(int warehouseId, int productId);
    Task<StockTransaction> AddAsync(StockTransaction stockTransaction);
    Task UpdateAsync(StockTransaction stockTransaction);
    Task DeleteAsync(StockTransaction stockTransaction);
    Task<int> SaveChangesAsync();
}

