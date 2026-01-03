using CodeZone.DAL.Entities;
using CodeZone.DAL.Persistence;
using CodeZone.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CodeZone.DAL.Repositories.Implementations;

public class StockTransactionRepository(AppDbContext context) : IStockTransactionRepository
{
    public async Task<IEnumerable<StockTransaction>> GetAllAsync()
    {
        return await context.StockTransactions
            .Include(st => st.Warehouse)
            .Include(st => st.Product)
            .ToListAsync();
    }
    public async Task<IEnumerable<StockTransaction>> GetPaginatedAsync(int pageNumber, int pageSize)
    {
        if (pageNumber <= 0) pageNumber = 1;
        if (pageSize <= 0) pageSize = 5;

        return await context.StockTransactions
            .Include(st => st.Warehouse)
            .Include(st => st.Product)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }
    public async Task<int> GetCountAsync()
    {
        return await context.StockTransactions.CountAsync();
    }

    public IQueryable<StockTransaction> Query()
    {
        return context.StockTransactions
            .Include(st => st.Warehouse)
            .Include(st => st.Product)
            .AsQueryable();
    }
    
    public async Task<StockTransaction?> GetByIdAsync(int id)
    {
        return await context.StockTransactions
            .Include(st => st.Warehouse)
            .Include(st => st.Product)
            .FirstOrDefaultAsync(st => st.Id == id);
    }
    
    public async Task<IEnumerable<StockTransaction>> GetByWarehouseAndProductAsync(int warehouseId, int productId)
    {
        return await context.StockTransactions
            .Where(st => st.WarehouseId == warehouseId && st.ProductId == productId)
            .ToListAsync();
    }
    
    public async Task<int> GetCurrentStockAsync(int warehouseId, int productId)
    { 
        return await context.StockTransactions
            .Where(st => st.WarehouseId == warehouseId && st.ProductId == productId)
            .SumAsync(st => st.Quantity);
    }
    
    public async Task<StockTransaction> AddAsync(StockTransaction stockTransaction)
    {
        await context.StockTransactions.AddAsync(stockTransaction);
        return stockTransaction;
    }
    
    public async Task UpdateAsync(StockTransaction stockTransaction)
    {
        context.StockTransactions.Update(stockTransaction);
    }
    
    public async Task DeleteAsync(StockTransaction stockTransaction)
    {
        context.StockTransactions.Remove(stockTransaction);
    }
    
    public async Task<int> SaveChangesAsync()
    {
        return await context.SaveChangesAsync();
    }
}

