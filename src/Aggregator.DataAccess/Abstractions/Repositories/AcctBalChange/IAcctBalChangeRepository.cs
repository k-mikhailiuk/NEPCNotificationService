using System.Linq.Expressions;

namespace Aggregator.DataAccess.Abstractions.Repositories.AcctBalChange;

/// <summary>
/// Интерфейс репозитория для работы с AcctBalChange.
/// </summary>
/// <remarks>
/// Предоставляет методы для получения и управления сущностями типа <see cref="AcctBalChange"/>.
/// </remarks>
public interface IAcctBalChangeRepository : IRepository<DataAccess.Entities.AcctBalChange.AcctBalChange>
{
    /// <summary>
    /// Получает список уведомлений AcctBalChange по их идентификаторам.
    /// </summary>
    /// <param name="ids">Список идентификаторов уведомлений.</param>
    /// <param name="ct">Токен отмены операции.</param>
    /// <returns>Список сущностей <see cref="Entities.AcctBalChange.AcctBalChange"/>.</returns>
    Task<List<Entities.AcctBalChange.AcctBalChange>> GetByIdsAsync(
        List<long> ids,
        CancellationToken ct = default);

    /// <summary>
    /// Получает список уведомлений AcctBalChange по их идентификаторам с загрузкой зависимостей.
    /// </summary>
    /// <param name="ids">Список идентификаторов уведомлений.</param>
    /// <param name="ct">Токен отмены операции.</param>
    /// <param name="includes">Навигационные свойства для включения.</param>
    /// <returns>Список сущностей <see cref="Entities.AcctBalChange.AcctBalChange"/> с подгруженными зависимостями.</returns>
    Task<List<Entities.AcctBalChange.AcctBalChange>> GetByIdsWithIncludesAsync(
        List<long> ids,
        CancellationToken ct = default,
        params Expression<Func<Entities.AcctBalChange.AcctBalChange, object>>[] includes);
}