namespace Aggregator.Core.Services.Abstractions;

public interface ICurrencyReplacer
{
    public Task<string?> ReplaceCurrency(string currency, CancellationToken cancellationToken = default);
}