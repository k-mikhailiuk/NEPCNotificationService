using System.Linq.Expressions;
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

    public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
    {
        return await _dbSet.AnyAsync(predicate, cancellationToken);
    }

    public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddAsync(entity, cancellationToken);
    }

    public void Attach(T entity)
    {
        _dbSet.Attach(entity);
    }
    
    public void Detach(T entity)
    {
        _dbSet.Entry(entity).State = EntityState.Detached;
    }

    public void DetachCollection(IEnumerable<T> entities)
    {
        foreach (var entity in entities)
        {
            Detach(entity);
        }
    }

    public void AttachCollection(IEnumerable<T> entities)
    {
        foreach (var entity in entities)
        {
            Attach(entity);
        }
    }

    
    public async Task<T?> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
    {
        return await _dbSet.FirstOrDefaultAsync(predicate, cancellationToken);
    }
    
    public async Task<List<T>> GetListAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
    {
        return await _dbSet
            .Where(predicate)
            .ToListAsync(cancellationToken);
    }

    public async Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken)
    {
        await _dbSet.AddRangeAsync(entities, cancellationToken);
    }
}