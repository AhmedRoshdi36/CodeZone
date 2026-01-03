using CodeZone.DAL.Entities;

namespace CodeZone.DAL.Repositories.Interfaces;
public interface IWarehouseRepository : IGenericRepository<Warehouse>
{
    Task<IEnumerable<Warehouse>> GetAllAsync();
    //Task<IEnumerable<Warehouse>> GetPaginatedAsync(int pageNumber, int pageSize);
    IQueryable<Warehouse> Query();
    Task<Warehouse?> GetByIdAsync(int id);
    Task<Warehouse?> GetByNameAsync(string name);
    Task<Warehouse> AddAsync(Warehouse warehouse);
    Task UpdateAsync(Warehouse warehouse);
    Task DeleteAsync(Warehouse warehouse);
    Task<int> GetCountAsync();

    Task<int> SaveChangesAsync();
}

