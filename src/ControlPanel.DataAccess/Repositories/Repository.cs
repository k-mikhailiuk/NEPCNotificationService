using System.Linq.Expressions;
using ControlPanel.DataAccess.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ControlPanel.DataAccess.Repositories;

/// <inheritdoc/>
public class Repository<T> : IRepository<T> where T : class
{
    /// <summary>
    /// Контекст базы данных.
    /// </summary>
    protected readonly ControlPanelDbContext _context;
    
    /// <summary>
    /// DbSet для работы с сущностью.
    /// </summary>
    protected readonly DbSet<T> _dbSet;

    /// <summary>
    /// Создает новый экземпляр репозитория для указанного контекста.
    /// </summary>
    /// <param name="context">Контекст базы данных.</param>
    protected Repository(ControlPanelDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    /// <inheritdoc/>
    public async Task<T?> GetByIdAsync(long id, CancellationToken cancellationToken)
    {
        return await _dbSet.FindAsync([id], cancellationToken: cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _dbSet.ToListAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
    {
        return await _dbSet.AnyAsync(predicate, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task AddAsync(T entity, CancellationToken cancellationToken)
    {
        await _dbSet.AddAsync(entity, cancellationToken);
    }

    /// <inheritdoc/>
    public void Remove(T entity)
    {
        _dbSet.Remove(entity);
    }

    /// <inheritdoc/>
    public async Task<T?> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
    {
        return await _dbSet.FirstOrDefaultAsync(predicate, cancellationToken);
    }
}