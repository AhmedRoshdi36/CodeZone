using CodeZone.DAL.Entities;

namespace CodeZone.DAL.Repositories.Interfaces;
public interface IWarehouseRepository
{
    Task<IEnumerable<Warehouse>> GetAllAsync();
    IQueryable<Warehouse> Query();
    Task<Warehouse?> GetByIdAsync(int id);
    Task<Warehouse?> GetByNameAsync(string name);
    Task<Warehouse> AddAsync(Warehouse warehouse);
    Task UpdateAsync(Warehouse warehouse);
    Task DeleteAsync(Warehouse warehouse);
    Task<int> SaveChangesAsync();
}

