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
    protected readonly ControlPanelDbContext Context;
    
    /// <summary>
    /// DbSet для работы с сущностью.
    /// </summary>
    protected readonly DbSet<T> DbSet;

    /// <summary>
    /// Создает новый экземпляр репозитория для указанного контекста.
    /// </summary>
    /// <param name="context">Контекст базы данных.</param>
    protected Repository(ControlPanelDbContext context)
    {
        Context = context;
        DbSet = Context.Set<T>();
    }

    /// <inheritdoc/>
    public async Task<T?> GetByIdAsync(long id, CancellationToken cancellationToken)
    {
        return await DbSet.FindAsync([id], cancellationToken: cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await DbSet.ToListAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
    {
        return await DbSet.AnyAsync(predicate, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task AddAsync(T entity, CancellationToken cancellationToken)
    {
        await DbSet.AddAsync(entity, cancellationToken);
    }

    /// <inheritdoc/>
    public void Remove(T entity)
    {
        DbSet.Remove(entity);
    }

    /// <inheritdoc/>
    public async Task<T?> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
    {
        return await DbSet.FirstOrDefaultAsync(predicate, cancellationToken);
    }
}