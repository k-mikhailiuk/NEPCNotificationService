using ControlPanel.DataAccess.Entities;

namespace Aggregator.Repositories.Abstractions.Repositories;

/// <summary>
/// Интерфейс репозитория для работы с LimitIdDescriptionDirectory.
/// </summary>
/// <remarks>
/// Предоставляет методы для получения и управления сущностями типа <see cref="LimitIdDescriptionDirectory"/>.
/// </remarks>
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