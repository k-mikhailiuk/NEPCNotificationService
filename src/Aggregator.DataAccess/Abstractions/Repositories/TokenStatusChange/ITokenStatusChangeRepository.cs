using System.Linq.Expressions;

namespace Aggregator.DataAccess.Abstractions.Repositories.TokenStatusChange;

/// <summary>
/// Интерфейс репозитория для работы с TokenStatusChange.
/// </summary>
/// <remarks>
/// Предоставляет методы для получения и управления сущностями типа <see cref="TokenStatusChange"/>.
/// </remarks>
public interface ITokenStatusChangeRepository : IRepository<DataAccess.Entities.TokenChangeStatus.TokenStatusChange>
{
    /// <summary>
    /// Получает список уведомлений TokenStatusChange по их идентификаторам.
    /// </summary>
    /// <param name="ids">Список идентификаторов уведомлений.</param>
    /// <param name="ct">Токен отмены операции.</param>
    /// <returns>Список сущностей <see cref="DataAccess.Entities.TokenChangeStatus.TokenStatusChange"/>.</returns>
    Task<List<DataAccess.Entities.TokenChangeStatus.TokenStatusChange>> GetByIdsAsync(
        List<long> ids,
        CancellationToken ct = default);
    
    /// <summary>
    /// Получает список уведомлений TokenStatusChange по их идентификаторам с загрузкой зависимостей.
    /// </summary>
    /// <param name="ids">Список идентификаторов уведомлений.</param>
    /// <param name="ct">Токен отмены операции.</param>
    /// <param name="includes">Навигационные свойства для включения.</param>
    /// <returns>Список сущностей <see cref="DataAccess.Entities.TokenChangeStatus.TokenStatusChange"/> с подгруженными зависимостями.</returns>
    Task<List<DataAccess.Entities.TokenChangeStatus.TokenStatusChange>> GetByIdsWithIncludesAsync(
        List<long> ids,
        CancellationToken ct = default,
        params Expression<Func<DataAccess.Entities.TokenChangeStatus.TokenStatusChange, object>>[] includes);
}