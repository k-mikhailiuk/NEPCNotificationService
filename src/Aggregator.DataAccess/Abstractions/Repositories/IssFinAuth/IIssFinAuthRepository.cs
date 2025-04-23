using System.Linq.Expressions;

namespace Aggregator.DataAccess.Abstractions.Repositories.IssFinAuth;

/// <summary>
/// Интерфейс репозитория для работы с IssFinAuth.
/// </summary>
/// <remarks>
/// Предоставляет методы для получения и управления сущностями типа <see cref="IssFinAuth"/>.
/// </remarks>
public interface IIssFinAuthRepository : IRepository<DataAccess.Entities.IssFinAuth.IssFinAuth>
{
    /// <summary>
    /// Получает список уведомлений IssFinAuth по их идентификаторам.
    /// </summary>
    /// <param name="ids">Список идентификаторов уведомлений.</param>
    /// <param name="ct">Токен отмены операции.</param>
    /// <returns>Список сущностей <see cref="DataAccess.Entities.IssFinAuth.IssFinAuth"/>.</returns>
    Task<List<DataAccess.Entities.IssFinAuth.IssFinAuth>> GetByIdsAsync(
        List<long> ids,
        CancellationToken ct = default);
    
    /// <summary>
    /// Получает список уведомлений IssFinAuth по их идентификаторам с загрузкой зависимостей.
    /// </summary>
    /// <param name="ids">Список идентификаторов уведомлений.</param>
    /// <param name="ct">Токен отмены операции.</param>
    /// <param name="includes">Навигационные свойства для включения.</param>
    /// <returns>Список сущностей <see cref="DataAccess.Entities.IssFinAuth.IssFinAuth"/> с подгруженными зависимостями.</returns>
    Task<List<DataAccess.Entities.IssFinAuth.IssFinAuth>> GetByIdsWithIncludesAsync(
        List<long> ids,
        CancellationToken ct = default,
        params Expression<Func<DataAccess.Entities.IssFinAuth.IssFinAuth, object>>[] includes);
}