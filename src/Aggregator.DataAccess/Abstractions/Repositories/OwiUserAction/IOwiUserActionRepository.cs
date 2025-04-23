using System.Linq.Expressions;

namespace Aggregator.DataAccess.Abstractions.Repositories.OwiUserAction;

/// <summary>
/// Интерфейс репозитория для работы с OwiUserAction.
/// </summary>
/// <remarks>
/// Предоставляет методы для получения и управления сущностями типа <see cref="OwiUserAction"/>.
/// </remarks>
public interface IOwiUserActionRepository : IRepository<DataAccess.Entities.OwiUserAction.OwiUserAction>
{
    /// <summary>
    /// Получает список уведомлений OwiUserAction по их идентификаторам.
    /// </summary>
    /// <param name="ids">Список идентификаторов уведомлений.</param>
    /// <param name="ct">Токен отмены операции.</param>
    /// <returns>Список сущностей <see cref="DataAccess.Entities.OwiUserAction.OwiUserAction"/>.</returns>
    Task<List<DataAccess.Entities.OwiUserAction.OwiUserAction>> GetByIdsAsync(
        List<long> ids,
        CancellationToken ct = default);
    
    /// <summary>
    /// Получает список уведомлений OwiUserAction по их идентификаторам с загрузкой зависимостей.
    /// </summary>
    /// <param name="ids">Список идентификаторов уведомлений.</param>
    /// <param name="ct">Токен отмены операции.</param>
    /// <param name="includes">Навигационные свойства для включения.</param>
    /// <returns>Список сущностей <see cref="DataAccess.Entities.OwiUserAction.OwiUserAction"/> с подгруженными зависимостями.</returns>
    Task<List<DataAccess.Entities.OwiUserAction.OwiUserAction>> GetByIdsWithIncludesAsync(
        List<long> ids,
        CancellationToken ct = default,
        params Expression<Func<DataAccess.Entities.OwiUserAction.OwiUserAction, object>>[] includes);
}