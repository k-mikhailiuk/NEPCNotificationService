using Aggregator.DTOs.AcsOtp;
using FluentValidation;

namespace Aggregator.Core.Validators.Notifications;

/// <summary>
/// Валидатор для команды DTO объекта авторизации AcsOtp.
/// </summary>
/// <remarks>
/// Устанавливает правила валидации для свойств объекта <see cref="AggregatorAcsOtpDto"/>,
/// включая валидацию вложенных объектов Details, CardInfo и коллекции Extensions.
/// </remarks>
public class AggregatorAcsOtpDtoCommandValidator : AbstractValidator<AggregatorAcsOtpDto>
{
    /// <summary>
    /// Инициализирует новый экземпляр <see cref="AggregatorAcsOtpDtoCommandValidator"/> и задаёт правила валидации.
    /// </summary>
    public AggregatorAcsOtpDtoCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThanOrEqualTo(1)
            .WithMessage("Id must be >= 1");

        RuleFor(x => x.EventId)
            .GreaterThanOrEqualTo(1)
            .WithMessage("EventId must be >= 1");

        RuleFor(x => x.Time)
            .Matches("^[0-9]{14}$")
            .WithMessage("Time must be 14 digits in the format YYYYMMDDHH24MISS");

        RuleFor(x => x.Details.TransactionTime)
            .Matches("^[0-9]{14}$")
            .WithMessage("Time must be 14 digits in the format YYYYMMDDHH24MISS");

        RuleFor(x => x.Details.AuthMoney).SetValidator(new AggregatorMoneyDtoCommandValidator());

        RuleFor(x => x.Details.OtpInfo.Otp)
            .MinimumLength(4)
            .WithMessage("Otp must be at least 4 characters long");

        RuleFor(x => x.Details.OtpInfo.ExpirationTime)
            .Matches("^[0-9]{14}$")
            .WithMessage("Time must be 14 digits in the format YYYYMMDDHH24MISS");

        RuleFor(x => x.CardInfo)
            .SetValidator(new AggregatorCardInfoDtoCommandValidator());

        RuleForEach(x => x.Extensions)
            .SetValidator(new AggregatorExtensionDtoCommandValidator())
            .When(x => x.Extensions != null);
    }
}