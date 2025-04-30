using System.Linq.Expressions;
using Aggregator.DataAccess.Entities.Abstract;

namespace Aggregator.DataAccess.Abstractions.Repositories;

/// <summary>
/// Репозиторий для выборки уведомлений и их наследников.
/// </summary>
public interface INotificationsRepository : IRepository<Notification>
{
    /// <summary>
    /// Получает уведомления указанного типа по их идентификаторам.
    /// </summary>
    /// <typeparam name="T">Тип уведомления.</typeparam>
    /// <param name="ids">Список идентификаторов уведомлений.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>
    /// Список уведомлений типа <typeparamref name="T"/>, соответствующих заданным идентификаторам.
    /// </returns>
    Task<List<T>> GetListByIdsAsync<T>(
        List<long>? ids,
        CancellationToken cancellationToken = default)
        where T : Notification;
    
    /// <summary>
    /// Получает уведомления указанного типа по их идентификаторам с загрузкой связанных сущностей.
    /// </summary>
    /// <typeparam name="T">Тип уведомления.</typeparam>
    /// <param name="ids">Список идентификаторов уведомлений.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <param name="includes">Навигационные свойства для включения.</param>
    /// <returns>
    /// Список уведомлений типа <typeparamref name="T"/> с загруженными указанными зависимостями.
    /// </returns>
    Task<List<T>> GetListByIdsAsync<T>(
        List<long>? ids,
        CancellationToken cancellationToken = default,
        params Expression<Func<T, object>>[] includes)
        where T : Notification;
}