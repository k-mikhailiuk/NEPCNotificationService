using Aggregator.DTOs;
using FluentValidation;

namespace Aggregator.Core.Validators;

/// <summary>
/// Валидатор для команды DTO объекта AggregatorMoney.
/// </summary>
/// <remarks>
/// Устанавливает правила валидации для свойств объекта AggregatorMoneyDto.
/// </remarks>
public class AggregatorMoneyDtoCommandValidator : AbstractValidator<AggregatorMoneyDto>
{
    /// <summary>
    /// Инициализирует новый экземпляр <see cref="AggregatorMoneyDtoCommandValidator"/> и задаёт правила валидации.
    /// </summary>
    public AggregatorMoneyDtoCommandValidator()
    {
        RuleFor(x => x.Amount)
            .NotNull()
            .WithMessage("Amount is required.");

        RuleFor(x => x.Currency)
            .Matches(@"^[0-9]{3}$")
            .WithMessage("Currency number is invalid.");
    }
}