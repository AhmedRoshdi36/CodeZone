namespace CodeZone.DAL.Repositories.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IWarehouseRepository Warehouses { get; }
    IProductRepository Products { get; }
    IStockTransactionRepository StockTransactions { get; }
    
    Task<int> SaveAsync();
}
