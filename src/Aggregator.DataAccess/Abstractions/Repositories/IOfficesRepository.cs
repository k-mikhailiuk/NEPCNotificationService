using Aggregator.DataAccess.Entities.ABSEntities;

namespace Aggregator.DataAccess.Abstractions.Repositories;

/// <summary>
/// Репозиторий для работы со справочником офисов.
/// </summary>
public interface IOfficesRepository : IRepository<Office>
{
    /// <summary>
    /// Возвращает сопоставление идентификаторов терминалов и соответствующих идентификаторов клиентов.
    /// </summary>
    /// <param name="terminalIds">Набор идентификаторов терминалов.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>
    /// Словарь, где ключ — идентификатор терминала, значение — идентификатор клиента.
    /// </returns>
    Task<Dictionary<string, int>> GetCustomerIdsByTerminalsAsync(
        IReadOnlyCollection<string> terminalIds,
        CancellationToken cancellationToken);
}