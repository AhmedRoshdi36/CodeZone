using CodeZone.BLL.Results;
using CodeZone.DAL.Entities;

namespace CodeZone.BLL.Services.Interfaces;

public interface IProductService
{
    Task<IEnumerable<Product>> GetAllAsync();
    Task<PaginatedResult<Product>> GetAllPaginatedAsync(int pageNumber, int pageSize);
   
    Task<Product?> GetByIdAsync(int id);
    Task<Result> CreateAsync(Product product);
    Task<Result> UpdateAsync(Product product);
    Task<Result> DeleteAsync(int id);
}

