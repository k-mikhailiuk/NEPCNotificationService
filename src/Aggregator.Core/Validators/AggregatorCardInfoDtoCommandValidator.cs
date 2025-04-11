using Aggregator.DTOs;
using FluentValidation;

namespace Aggregator.Core.Validators;

/// <summary>
/// Валидатор для команды DTO объекта AggregatorCardInfo.
/// </summary>
/// <remarks>
/// Устанавливает правила валидации для свойств объекта AggregatorCardInfoDto.
/// </remarks>
public class AggregatorCardInfoDtoCommandValidator : AbstractValidator<AggregatorCardInfoDto>
{
    /// <summary>
    /// Инициализирует новый экземпляр <see cref="AggregatorCardInfoDtoCommandValidator"/> и задаёт правила валидации.
    /// </summary>
    public AggregatorCardInfoDtoCommandValidator()
    {
        RuleFor(x => x.ExpDate)
            .Matches("^([0-9]{2})(0[1-9]|1[0-2])$")
            .WithMessage("Exp date must is not valid");

        RuleFor(x => x.RefPan)
            .MinimumLength(1)
            .MaximumLength(64)
            .WithMessage("Ref pan must be between 1 and 64 characters");

        RuleFor(x => x.ContractId)
            .Length(6)
            .WithMessage("Contract id must be 6 characters");

        RuleFor(x => x.MobilePhone)
            .MinimumLength(1)
            .MaximumLength(16)
            .WithMessage("Mobile phone must be between 1 and 16 characters");

        RuleFor(x => x.CardIdentifier)
            .NotEmpty()
            .WithMessage("CardIdentifier must be not empty");
    }
}