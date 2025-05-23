using Aggregator.DTOs.TokenStausChange;
using FluentValidation;

namespace Aggregator.Core.Validators.Details;

/// <summary>
/// Валидатор для команды DTO объекта деталей изменения статуса токена.
/// </summary>
/// <remarks>
/// Устанавливает правила валидации для свойств объекта <see cref="AggregatorTokenStatusChangeDetailsDto"/>.
/// </remarks>
public class
    AggregatorTokenStatusChangeDetailsDtoCommandValidator : AbstractValidator<AggregatorTokenStatusChangeDetailsDto>
{
    /// <summary>
    /// Инициализирует новый экземпляр <see cref="AggregatorTokenStatusChangeDetailsDtoCommandValidator"/> и задаёт правила валидации.
    /// </summary>
    public AggregatorTokenStatusChangeDetailsDtoCommandValidator()
    {
        RuleFor(x => x.DpanRef)
            .MinimumLength(1)
            .MaximumLength(48)
            .WithMessage("DPAN REF must be between 1 to 48 characters.");

        RuleFor(x => x.PaymentSystem)
            .Matches("^(VISA|MC|MIR)$")
            .WithMessage("Payment system is not valid.");

        RuleFor(x => x.Status)
            .Must(dir => dir is 'A' or 'I' or 'S' or 'L')
            .WithMessage("Status is not valid.");

        RuleFor(x => x.ChangeDate)
            .Matches("^[0-9]{14}$")
            .WithMessage("Change date is not valid.");

        RuleFor(x => x.DpanExpDate)
            .Matches("^([0-9]{2})(0[1-9]|1[0-2])$")
            .WithMessage("DPAN exp date is not valid.");

        RuleFor(x => x.WalletProvider)
            .Length(1, 11)
            .WithMessage("WalletProvider must be between 1 to 11 characters.");

        RuleFor(x => x.DeviceName)
            .Length(1, 128)
            .When(x => !string.IsNullOrWhiteSpace(x.DeviceName))
            .WithMessage("DeviceName must be between 1 to 128 characters.");

        RuleFor(x => x.DeviceType)
            .Length(1, 30)
            .When(x => !string.IsNullOrWhiteSpace(x.DeviceType))
            .WithMessage("DeviceType must be between 1 to 128 characters.");

        RuleFor(x => x.DeviceId)
            .Length(1, 48)
            .When(x => !string.IsNullOrWhiteSpace(x.DeviceId))
            .WithMessage("DeviceId must be between 1 to 128 characters.");

        RuleFor(x => x.FpanRef)
            .Length(1, 48)
            .When(x => !string.IsNullOrWhiteSpace(x.FpanRef))
            .WithMessage("FpanRef must be between 1 to 128 characters.");

        RuleFor(x => x.CardIdentifier)
            .NotEmpty()
            .WithMessage("CardIdentifier must be not empty");
    }
}