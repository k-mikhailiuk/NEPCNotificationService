using Aggregator.DTOs.PinChange;
using FluentValidation;

namespace Aggregator.Core.Validators.Details;

/// <summary>
/// Валидатор для команды DTO объекта деталей изменения PIN-кода.
/// </summary>
/// <remarks>
/// Устанавливает правила валидации для свойств объекта <see cref="AggregatorPinChangeDetailsDto"/>.
/// </remarks>
public class AggregatorPinChangeDetailsDtoCommandValidator : AbstractValidator<AggregatorPinChangeDetailsDto>
{
    /// <summary>
    /// Инициализирует новый экземпляр <see cref="AggregatorPinChangeDetailsDtoCommandValidator"/> и задаёт правила валидации.
    /// </summary>
    public AggregatorPinChangeDetailsDtoCommandValidator()
    {
        RuleFor(x => x.ExpDate)
            .Matches("^([0-9]{2})(0[1-9]|1[0-2])$")
            .WithMessage("Id is invalid");

        RuleFor(x => x.TransactionTime)
            .Matches("^[0-9]{14}$")
            .WithMessage("transactionTime must be 14 digits in format YYYYMMDDHH24MISS");

        RuleFor(x => x.Status)
            .Matches("^(OK|NOK)$")
            .WithMessage("status must be OK or NOK");

        RuleFor(x => x.ResponseCode)
            .InclusiveBetween(1, 999999)
            .WithMessage("responseCode must be in [1..999999], i.e. up to 6 digits");

        RuleFor(x => x.Service)
            .MinimumLength(1)
            .MaximumLength(30)
            .WithMessage("service must be between 1 and 30 characters");

        RuleFor(x => x.CardIdentifier)
            .NotEmpty()
            .WithMessage("CardIdentifier must be not empty");
    }
}