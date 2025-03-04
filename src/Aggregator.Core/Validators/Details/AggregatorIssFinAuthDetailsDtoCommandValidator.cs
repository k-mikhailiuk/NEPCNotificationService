using Aggregator.DTOs.IssFinAuth;
using FluentValidation;

namespace Aggregator.Core.Validators.Details;

public class AggregatorIssFinAuthDetailsDtoCommandValidator : AbstractValidator<AggregatorIssFinAuthDetailsDto>
{
    public AggregatorIssFinAuthDetailsDtoCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThanOrEqualTo(1)
            .WithMessage("id must be >= 1");

        RuleFor(x => x.Reversal)
            .InclusiveBetween(0, 1)
            .WithMessage("reversal must be 0 or 1");

        RuleFor(x => x.IssInstId)
            .Length(4)
            .WithMessage("issInstId must be exactly 4 characters");

        RuleFor(x => x.CorrespondingAccount)
            .Length(4)
            .WithMessage("correspondingAccount must be exactly 4 characters");

        RuleFor(x => x.AccountId)
            .MinimumLength(1)
            .MaximumLength(32)
            .When(x => !string.IsNullOrEmpty(x.AccountId))
            .WithMessage("accountId length must be between 1 and 32");
        
        RuleFor(x => x.AuthMoney)
            .SetValidator(new AggregatorMoneyDtoCommandValidator());

        RuleFor(x => x.AuthDirection)
            .Matches("^[CD]$")
            .When(x => !string.IsNullOrEmpty(x.AuthDirection))
            .WithMessage("authDirection is invalid");
        
        RuleFor(x => x.ConvMoney)
            .SetValidator(new AggregatorMoneyDtoCommandValidator())
            .When(x=>x.ConvMoney is not null);
        
        RuleFor(x => x.AccountBalance)
            .SetValidator(new AggregatorMoneyDtoCommandValidator())
            .When(x=>x.AccountBalance is not null);
        
        RuleFor(x => x.BillingMoney)
            .SetValidator(new AggregatorMoneyDtoCommandValidator())
            .When(x=>x.BillingMoney is not null);

        RuleFor(x => x.LocalTime)
            .Matches("^[0-9]{14}$")
            .When(x => !string.IsNullOrEmpty(x.LocalTime))
            .WithMessage("localTime must be 14 digits in format YYYYMMDDHH24MISS");

        RuleFor(x => x.TransactionTime)
            .Matches("^[0-9]{14}$")
            .WithMessage("transactionTime must be 14 digits in format YYYYMMDDHH24MISS");

        RuleFor(x => x.ResponseCode)
            .InclusiveBetween(1, 999999)
            .WithMessage("responseCode must be in [1..999999], i.e. up to 6 digits");

        RuleFor(x => x.ApprovalCode)
            .Length(6)
            .When(x => !string.IsNullOrEmpty(x.ApprovalCode))
            .WithMessage("approvalCode must be exactly 6 characters");

        RuleFor(x => x.RRN)
            .Length(12)
            .When(x => !string.IsNullOrEmpty(x.RRN))
            .WithMessage("rrn must be exactly 12 characters");
        
        RuleFor(x => x.AcqFee)
            .SetValidator(new AggregatorMoneyDtoCommandValidator())
            .When(x=>x.AcqFee is not null);

        RuleFor(x => x.AcqFeeDirection)
            .Matches("^[CD]$")
            .When(x => !string.IsNullOrEmpty(x.AcqFeeDirection))
            .WithMessage("acqFeeDirection must be 'C' or 'D'");

        RuleFor(x => x.IssFeeDirection)
            .Matches("^[CD]$")
            .When(x => !string.IsNullOrEmpty(x.IssFeeDirection))
            .WithMessage("issFeeDirection must be 'C' or 'D'");

        RuleFor(x => x.SvTrace)
            .GreaterThanOrEqualTo(1)
            .WithMessage("svTrace must be >= 1");

        RuleFor(x => x.AuthorizationCondition)
            .Length(12)
            .When(x => !string.IsNullOrEmpty(x.AuthorizationCondition))
            .WithMessage("authorizationCondition must be exactly 12 characters");

        RuleFor(x => x.Dpan)
            .Matches("^[0-9]{13,19}$")
            .When(x => !string.IsNullOrEmpty(x.Dpan))
            .WithMessage("dpan is invalid");

        RuleFor(x => x.WalletProvider)
            .SetValidator(new AggregatorWalletProviderDtoCommandValidator())
            .When(x=> x.WalletProvider is not null);

        RuleForEach(x => x.CheckedLimits)
            .SetValidator(new AggregatorCheckedLimitDtoCommandValidator())
            .When(x => x.CheckedLimits is not null);

        RuleFor(x => x.AuthMoneyDetails)
            .SetValidator(new AggregatorAuthMoneyDetailsDtoCommandValidator())
            .When(x => x.AuthMoneyDetails is not null);
        
        RuleFor(x => x.CardIdentifier)
            .NotEmpty()
            .WithMessage("CardIdentifier must be not empty");
    }
}