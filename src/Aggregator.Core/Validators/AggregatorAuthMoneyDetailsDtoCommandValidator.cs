using Aggregator.DTOs;
using FluentValidation;

namespace Aggregator.Core.Validators;

/// <summary>
/// Валидатор для команды DTO объекта AggregatorAuthMoneyDetails.
/// </summary>
/// <remarks>
/// Класс устанавливает правила валидации для свойств объекта AggregatorAuthMoneyDetailsDto.
/// </remarks>
public class AggregatorAuthMoneyDetailsDtoCommandValidator : AbstractValidator<AggregatorAuthMoneyDetailsDto>
{
    /// <summary>
    /// Инициализирует новый экземпляр <see cref="AggregatorAuthMoneyDetailsDtoCommandValidator"/> и задаёт правила валидации.
    /// </summary>
    public AggregatorAuthMoneyDetailsDtoCommandValidator()
    {
        RuleFor(x => x.ExceedLimitMoney)
            .SetValidator(new AggregatorMoneyDtoCommandValidator());

        RuleFor(x => x.OwnFundsMoney)
            .SetValidator(new AggregatorMoneyDtoCommandValidator());
    }
}