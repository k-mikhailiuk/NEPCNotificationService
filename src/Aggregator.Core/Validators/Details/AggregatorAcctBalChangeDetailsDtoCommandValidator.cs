using Aggregator.DTOs.AcctBalChange;
using FluentValidation;

namespace Aggregator.Core.Validators.Details;

/// <summary>
/// Валидатор для команды DTO объекта деталей изменения баланса счета (AggregatorAcctBalChangeDetails).
/// </summary>
/// <remarks>
/// Устанавливает правила валидации для свойств объекта AggregatorAcctBalChangeDetailsDto.
/// </remarks>
public class AggregatorAcctBalChangeDetailsDtoCommandValidator : AbstractValidator<AggregatorAcctBalChangeDetailsDto>
{
    /// <summary>
    /// Инициализирует новый экземпляр <see cref="AggregatorAcctBalChangeDetailsDtoCommandValidator"/> и задаёт правила валидации.
    /// </summary>
    public AggregatorAcctBalChangeDetailsDtoCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotNull()
            .WithMessage("Id cannot be null");

        RuleFor(x => x.Reversal)
            .InclusiveBetween(0, 1)
            .WithMessage("reversal must be 0 or 1");

        RuleFor(x => x.TransType)
            .NotNull()
            .WithMessage("TransType cannot be null");

        RuleFor(x => x.IssInstId)
            .Length(4)
            .WithMessage("IssInstId must be 4 characters");

        RuleFor(x => x.AccountId)
            .MinimumLength(1)
            .MaximumLength(32)
            .WithMessage("Account id must be between 1 and 32 characters");

        RuleFor(x => x.Direction)
            .Must(dir => dir is 'C' or 'D')
            .WithMessage("Direction is invalid");
    }
}