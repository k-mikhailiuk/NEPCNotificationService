using Aggregator.DataAccess;
using Aggregator.Repositories.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Aggregator.Repositories.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly AggregatorDbContext _context;
    protected readonly DbSet<T> _dbSet;

    protected Repository(AggregatorDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task<T?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        return await _dbSet.FindAsync([id], cancellationToken: cancellationToken);
    }

    public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet.ToListAsync(cancellationToken);
    }

    public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddAsync(entity, cancellationToken);
    }
}