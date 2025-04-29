using Aggregator.DTOs;
using FluentValidation;

namespace Aggregator.Core.Validators;

/// <summary>
/// Валидатор для команды DTO объекта AggregatorAmtLimit.
/// </summary>
/// <remarks>
/// Устанавливает правила валидации для свойств объекта AggregatorAmtLimitDto.
/// </remarks>
public class AggregatorAmtLimitDtoCommandValidator : AbstractValidator<AggregatorAmtLimitDto>
{
    /// <summary>
    /// Инициализирует новый экземпляр <see cref="AggregatorAmtLimitDtoCommandValidator"/> и задаёт правила валидации.
    /// </summary>
    public AggregatorAmtLimitDtoCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(1).WithMessage("Id should be greater than or equal 1");

        RuleFor(x => x.CycleType)
            .Length(1, 30)
            .When(x => !string.IsNullOrEmpty(x.CycleType))
            .WithMessage("CycleType should be between 1 and 30 characters");

        RuleFor(x => x.CycleLength)
            .GreaterThanOrEqualTo(0)
            .When(x => x.CycleLength is not null)
            .WithMessage("CycleType should be greater than or equal 0");

        RuleFor(x => x.EndTime)
            .Matches("^[0-9]{14}$")
            .When(x => !string.IsNullOrEmpty(x.EndTime))
            .WithMessage("EndTime is not valid");

        RuleFor(x => x.Currency)
            .Matches("^[0-9]{3}$")
            .WithMessage("Currency is not valid");

        RuleFor(x => x.TrsAmount)
            .GreaterThanOrEqualTo(0)
            .WithMessage("TrsAmount should be greater than or equal 0");

        RuleFor(x => x.UsedAmount)
            .GreaterThanOrEqualTo(0)
            .WithMessage("UsedAmount should be greater than or equal 0");
    }
}