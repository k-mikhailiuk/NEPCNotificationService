using Aggregator.DataAccess;
using Aggregator.Repositories.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Aggregator.Repositories.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly AggregatorDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public Repository(AggregatorDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task<T?> GetByIdAsync(long id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }
}