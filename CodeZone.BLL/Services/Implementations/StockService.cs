using CodeZone.BLL.Results;
using CodeZone.BLL.Services.Interfaces;
using CodeZone.DAL.Entities;
using CodeZone.DAL.Repositories.Interfaces;

namespace CodeZone.BLL.Services.Implementations;

public class StockService(IUnitOfWork unitOfWork) : IStockService
{
    public async Task<IEnumerable<StockTransaction>> GetAllAsync()
    {
        return await unitOfWork.StockTransactions.GetAllAsync();
    }
    public async Task<PaginatedResult<StockTransaction>> GetAllPaginatedAsync(int pageNumber, int pageSize)
    {
        // Validate page parameters
        if (pageNumber < 1) pageNumber = 1;
        if (pageSize < 1) pageSize = 5;

        var totalCount = await unitOfWork.StockTransactions.GetCountAsync();
        var stockTransactions = await unitOfWork.StockTransactions.GetPaginatedAsync(pageNumber, pageSize);

        return new PaginatedResult<StockTransaction>
        {
            Items = stockTransactions,
            TotalCount = totalCount,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
    }

    public async Task<StockTransaction?> GetByIdAsync(int id)
    {
        return await unitOfWork.StockTransactions.GetByIdAsync(id);
    }

    public async Task<int> GetCurrentStockAsync(int warehouseId, int productId)
    {
        if (warehouseId <= 0 || productId <= 0)
            return 0;
        return await unitOfWork.StockTransactions.GetCurrentStockAsync(warehouseId, productId);
    }


    public async Task<Result> CreateAsync(StockTransaction stock)
    {
        var warehouse = await unitOfWork.Warehouses.GetByIdAsync(stock.WarehouseId);
        if (warehouse == null)
            return Result.Failure($"Warehouse with ID {stock.WarehouseId} not found.");

        var product = await unitOfWork.Products.GetByIdAsync(stock.ProductId);
        if (product == null)
            return Result.Failure($"Product with ID {stock.ProductId} not found.");

        if (stock.Quantity == 0)
            return Result.Failure("Quantity cannot be zero.");

        var currentStock = await GetCurrentStockAsync(stock.WarehouseId, stock.ProductId);
        var newStock = currentStock + stock.Quantity;

        if (newStock < 0)
            return Result.Failure(
                $"Cannot remove {Math.Abs(stock.Quantity)} items. Current stock is {currentStock}."
            );


        await unitOfWork.StockTransactions.AddAsync(stock);
        await unitOfWork.SaveAsync();

        return Result.Success();
    }
    public async Task<Result> UpdateAsync(StockTransaction stockTransaction)
    {
        var existing = await unitOfWork.StockTransactions.GetByIdAsync(stockTransaction.Id);
        if (existing == null)
            return Result.Failure($"Stock transaction with ID {stockTransaction.Id} not found.");

        if (existing.WarehouseId != stockTransaction.WarehouseId ||
            existing.ProductId != stockTransaction.ProductId)
        {
            var oldStock = await GetCurrentStockAsync(existing.WarehouseId, existing.ProductId);
            if (oldStock - existing.Quantity < 0)
                return Result.Failure("Cannot move transaction. Old stock would go below zero.");

            var newStock = await GetCurrentStockAsync(stockTransaction.WarehouseId, stockTransaction.ProductId);
            if (newStock + stockTransaction.Quantity < 0)
                return Result.Failure("Cannot move transaction. New stock would go below zero.");
        }
        else
        {
            var currentStock = await GetCurrentStockAsync(existing.WarehouseId, existing.ProductId);
            var stockWithoutThis = currentStock - existing.Quantity;
            var newStock = stockWithoutThis + stockTransaction.Quantity;

            if (newStock < 0)
                return Result.Failure("Cannot update transaction. Stock would go below zero.");
        }

        existing.WarehouseId = stockTransaction.WarehouseId;
        existing.ProductId = stockTransaction.ProductId;
        existing.Quantity = stockTransaction.Quantity;

        await unitOfWork.StockTransactions.UpdateAsync(existing);
        await unitOfWork.SaveAsync();

        return Result.Success();
    }
    public async Task<Result> DeleteAsync(int id)
    {
        var transaction = await unitOfWork.StockTransactions.GetByIdAsync(id);
        if (transaction == null)
            return Result.Failure($"Stock transaction with ID {id} not found.");

        var currentStock = await GetCurrentStockAsync(transaction.WarehouseId, transaction.ProductId);
        if (currentStock - transaction.Quantity < 0)
            return Result.Failure("Cannot delete transaction. Stock would go below zero.");

        await unitOfWork.StockTransactions.DeleteAsync(transaction);
        await unitOfWork.SaveAsync();

        return Result.Success();
    }

   
}

