using System.Linq.Expressions;
using Aggregator.DataAccess.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Aggregator.DataAccess.Repositories;

/// <summary>
/// Базовый репозиторий для выполнения операций CRUD над сущностями.
/// </summary>
/// <typeparam name="T">Тип сущности.</typeparam>
public class Repository<T> : IRepository<T> where T : class
{
    /// <summary>
    /// Контекст базы данных Aggregator.
    /// </summary>
    protected readonly AggregatorDbContext _context;

    /// <summary>
    /// Набор данных (DbSet) для сущности.
    /// </summary>
    protected readonly DbSet<T> _dbSet;

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="Repository{T}"/>.
    /// </summary>
    /// <param name="context">Контекст базы данных, используемый для доступа к данным.</param>
    protected Repository(AggregatorDbContext context)
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
    public async Task<List<T>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _dbSet.ToListAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
    {
        return await _dbSet.Where(predicate).ToListAsync(cancellationToken);
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

    /// <inheritdoc/>
    public async Task<List<T>> GetListByIdsRawSqlAsync(List<long> ids, CancellationToken cancellationToken)
    {
        if (ids.Count == 0)
            return [];

        var inClause = string.Join(",", ids);

        var entityType = _context.Model.FindEntityType(typeof(T));
        if (entityType == null)
            throw new InvalidOperationException($"Не удалось найти метаданные для типа {typeof(T).Name}.");

        var tableName = entityType.GetTableName();
        var schema = entityType.GetSchema();
        var fullTableName = string.IsNullOrEmpty(schema)
            ? $"[{tableName}]"
            : $"[{schema}].[{tableName}]";

        var keyProperty = entityType.FindPrimaryKey()?.Properties.FirstOrDefault();
        if (keyProperty == null)
            throw new InvalidOperationException($"Тип {typeof(T).Name} не имеет первичного ключа.");

        var keyColumnName = keyProperty.GetColumnName();

        var sql = $"SELECT * FROM {fullTableName} WHERE [{keyColumnName}] IN ({inClause})";

        return await _context.Set<T>().FromSqlRaw(sql).ToListAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<List<T>> GetListByIdsRawSqlAsync(
        List<long> ids,
        CancellationToken cancellationToken,
        params Expression<Func<T, object>>[] includes)
    {
        if (ids.Count == 0)
            return [];

        var inClause = string.Join(",", ids);

        var entityType = _context.Model.FindEntityType(typeof(T));
        if (entityType == null)
            throw new InvalidOperationException($"Не удалось найти метаданные для типа {typeof(T).Name}.");

        var tableName = entityType.GetTableName();
        var schema = entityType.GetSchema();
        var fullTableName = string.IsNullOrEmpty(schema)
            ? $"[{tableName}]"
            : $"[{schema}].[{tableName}]";

        var keyProperty = entityType.FindPrimaryKey()?.Properties.FirstOrDefault();
        if (keyProperty == null)
            throw new InvalidOperationException($"Тип {typeof(T).Name} не имеет первичного ключа.");

        var keyColumnName = keyProperty.GetColumnName();

        var sql = $"SELECT * FROM {fullTableName} WHERE [{keyColumnName}] IN ({inClause})";

        var query = _context.Set<T>().FromSqlRaw(sql);

        query = includes.Aggregate(query, (current, include) => current.Include(include));

        return await query.ToListAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken)
    {
        foreach (var entity in entities)
        {
            await AddAsync(entity, cancellationToken);
        }
    }

    /// <inheritdoc/>
    public void RemoveRange(IEnumerable<T> entities)
    {
        foreach (var entity in entities)
        {
            Remove(entity);
        }
    }
}