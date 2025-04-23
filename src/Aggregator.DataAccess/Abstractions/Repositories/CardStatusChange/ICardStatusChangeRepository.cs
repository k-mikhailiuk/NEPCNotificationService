using System.Linq.Expressions;

namespace Aggregator.DataAccess.Abstractions.Repositories.CardStatusChange;

/// <summary>
/// Интерфейс репозитория для работы с CardStatusChange.
/// </summary>
/// <remarks>
/// Предоставляет методы для получения и управления сущностями типа <see cref="CardStatusChange"/>.
/// </remarks>
public interface ICardStatusChangeRepository : IRepository<DataAccess.Entities.CardStatusChange.CardStatusChange>
{
    /// <summary>
    /// Получает список уведомлений CardStatusChange по их идентификаторам.
    /// </summary>
    /// <param name="ids">Список идентификаторов уведомлений.</param>
    /// <param name="ct">Токен отмены операции.</param>
    /// <returns>Список сущностей <see cref="DataAccess.Entities.CardStatusChange.CardStatusChange"/>.</returns>
    Task<List<DataAccess.Entities.CardStatusChange.CardStatusChange>> GetByIdsAsync(
        List<long> ids,
        CancellationToken ct = default);
    
    /// <summary>
    /// Получает список уведомлений CardStatusChange по их идентификаторам с загрузкой зависимостей.
    /// </summary>
    /// <param name="ids">Список идентификаторов уведомлений.</param>
    /// <param name="ct">Токен отмены операции.</param>
    /// <param name="includes">Навигационные свойства для включения.</param>
    /// <returns>Список сущностей <see cref="DataAccess.Entities.CardStatusChange.CardStatusChange"/> с подгруженными зависимостями.</returns>
    Task<List<DataAccess.Entities.CardStatusChange.CardStatusChange>> GetByIdsWithIncludesAsync(
        List<long> ids,
        CancellationToken ct = default,
        params Expression<Func<DataAccess.Entities.CardStatusChange.CardStatusChange, object>>[] includes);
}