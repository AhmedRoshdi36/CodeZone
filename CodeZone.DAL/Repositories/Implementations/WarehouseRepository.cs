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
    //public async Task<IEnumerable<Warehouse>> GetPaginatedAsync(int pageNumber, int pageSize)
    //{
    //    return await context.Warehouses
    //        .Skip((pageNumber - 1) * pageSize)
    //        .Take(pageSize)
    //        .ToListAsync();
    //}
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

    public async Task<Warehouse> AddAsync(Warehouse student)
    {
        await context.Warehouses.AddAsync(student);
        return student;
    }

    public async Task UpdateAsync(Warehouse warehouse)
    {
          context.Warehouses.Update(warehouse);
    }

    public async Task DeleteAsync(Warehouse student)
    {
        context.Warehouses.Remove(student);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await context.SaveChangesAsync();
    }
    //public async Task<int> GetCountAsync()
    //{
    //    return await context.Warehouses.CountAsync();
    //}
}

