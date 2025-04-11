using ControlPanel.DataAccess.Entities;

namespace ControlPanel.DataAccess.Abstractions.Repositories;

/// <summary>
/// Интерфейс репозитория для работы с Currency.
/// </summary>
public interface ICurrenciesRepository : IRepository<Currency>
{
    /// <summary>
    /// Возвращает объект Currency по заданному коду.
    /// </summary>
    /// <param name="code">Код валюты.</param>
    /// <param name="cancellationToken">Токен для отмены операции.</param>
    /// <returns>Объект Currency, если найден; иначе, null.</returns>
    Task<Currency?> GetByCodeAsync(int code, CancellationToken cancellationToken);
}