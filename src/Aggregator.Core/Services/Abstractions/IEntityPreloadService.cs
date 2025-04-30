using Aggregator.DataAccess.Entities.Abstract;

namespace Aggregator.Core.Services.Abstractions;

/// <summary>
/// Обработчик сущностей.
/// </summary>
public interface IEntityPreloadService
{
    /// <summary>
    /// Обрабатывает сущности, добавляя их в БД, если они отсутствуют.
    /// </summary>
    /// <param name="entities">Список сущностей AcctBalChange.</param>
    void ProcessEntities<T>(IEnumerable<T> entities) where T : Notification;
}