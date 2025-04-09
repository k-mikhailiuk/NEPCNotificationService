using Aggregator.Core.Services.Abstractions;
using Aggregator.DataAccess;
using Aggregator.Repositories.Abstractions;
using Common.Enums;
using Microsoft.Extensions.DependencyInjection;

namespace Aggregator.Core.Services;

public class LimitIdReplacer(IServiceProvider serviceProvider) : ILimitIdReplacer
{
    public async Task<string?> ReplaceLimitIdAsync(long limitId, Language language,
        CancellationToken cancellationToken = default)
    {
        if (limitId == 0)
            return string.Empty;

        using var scope = serviceProvider.CreateScope();

        await using var context = scope.ServiceProvider.GetRequiredService<AggregatorDbContext>();

        using var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

        var limitIdDescription =
            await unitOfWork.LimitIdDescriptionDirectories.GetByLimitCodeAsync(limitId, cancellationToken);
        
        if (limitIdDescription == null)
            return string.Empty;

        return language switch
        {
            Language.Undefined => string.Empty,
            Language.Russian =>
                limitIdDescription.DescriptionRu,
            Language.Kyrgyz =>
                limitIdDescription.DescriptionKg,
            Language.English =>
                limitIdDescription.DescriptionEn,
            _ => throw new ArgumentOutOfRangeException(nameof(language), language, null)
        };
    }
}