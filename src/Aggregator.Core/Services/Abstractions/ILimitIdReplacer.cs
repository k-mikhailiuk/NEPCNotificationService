using Common.Enums;

namespace Aggregator.Core.Services.Abstractions;

public interface ILimitIdReplacer
{
    public Task<string?> ReplaceLimitIdAsync(long limitId, Language language, CancellationToken cancellationToken = default);
}