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

    public async Task<T?> GetByIdAsync(long id, CancellationToken cancellationToken)
    {
        return await _dbSet.FindAsync([id], cancellationToken: cancellationToken);
    }

    public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _dbSet.ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
    {
        return await _dbSet.AnyAsync(predicate, cancellationToken);
    }

    public async Task AddAsync(T entity, CancellationToken cancellationToken)
    {
        await _dbSet.AddAsync(entity, cancellationToken);
    }

    public void Remove(T entity)
    {
        _dbSet.Remove(entity);
    }

    public async Task<T?> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
    {
        return await _dbSet.FirstOrDefaultAsync(predicate, cancellationToken);
    }

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

    public async Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken)
    {
        foreach (var entity in entities)
        {
            await AddAsync(entity, cancellationToken);
        }
    }

    public void RemoveRange(IEnumerable<T> entities)
    {
        foreach (var entity in entities)
        {
            Remove(entity);
        }
    }
}