using CodeZone.BLL.Results;
using CodeZone.BLL.Services.Interfaces;
using CodeZone.DAL.Entities;
using CodeZone.DAL.Repositories.Interfaces;

namespace CodeZone.BLL.Services.Implementations;

public class ProductService(IUnitOfWork unitOfWork) : IProductService
{
    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await unitOfWork.Products.GetAllAsync();
    }

    public async Task<PaginatedResult<Product>> GetAllPaginatedAsync(int pageNumber, int pageSize)
    {
        // Validate page parameters
        if (pageNumber < 1) pageNumber = 1;
        if (pageSize < 1) pageSize = 5;

        var totalCount = await unitOfWork.Products.GetCountAsync();
        var products = await unitOfWork.Products.GetPaginatedAsync(pageNumber, pageSize);


        return new PaginatedResult<Product>
        {
            Items = products,
            TotalCount = totalCount,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
    }
  
    public async Task<Product?> GetByIdAsync(int id)
    {
        return await unitOfWork.Products.GetByIdAsync(id);
    }

    public async Task<Result> CreateAsync(Product product)
    {
        // Check for unique SKU
        var existing = await unitOfWork.Products.GetBySkuAsync(product.SKU);
        if (existing != null)
            return Result.Failure($"A product with the SKU '{product.SKU}' already exists.");

        await unitOfWork.Products.AddAsync(product);
        await unitOfWork.SaveAsync();
        return Result.Success();
    }

    public async Task<Result> UpdateAsync(Product product)
    {
        var existing = await unitOfWork.Products.GetByIdAsync(product.Id);
         if (existing == null)
            return Result.Failure($"Product with ID {product.Id} not found.");

        // Check for unique SKU (excluding current product)
        var duplicate = await unitOfWork.Products.GetBySkuAsync(product.SKU);
        if (duplicate != null && duplicate.Id != product.Id)
              return Result.Failure($"A product with the SKU '{product.SKU}' already exists.");

        existing.Name = product.Name;
        existing.SKU = product.SKU;
        existing.Description = product.Description;
        await unitOfWork.Products.UpdateAsync(existing);
        await unitOfWork.SaveAsync();
        return Result.Success();
    }

    public async Task<Result> DeleteAsync(int id)
    {
        var product = await unitOfWork.Products.GetByIdAsync(id); 
        if(product == null)
            return Result.Failure($"Product with ID {id} not found.");
        await unitOfWork.Products.DeleteAsync(product);
        await unitOfWork.SaveAsync();
        return Result.Success();

    }
}

