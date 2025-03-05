using Aggregator.DTOs.AcqFinAuth;
using FluentValidation;

namespace Aggregator.Core.Validators.Details;

public class AggregatorAcqFinAuthDetailsDtoCommandValidator : AbstractValidator<AggregatorAcqFinAuthDetailsDto>
{
    public AggregatorAcqFinAuthDetailsDtoCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThanOrEqualTo(1)
            .WithMessage("id must be >= 1");

        RuleFor(x => x.Reversal)
            .InclusiveBetween(0, 1)
            .WithMessage("reversal must be 0 or 1");
        
        RuleFor(x=>x.TransType)
            .NotNull()
            .WithMessage("TransType cannot be null");

        RuleFor(x => x.ExpDate)
            .Matches("^([0-9]{2})(0[1-9]|1[0-2])$")
            .When(x => !string.IsNullOrEmpty(x.ExpDate))
            .WithMessage("ExpDate is invalid");
        
        RuleFor(x=>x.AccountId)
            .MinimumLength(1)
            .MaximumLength(32)
            .When(x => !string.IsNullOrEmpty(x.AccountId))
            .WithMessage("Account Id cannot be empty");
        
        RuleFor(x=>x.CorrespondingAccount)
            .Length(4)
            .WithMessage("Corresponding account cannot be empty");

        RuleFor(x => x.AuthMoney)
            .SetValidator(new AggregatorMoneyDtoCommandValidator());
        
        RuleFor(x=>x.AuthDirection)
            .Must(dir => dir is 'C' or 'D')
            .WithMessage("AuthDirection is invalid");
        
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
            .When(x => x.AcqFee is not null);
        
        RuleFor(x=>x.AcqFeeDirection)
            .Must(dir => dir is 'C' or 'D')
            .When(x=> x.AcqFeeDirection != null)
            .WithMessage("AcqFeeDirection is invalid");
        
        RuleFor(x=>x.ConvMoney)
            .SetValidator(new AggregatorMoneyDtoCommandValidator())
            .When(x => x.ConvMoney is not null);
        
        RuleFor(x => x.PhysTerm)
            .InclusiveBetween(0, 1)
            .WithMessage("reversal must be 0 or 1");

        RuleFor(x => x.AuthorizationCondition)
            .Length(12)
            .When(x=> string.IsNullOrEmpty(x.AuthorizationCondition))
            .WithMessage("AuthorizationCondition must be exactly 12 characters");
        
        RuleFor(x => x.PosEntryMode)
            .Length(4)
            .When(x => !string.IsNullOrEmpty(x.PosEntryMode))
            .WithMessage("CardIdentifier must be exactly 4 characters");
        
        RuleFor(x=>x.ServiceId)
            .MinimumLength(1)
            .MaximumLength(12)
            .When(x => !string.IsNullOrEmpty(x.ServiceId))
            .WithMessage("Service Id should be from 1 to 12 characters");
        
        RuleFor(x=>x.ServiceCode)
            .Matches("^\\d{3}$")
            .When(x => !string.IsNullOrEmpty(x.ServiceCode))
            .WithMessage("Service Code is invalid");
        
        RuleFor(x=>x.CardIdentifier)
            .NotEmpty()
            .WithMessage("Card identifier should not be empty");
    }   
}