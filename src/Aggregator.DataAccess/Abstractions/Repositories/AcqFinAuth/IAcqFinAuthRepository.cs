using System.Linq.Expressions;

namespace Aggregator.DataAccess.Abstractions.Repositories.AcqFinAuth;

/// <summary>
/// Интерфейс репозитория для работы с AcqFinAuth.
/// </summary>
/// <remarks>
/// Предоставляет методы для получения и управления сущностями типа <see cref="AcqFinAuth"/>.
/// </remarks>
public interface IAcqFinAuthRepository : IRepository<DataAccess.Entities.AcqFinAuth.AcqFinAuth>
{
    /// <summary>
    /// Получает список уведомлений AcqFinAuth по их идентификаторам.
    /// </summary>
    /// <param name="ids">Список идентификаторов уведомлений.</param>
    /// <param name="ct">Токен отмены операции.</param>
    /// <returns>Список сущностей <see cref="DataAccess.Entities.AcqFinAuth.AcqFinAuth"/>.</returns>
    Task<List<DataAccess.Entities.AcqFinAuth.AcqFinAuth>> GetByIdsAsync(
        List<long> ids,
        CancellationToken ct = default);
    
    /// <summary>
    /// Получает список уведомлений AcqFinAuth по их идентификаторам с загрузкой зависимостей.
    /// </summary>
    /// <param name="ids">Список идентификаторов уведомлений.</param>
    /// <param name="ct">Токен отмены операции.</param>
    /// <param name="includes">Навигационные свойства для включения.</param>
    /// <returns>Список сущностей <see cref="DataAccess.Entities.AcqFinAuth.AcqFinAuth"/> с подгруженными зависимостями.</returns>
    Task<List<DataAccess.Entities.AcqFinAuth.AcqFinAuth>> GetByIdsWithIncludesAsync(
        List<long> ids,
        CancellationToken ct = default,
        params Expression<Func<DataAccess.Entities.AcqFinAuth.AcqFinAuth, object>>[] includes);
}