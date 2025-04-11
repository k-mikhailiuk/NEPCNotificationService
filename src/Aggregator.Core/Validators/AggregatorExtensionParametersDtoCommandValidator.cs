using Aggregator.DTOs;
using FluentValidation;

namespace Aggregator.Core.Validators;

/// <summary>
/// Валидатор для команды DTO объекта AggregatorExtensionParameters.
/// </summary>
/// <remarks>
/// Устанавливает правила валидации для свойств объекта AggregatorExtensionParametersDto.
/// </remarks>
public class AggregatorExtensionParametersDtoCommandValidator : AbstractValidator<AggregatorExtensionParametersDto>
{
    /// <summary>
    /// Инициализирует новый экземпляр <see cref="AggregatorExtensionParametersDtoCommandValidator"/> и задаёт правила валидации.
    /// </summary>
    public AggregatorExtensionParametersDtoCommandValidator()
    {
        RuleFor(x => x.Name)
            .MinimumLength(1)
            .MaximumLength(256)
            .WithMessage("Name must be between 1 and 256 characters.");

        RuleFor(x => x.Value)
            .MinimumLength(1)
            .WithMessage("Value must have at least 1 character.");
    }
}