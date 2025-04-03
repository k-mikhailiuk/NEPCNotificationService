using Aggregator.DataAccess;

namespace Aggregator.Core.Services.Abstractions;

public interface ILanguageSelector
{
    Task<long?> GetLanguageIdAsync(long customerId, AggregatorDbContext context,
        CancellationToken cancellationToken);
}