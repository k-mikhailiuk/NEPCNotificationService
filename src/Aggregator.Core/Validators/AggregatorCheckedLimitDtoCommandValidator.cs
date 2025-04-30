using Aggregator.DTOs;
using FluentValidation;

namespace Aggregator.Core.Validators;

/// <summary>
/// Валидатор для команды DTO объекта AggregatorCheckedLimit.
/// </summary>
/// <remarks>
/// Устанавливает правила валидации для свойств объекта AggregatorCheckedLimitDto.
/// </remarks>
public class AggregatorCheckedLimitDtoCommandValidator : AbstractValidator<AggregatorCheckedLimitDto>
{
    /// <summary>
    /// Инициализирует новый экземпляр <see cref="AggregatorCheckedLimitDtoCommandValidator"/> и задаёт правила валидации.
    /// </summary>
    public AggregatorCheckedLimitDtoCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThanOrEqualTo(1)
            .WithMessage("Id must be greater than 1.");

        RuleFor(x => x.Type)
            .IsInEnum()
            .WithMessage("Type must be a in enum range.");

        RuleFor(x => x.Exceeded)
            .NotNull()
            .WithMessage("Exceeded value must be provided.");
    }
}