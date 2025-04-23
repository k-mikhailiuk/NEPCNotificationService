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
    protected readonly AggregatorDbContext Context;

    /// <summary>
    /// Набор данных (DbSet) для сущности.
    /// </summary>
    protected readonly DbSet<T> DbSet;

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="Repository{T}"/>.
    /// </summary>
    /// <param name="context">Контекст базы данных, используемый для доступа к данным.</param>
    protected Repository(AggregatorDbContext context)
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
    public async Task<List<T>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await DbSet.ToListAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
    {
        return await DbSet.Where(predicate).ToListAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
    {
        return await DbSet.AnyAsync(predicate, cancellationToken);
    }

    /// <inheritdoc/>
    public void Add(T entity)
    {
         DbSet.Add(entity);
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
    
    /// <inheritdoc/>
    public async Task<List<T>> GetListByIdsRawSqlAsync(List<long> ids, CancellationToken cancellationToken)
    {
        if (ids.Count == 0)
            return [];

        var inClause = string.Join(",", ids);

        var entityType = Context.Model.FindEntityType(typeof(T));
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

        return await Context.Set<T>().FromSqlRaw(sql).ToListAsync(cancellationToken);
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

        var entityType = Context.Model.FindEntityType(typeof(T));
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

        var query = Context.Set<T>().FromSqlRaw(sql);

        query = includes.Aggregate(query, (current, include) => current.Include(include));

        return await query.ToListAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public void AddRange(IEnumerable<T> entities)
    {
        DbSet.AddRange(entities);
    }

    /// <inheritdoc/>
    public void RemoveRange(IEnumerable<T> entities)
    {
       DbSet.RemoveRange(entities);
    }
    
    /// <inheritdoc/>
    public Task<List<T>> GetAllWithConditionAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default)
    {
        return DbSet.Where(expression).ToListAsync(cancellationToken);
    }
}