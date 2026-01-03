using CodeZone.BLL.Results;
using CodeZone.BLL.ViewModels;
using CodeZone.DAL.Entities;

namespace CodeZone.BLL.Services.Interfaces;

public interface IWarehouseService
{
    Task<IEnumerable<Warehouse>> GetAllAsync();
    Task<PaginatedResult<Warehouse>> GetAllPaginatedAsync(int pageNumber, int pageSize);
    Task<WarehouseDetailsViewModel> GetDetails(int id);
    Task<Warehouse?> GetByIdAsync(int id);
    Task<Result> CreateAsync(Warehouse warehouse);
    Task<Result> UpdateAsync(Warehouse warehouse);
    Task<Result> DeleteAsync(int id);
}

