using System.Linq.Expressions;

namespace NotificationService.DataAccess.Abstractions;

public interface IRepository<T> where T : class
{
    Task<List<T>> GetAllWithConditionAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default);
}