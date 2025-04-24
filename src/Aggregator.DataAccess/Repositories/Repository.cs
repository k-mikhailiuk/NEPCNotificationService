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
    public IQueryable<T> GetAll()
    {
        return DbSet;
    }

    /// <inheritdoc/>
    public IQueryable<T> GetAll(Expression<Func<T, bool>> predicate)
    {
        return DbSet.Where(predicate);
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
    public IQueryable<T> GetQueryByIds(IReadOnlyCollection<long> ids)
    {
        if (ids.Count == 0)
            return Enumerable.Empty<T>().AsQueryable();

        var entityType = Context.Model.FindEntityType(typeof(T))
                         ?? throw new InvalidOperationException($"Не удалось найти метаданные для типа {typeof(T).Name}.");

        var pkProperty = entityType.FindPrimaryKey()?.Properties.FirstOrDefault()
                         ?? throw new InvalidOperationException($"Тип {typeof(T).Name} не имеет первичного ключа.");

        var keyName = pkProperty.Name;

        var query = Context.Set<T>()
            .Where(e => ids.Contains(EF.Property<long>(e, keyName)));

        return query;
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