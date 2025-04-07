namespace Aggregator.Core.Services.Abstractions;

public interface ICurrencyReplacer
{
    public Task<string?> ReplaceCurrencyAsync(string currency, CancellationToken cancellationToken = default);
}