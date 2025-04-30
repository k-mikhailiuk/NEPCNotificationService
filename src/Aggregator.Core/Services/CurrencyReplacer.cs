using Aggregator.Core.Services.Abstractions;
using ControlPanel.DataAccess.Abstractions;
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

        using var unitOfWork = scope.ServiceProvider.GetRequiredService<IControlPanelUnitOfWork>();
        
        if (!int.TryParse(currency, out var currencyCode)) 
            return string.Empty;
        
        var currencyFromDb = await unitOfWork.Currencies.GetByCodeAsync(currencyCode, cancellationToken);
        
        return currencyFromDb is null ? string.Empty : currencyFromDb.CurrencySymbol;
    }
}