using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using NotificationService.DataAccess.Abstractions;

namespace NotificationService.DataAccess;

/// <inheritdoc/>
public class Repository<T> : IRepository<T> where T : class
{
    /// <summary>
    /// Контекст базы данных для NotificationService.
    /// </summary>
    protected readonly NotificationServiceDbContext _context;
    
    /// <summary>
    /// Набор сущностей типа <typeparamref name="T"/>.
    /// </summary>
    protected readonly DbSet<T> _dbSet;

    /// <summary>
    /// Инициализирует новый экземпляр репозитория с указанным контекстом базы данных.
    /// </summary>
    /// <param name="context">Контекст базы данных NotificationService.</param>
    protected Repository(NotificationServiceDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    /// <inheritdoc/>
    public Task<List<T>> GetAllWithConditionAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default)
    {
       return _dbSet.Where(expression).ToListAsync(cancellationToken);
    }
}