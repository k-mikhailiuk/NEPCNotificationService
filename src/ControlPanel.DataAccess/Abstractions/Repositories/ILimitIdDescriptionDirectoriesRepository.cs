using ControlPanel.DataAccess.Entities;

namespace ControlPanel.DataAccess.Abstractions.Repositories;

/// <summary>
/// Интерфейс репозитория для работы с LimitIdDescriptionDirectory.
/// </summary>
public interface ILimitIdDescriptionDirectoriesRepository : IRepository<LimitIdDescriptionDirectory>
{
    /// <summary>
    /// Асинхронно получает описание лимита по его коду.
    /// </summary>
    /// <param name="limitCode">Уникальный код лимита.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>
    /// Объект <see cref="LimitIdDescriptionDirectory"/> с описанием лимита, если найден; иначе <c>null</c>.
    /// </returns>
    Task<LimitIdDescriptionDirectory?> GetByLimitCodeAsync(long limitCode, CancellationToken cancellationToken);
}