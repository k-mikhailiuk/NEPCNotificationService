using Aggregator.Core.Services.Abstractions;
using ControlPanel.DataAccess.Abstractions;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;

namespace Aggregator.Core.Services;

/// <summary>
/// Реализация интерфейса <see cref="ICurrencyReplacer"/> для замены идентификатора валюты на соответствующий символ.
/// </summary>
public class CurrencyReplacer(IServiceProvider serviceProvider, IMemoryCache cache) : ICurrencyReplacer
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
        
        if (!int.TryParse(currency, out var currencyCode)) 
            return string.Empty;
        
        var cacheDictionary = await cache.GetOrCreateAsync(currencyCode, async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1);

            using var scope = serviceProvider.CreateScope();
            var uow = scope.ServiceProvider.GetRequiredService<IControlPanelUnitOfWork>();
            var allCurrencies = await uow.Currencies
                .GetAllAsync(cancellationToken);

            return allCurrencies
                .ToDictionary(x => x.CurrencyCode, x => x.CurrencySymbol);
        });

        return cacheDictionary != null && cacheDictionary.TryGetValue(currencyCode, out var symbol)
            ? symbol
            : string.Empty;
    }
}