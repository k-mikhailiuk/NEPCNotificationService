using Aggregator.DataAccess;

namespace Aggregator.Core.Services.Abstractions;

public interface ILanguageSelector
{
    Task<long?> GetLanguageId(long customerId, AggregatorDbContext context,
        CancellationToken cancellationToken);
}