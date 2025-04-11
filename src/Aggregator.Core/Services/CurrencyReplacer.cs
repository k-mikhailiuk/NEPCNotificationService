using Aggregator.Core.Services.Abstractions;
using Aggregator.DataAccess;
using Aggregator.Repositories.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Aggregator.Core.Services;

/// <summary>
/// Реализация интерфейса <see cref="ICurrencyReplacer"/> для замены идентификатора валюты на соответствующий символ.
/// </summary>
public class CurrencyReplacer(IServiceProvider serviceProvider) : ICurrencyReplacer
{
    /// <summary>
    /// Асинхронно заменяет валютный код на строковое представление валютного символа.
    /// </summary>
    /// <param name="currency">Строка с валютным кодом.</param>
    /// <param name="cancellationToken">Токен для отмены операции.</param>
    /// <returns>Строку, представляющую валютный символ, или пустую строку, если замена не выполнена.</returns>
    public async Task<string?> ReplaceCurrencyAsync(string currency, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(currency))
            return string.Empty;
        
        using var scope = serviceProvider.CreateScope();

        await using var context = scope.ServiceProvider.GetRequiredService<AggregatorDbContext>();
        
        using var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
        
        int.TryParse(currency, out var currencyCode);
        
        if(currencyCode == 0)
            return string.Empty;
        
        var currencyFromDb = await unitOfWork.Currencies.GetByCodeAsync(currencyCode, cancellationToken);
        
        return currencyFromDb is null ? string.Empty : currencyFromDb.CurrencySymbol;
    }
}