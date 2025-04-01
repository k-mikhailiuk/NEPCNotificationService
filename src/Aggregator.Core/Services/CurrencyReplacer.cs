using Aggregator.Core.Services.Abstractions;
using Aggregator.DataAccess;
using Aggregator.Repositories.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Aggregator.Core.Services;

public class CurrencyReplacer(IServiceProvider serviceProvider) : ICurrencyReplacer
{
    public async Task<string?> ReplaceCurrency(string currency, CancellationToken cancellationToken = default)
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