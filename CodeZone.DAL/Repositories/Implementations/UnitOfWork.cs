using CodeZone.DAL.Persistence;
using CodeZone.DAL.Repositories.Interfaces;

namespace CodeZone.DAL.Repositories.Implementations;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    public IWarehouseRepository Warehouses { get; }
    public IProductRepository Products { get; }
    public IStockTransactionRepository StockTransactions { get; }

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
        Warehouses = new WarehouseRepository(_context);
        Products = new ProductRepository(_context);
        StockTransactions = new StockTransactionRepository(_context);
    }
    
    public async Task<int> SaveAsync() => await _context.SaveChangesAsync();

    public void Dispose() => _context.Dispose();
}
