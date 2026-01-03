using CodeZone.BLL.Results;
using CodeZone.BLL.Services.Interfaces;
using CodeZone.BLL.ViewModels;
using CodeZone.DAL.Entities;
using CodeZone.DAL.Repositories.Interfaces;

namespace CodeZone.BLL.Services.Implementations;

public class WarehouseService(IUnitOfWork unitOfWork) : IWarehouseService
{
    public async Task<IEnumerable<Warehouse>> GetAllAsync()
    {
        return await unitOfWork.Warehouses.GetAllAsync();
    }

    public async Task<Warehouse?> GetByIdAsync(int id)
    {
        return await unitOfWork.Warehouses.GetByIdAsync(id);
    }
    public async Task<WarehouseDetailsViewModel> GetDetails(int id)
    {
        var warehouse = await unitOfWork.Warehouses.GetByIdAsync(id);
        var count =  await unitOfWork.Products.GetProductsCountAsync(id); 
        return  new WarehouseDetailsViewModel
        {
            Id = warehouse!.Id,
            Name = warehouse.Name,
            ProductsCount = count
        };
    }
    public async Task<PaginatedResult<Warehouse>> GetAllPaginatedAsync(int pageNumber, int pageSize)
    {
        // Validate page parameters
        if (pageNumber < 1) pageNumber = 1;
        if (pageSize < 1) pageSize = 5;

        var totalCount = await unitOfWork.Warehouses.GetCountAsync();
        var warehouses = await unitOfWork.Warehouses.GetPaginatedAsync(pageNumber, pageSize);

        return new PaginatedResult<Warehouse>
        {
            Items = warehouses,
            TotalCount = totalCount,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
    }
    public async Task<Result> CreateAsync(Warehouse warehouse)
    {
        // Check for unique name
        var existing = await unitOfWork.Warehouses.GetByNameAsync(warehouse.Name);
        if (existing != null)
        {
            return Result.Failure($"A warehouse with the name '{warehouse.Name}' already exists.");
        }


        await unitOfWork.Warehouses.AddAsync(warehouse);
        await unitOfWork.SaveAsync();
        return Result.Success();
    }

    public async Task<Result> UpdateAsync(Warehouse warehouse)
    {
        var existing = await unitOfWork.Warehouses.GetByIdAsync(warehouse.Id);
        if (existing == null)
            return Result.Failure($"Warehouse with ID {warehouse.Id} not found.");

        // Check for unique name (excluding current warehouse)
        var duplicate = await unitOfWork.Warehouses.GetByNameAsync(warehouse.Name);
        if (duplicate != null && duplicate.Id != warehouse.Id)
        {
            return Result.Failure($"A warehouse with the name '{warehouse.Name}' already exists.");
        }

        existing.Name = warehouse.Name;
        await unitOfWork.Warehouses.UpdateAsync(existing);
        await unitOfWork.SaveAsync();
        return Result.Success();
    }

    public async Task<Result> DeleteAsync(int id)
    {
        var warehouse = await unitOfWork.Warehouses.GetByIdAsync(id);
        if (warehouse == null)
            return Result.Failure($"Warehouse with ID {id} not found.");
        await unitOfWork.Warehouses.DeleteAsync(warehouse);
        await unitOfWork.SaveAsync();
        return Result.Success();
    }
}

