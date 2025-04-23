using System.Linq.Expressions;

namespace Aggregator.DataAccess.Abstractions.Repositories.PinChange;

/// <summary>
/// Интерфейс репозитория для работы с PinChange.
/// </summary>
/// <remarks>
/// Предоставляет методы для получения и управления сущностями типа <see cref="PinChange"/>.
/// </remarks>
public interface IPinChangeRepository : IRepository<DataAccess.Entities.PinChange.PinChange>
{
    /// <summary>
    /// Получает список уведомлений PinChange по их идентификаторам.
    /// </summary>
    /// <param name="ids">Список идентификаторов уведомлений.</param>
    /// <param name="ct">Токен отмены операции.</param>
    /// <returns>Список сущностей <see cref="DataAccess.Entities.PinChange.PinChange"/>.</returns>
    Task<List<DataAccess.Entities.PinChange.PinChange>> GetByIdsAsync(
        List<long> ids,
        CancellationToken ct = default);
    
    /// <summary>
    /// Получает список уведомлений PinChange по их идентификаторам с загрузкой зависимостей.
    /// </summary>
    /// <param name="ids">Список идентификаторов уведомлений.</param>
    /// <param name="ct">Токен отмены операции.</param>
    /// <param name="includes">Навигационные свойства для включения.</param>
    /// <returns>Список сущностей <see cref="DataAccess.Entities.PinChange.PinChange"/> с подгруженными зависимостями.</returns>
    Task<List<DataAccess.Entities.PinChange.PinChange>> GetByIdsWithIncludesAsync(
        List<long> ids,
        CancellationToken ct = default,
        params Expression<Func<DataAccess.Entities.PinChange.PinChange, object>>[] includes);
}