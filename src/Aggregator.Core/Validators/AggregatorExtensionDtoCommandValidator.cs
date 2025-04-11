using Aggregator.DTOs;
using FluentValidation;

namespace Aggregator.Core.Validators;

/// <summary>
/// Валидатор для команды DTO объекта AggregatorExtension.
/// </summary>
/// <remarks>
/// Устанавливает правила валидации для свойств объекта AggregatorExtensionDto.
/// </remarks>
public class AggregatorExtensionDtoCommandValidator : AbstractValidator<AggregatorExtensionDto>
{
    /// <summary>
    /// Инициализирует новый экземпляр <see cref="AggregatorExtensionDtoCommandValidator"/> и задаёт правила валидации.
    /// </summary>
    public AggregatorExtensionDtoCommandValidator()
    {
        RuleFor(x => x.Id)
            .MinimumLength(1)
            .MaximumLength(50)
            .WithMessage("Id must be between 1 and 50 characters");

        RuleFor(x => x.Critical)
            .InclusiveBetween(0, 1)
            .WithMessage("Id must be in [0..1]");

        RuleForEach(x => x.Parameters)
            .SetValidator(new AggregatorExtensionParametersDtoCommandValidator())
            .When(x => x.Parameters != null);
    }
}