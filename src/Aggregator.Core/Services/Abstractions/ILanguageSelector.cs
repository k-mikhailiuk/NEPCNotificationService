namespace Aggregator.Core.Services.Abstractions;

public interface ILanguageSelector
{
    Task<long?> GetLanguageId(long customerId,
        CancellationToken cancellationToken);
}