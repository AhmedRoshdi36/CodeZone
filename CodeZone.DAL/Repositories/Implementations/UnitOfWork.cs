using CodeZone.DAL.Persistence;
using CodeZone.DAL.Repositories.Interfaces;


namespace CodeZone.DAL.Repositories.Implementations;


public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    public IWarehouseRepository Warehouses { get; }


    public UnitOfWork(AppDbContext context)
    {
        _context = context;
        Warehouses = new WarehouseRepository(_context);

    }
    public async Task<int> SaveAsync() => await _context.SaveChangesAsync();

    public void Dispose() => _context.Dispose();
}
