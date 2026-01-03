using CodeZone.DAL.Entities;
using CodeZone.DAL.Persistence;
using CodeZone.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace CodeZone.DAL.Repositories.Implementations;


public class WarehouseRepository(AppDbContext context) : GenericRepository<Warehouse>(context),IWarehouseRepository
{
    public async Task<IEnumerable<Warehouse>> GetAllAsync()
    {
        return await context.Warehouses.ToListAsync();
    }
   
    public IQueryable<Warehouse> Query()
    {
        return context.Warehouses.AsQueryable();
    }
    public async Task<Warehouse?> GetByIdAsync(int id)
    {
        return await context.Warehouses.FindAsync(id);
    }

    public async Task<Warehouse?> GetByNameAsync(string name)
    {
        return await context.Warehouses
            .FirstOrDefaultAsync(s => s.Name == name);
    }

    public async Task<Warehouse> AddAsync(Warehouse warehouse )
    {
        await context.Warehouses.AddAsync(warehouse);
        return warehouse;
    }

    public async Task UpdateAsync(Warehouse warehouse)
    {
          context.Warehouses.Update(warehouse);
    }

    public async Task DeleteAsync(Warehouse warehouse)
    {
        context.Warehouses.Remove(warehouse);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await context.SaveChangesAsync();
    }
   
}

