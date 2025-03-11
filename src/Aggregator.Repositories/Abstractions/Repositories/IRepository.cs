using System.Linq.Expressions;

namespace Aggregator.Repositories.Abstractions.Repositories;

public interface IRepository<T> where T : class
{
    Task<T?> GetByIdAsync(long id, CancellationToken cancellationToken = default);
    Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);
    Task AddAsync(T entity, CancellationToken cancellationToken = default);
    Task<T?> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);
    void Attach(T entity);
    void Detach(T entity);
    void DetachCollection(IEnumerable<T> entities);
    void AttachCollection(IEnumerable<T> entities);
    Task<List<T>> GetListAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);
    Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken);
}