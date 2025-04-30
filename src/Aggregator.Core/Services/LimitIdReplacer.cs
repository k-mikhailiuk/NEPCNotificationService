using Aggregator.Core.Services.Abstractions;
using Aggregator.DataAccess.Abstractions;
using Common.Enums;
using ControlPanel.DataAccess.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Aggregator.Core.Services;

/// <summary>
/// Реализация <see cref="ILimitIdReplacer"/> для замены идентификатора лимита на строковое описание.
/// </summary>
public class LimitIdReplacer(IServiceProvider serviceProvider) : ILimitIdReplacer
{
    /// <summary>
    /// Асинхронно заменяет числовой идентификатор лимита на его строковое описание в зависимости от языка.
    /// </summary>
    /// <param name="limitId">Идентификатор лимита.</param>
    /// <param name="language">Язык, для которого нужно получить описание лимита.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>
    /// Строка, представляющая описание лимита, или пустая строка, если описание не найдено или limitId равен 0.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Выбрасывается, если язык не поддерживается.
    /// </exception>
    public async Task<string?> ReplaceLimitIdAsync(long limitId, Language language,
        CancellationToken cancellationToken = default)
    {
        if (limitId == 0)
            return string.Empty;

        using var scope = serviceProvider.CreateScope();

        using var unitOfWork = scope.ServiceProvider.GetRequiredService<IAggregatorUnitOfWork>();
        using var controlPanelUnitOfWork = scope.ServiceProvider.GetRequiredService<IControlPanelUnitOfWork>();

        var limitIdDescription =
            await controlPanelUnitOfWork.LimitIdDescriptionDirectories.GetByLimitCodeAsync(limitId, cancellationToken);
        
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