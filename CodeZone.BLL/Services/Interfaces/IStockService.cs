using CodeZone.BLL.Results;
using CodeZone.DAL.Entities;

namespace CodeZone.BLL.Services.Interfaces;

public interface IStockService 
{
    Task<IEnumerable<StockTransaction>> GetAllAsync();
    Task<PaginatedResult<StockTransaction>> GetAllPaginatedAsync(int pageNumber, int pageSize);

    Task<StockTransaction?> GetByIdAsync(int id);
    Task<int> GetCurrentStockAsync(int warehouseId, int productId);
    Task<Result> CreateAsync(StockTransaction stockTransaction);
    Task<Result> UpdateAsync(StockTransaction stockTransaction);
    Task<Result> DeleteAsync(int id);
}

