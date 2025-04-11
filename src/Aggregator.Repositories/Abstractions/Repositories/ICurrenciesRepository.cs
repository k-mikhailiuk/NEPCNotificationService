using ControlPanel.DataAccess.Entities;

namespace Aggregator.Repositories.Abstractions.Repositories;

/// <summary>
/// Интерфейс репозитория для работы с валютами.
/// </summary>
/// <remarks>
/// Предоставляет методы для получения и управления сущностями типа <see cref="Currency"/>.
/// </remarks>
public interface ICurrenciesRepository : IRepository<Currency>
{
    /// <summary>
    /// Асинхронно получает валюту по заданному коду.
    /// </summary>
    /// <param name="code">Код валюты.</param>
    /// <param name="cancellationToken">Токен для отмены операции.</param>
    /// <returns>
    /// Объект <see cref="Currency"/>, если валюта с указанным кодом найдена, или <c>null</c> в противном случае.
    /// </returns>
    Task<Currency?> GetByCodeAsync(int code, CancellationToken cancellationToken);
}