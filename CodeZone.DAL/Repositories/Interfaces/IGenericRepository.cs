
namespace CodeZone.DAL.Repositories.Interfaces;

public interface IGenericRepository<TEntity> where TEntity : class
{
    Task<IEnumerable<TEntity>> GetPaginatedAsync(int pageNumber, int pageSize);
    Task<int> GetCountAsync();

}