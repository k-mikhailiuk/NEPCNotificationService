using Aggregator.DTOs;
using FluentValidation;

namespace Aggregator.Core.Validators;

/// <summary>
/// Валидатор для команды DTO объекта AggregatorLimitWrapper.
/// </summary>
/// <remarks>
/// Устанавливает правила валидации для свойств объекта AggregatorLimitWrapperDto, включая валидацию для AmtLimit и CntLimit.
/// </remarks>
public class AggregatorLimitWrapperDtoCommandValidator : AbstractValidator<AggregatorLimitWrapperDto>
{
    /// <summary>
    /// Инициализирует новый экземпляр <see cref="AggregatorLimitWrapperDtoCommandValidator"/> и задаёт правила валидации.
    /// </summary>
    public AggregatorLimitWrapperDtoCommandValidator()
    {
        RuleFor(x => x.AmtLimit)
            .SetValidator(new AggregatorAmtLimitDtoCommandValidator())
            .When(x => x.AmtLimit is not null);

        RuleFor(x => x.CntLimit)
            .SetValidator(new AggregatorCntLimitDtoCommandValidator())
            .When(x => x.CntLimit is not null);
    }
}