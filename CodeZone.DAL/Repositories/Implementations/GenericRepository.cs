using CodeZone.DAL.Persistence;
using CodeZone.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;

namespace CodeZone.DAL.Repositories.Implementations;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
{
    private readonly AppDbContext _context;
    private readonly DbSet<TEntity> _dbSet;

    public GenericRepository(AppDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<TEntity>();
    }
    public async Task<IEnumerable<TEntity>> GetPaginatedAsync(int pageNumber, int pageSize)
    {
        if (pageNumber <= 0) pageNumber = 1;
        if (pageSize <= 0) pageSize = 5;
        return await _dbSet
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }
    public async Task<int> GetCountAsync()
    {
        return await _dbSet.CountAsync();
    }

}