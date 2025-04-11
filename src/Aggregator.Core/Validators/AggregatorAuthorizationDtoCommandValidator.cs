using Aggregator.DTOs;
using FluentValidation;

namespace Aggregator.Core.Validators;

/// <summary>
/// Валидатор для команды DTO объекта AggregatorAuthorization.
/// </summary>
/// <remarks>
/// Класс устанавливает правила валидации для свойств объекта AggregatorAuthorizationDto.
/// </remarks>
public class AggregatorAuthorizationDtoCommandValidator : AbstractValidator<AggregatorAuthorizationDto>
{
    /// <summary>
    /// Инициализирует новый экземпляр <see cref="AggregatorAuthorizationDtoCommandValidator"/> и задаёт правила валидации.
    /// </summary>
    public AggregatorAuthorizationDtoCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotNull()
            .WithMessage("Id cannot be null");

        RuleFor(x => x.Reversal)
            .InclusiveBetween(0, 1)
            .WithMessage("Reversal must be 0 or 1");
    }
}