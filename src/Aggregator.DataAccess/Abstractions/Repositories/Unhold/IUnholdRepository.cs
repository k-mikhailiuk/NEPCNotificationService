using System.Linq.Expressions;

namespace Aggregator.DataAccess.Abstractions.Repositories.Unhold;

/// <summary>
/// Интерфейс репозитория для работы с Unhold.
/// </summary>
/// <remarks>
/// Предоставляет методы для получения и управления сущностями типа <see cref="Unhold"/>.
/// </remarks>
public interface IUnholdRepository : IRepository<DataAccess.Entities.Unhold.Unhold>
{
    /// <summary>
    /// Получает список уведомлений Unhold по их идентификаторам.
    /// </summary>
    /// <param name="ids">Список идентификаторов уведомлений.</param>
    /// <param name="ct">Токен отмены операции.</param>
    /// <returns>Список сущностей <see cref="Entities.Unhold.Unhold"/>.</returns>
    Task<List<Entities.Unhold.Unhold>> GetByIdsAsync(
        List<long> ids,
        CancellationToken ct = default);
    
    /// <summary>
    /// Получает список уведомлений Unhold по их идентификаторам с загрузкой зависимостей.
    /// </summary>
    /// <param name="ids">Список идентификаторов уведомлений.</param>
    /// <param name="ct">Токен отмены операции.</param>
    /// <param name="includes">Навигационные свойства для включения.</param>
    /// <returns>Список сущностей <see cref="Entities.Unhold.Unhold"/> с подгруженными зависимостями.</returns>
    Task<List<Entities.Unhold.Unhold>> GetByIdsWithIncludesAsync(
        List<long> ids,
        CancellationToken ct = default,
        params Expression<Func<Entities.Unhold.Unhold, object>>[] includes);
}