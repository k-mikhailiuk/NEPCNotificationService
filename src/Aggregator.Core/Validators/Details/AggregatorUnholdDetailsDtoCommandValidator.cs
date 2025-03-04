using Aggregator.DTOs.Unhold;
using FluentValidation;

namespace Aggregator.Core.Validators.Details;

public class AggregatorUnholdDetailsDtoCommandValidator : AbstractValidator<AggregatorUnholdDetailsDto>
{
    public AggregatorUnholdDetailsDtoCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("Id must be greater than 0");

        RuleFor(x => x.Reversal)
            .InclusiveBetween(0, 1)
            .WithMessage("reversal must be 0 or 1");

        RuleFor(x => x.TransType)
            .NotEmpty()
            .WithMessage("TransType must be not null or empty");

        RuleFor(x => x.CorrespondingAccount)
            .Length(4)
            .WithMessage("CorrespondingAccount must be 4 characters long");

        RuleFor(x => x.AccountId)
            .MinimumLength(1)
            .MaximumLength(32)
            .WithMessage("AccountId must be between from 1 to 32 characters");

        RuleFor(x => x.AuthMoney)
            .SetValidator(new AggregatorMoneyDtoCommandValidator());

        RuleFor(x => x.UnholdDirection)
            .Matches("^[CD]$")
            .WithMessage("UnholdDirection is not valid");
        
        RuleFor(x=>x.UnholdMoney)
            .SetValidator(new AggregatorMoneyDtoCommandValidator());

        RuleFor(x => x.LocalTime)
            .Matches("^[0-9]{14}$")
            .When(x => !string.IsNullOrEmpty(x.LocalTime))
            .WithMessage("localTime must be 14 digits in format YYYYMMDDHH24MISS");

        RuleFor(x => x.TransactionTime)
            .Matches("^[0-9]{14}$")
            .WithMessage("transactionTime must be 14 digits in format YYYYMMDDHH24MISS");

        RuleFor(x => x.ApprovalCode)
            .Length(6)
            .WithMessage("approvalCode must be exactly 6 characters");

        RuleFor(x => x.RRN)
            .Length(12)
            .When(x => !string.IsNullOrEmpty(x.RRN))
            .WithMessage("RRN must be exactly 12 characters");

        RuleFor(x => x.IssFee)
            .SetValidator(new AggregatorMoneyDtoCommandValidator())
            .When(x => x.IssFee is not null);

        RuleFor(x => x.IssFeeDirection)
            .Matches("^[CD]$")
            .When(x => !string.IsNullOrEmpty(x.IssFeeDirection))
            .WithMessage("IssFeeDirection is not valid");

        RuleFor(x => x.SvTrace)
            .GreaterThan(0)
            .When(x => x.SvTrace != null)
            .WithMessage("SvTrace must be greater than 0");
        
        RuleFor(x=>x.WalletProvider)
            .SetValidator(new AggregatorWalletProviderDtoCommandValidator())
            .When(x => x.WalletProvider is not null);

        RuleFor(x => x.Dpan)
            .Matches("^[0-9]{13,19}$")
            .When(x => !string.IsNullOrEmpty(x.Dpan))
            .WithMessage("Dpan is not valid");
        
        RuleFor(x => x.CardIdentifier)
            .NotEmpty()
            .WithMessage("CardIdentifier must be not empty");
    }
}