using Aggregator.DataAccess.Abstractions.Repositories;
using ControlPanel.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Aggregator.DataAccess.Repositories;

/// <summary>
/// Репозиторий для работы с Currency.
/// </summary>
/// <remarks>
/// Реализует интерфейс <see cref="ICurrenciesRepository"/> и наследует базовый класс 
/// <see cref="Repository{T}"/> для сущности <see cref="Currency"/>.
/// </remarks>
public class CurrenciesRepository(AggregatorDbContext context) : Repository<Currency>(context), ICurrenciesRepository
{
    /// <summary>
    /// Асинхронно получает объект <see cref="Currency"/> по его коду.
    /// </summary>
    /// <param name="code">Код валюты.</param>
    /// <param name="cancellationToken">Токен для отмены операции.</param>
    /// <returns>
    /// Объект <see cref="Currency"/>, если найден, иначе <c>null</c>.
    /// </returns>
    public async Task<Currency?> GetByCodeAsync(int code, CancellationToken cancellationToken)
        => await DbSet.FirstOrDefaultAsync(x => x.CurrencyCode == code, cancellationToken: cancellationToken);
}
