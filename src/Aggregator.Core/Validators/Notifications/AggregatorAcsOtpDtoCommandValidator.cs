using Aggregator.DTOs.AcsOtp;
using FluentValidation;

namespace Aggregator.Core.Validators.Notifications;

public class AggregatorAcsOtpDtoCommandValidator : AbstractValidator<AggregatorAcsOtpDto>
{
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