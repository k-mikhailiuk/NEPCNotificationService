using Aggregator.DTOs.AcctBalChange;
using FluentValidation;

namespace Aggregator.Core.Validators.Details;

public class AggregatorAcctBalChangeDetailsDtoCommandValidator : AbstractValidator<AggregatorAcctBalChangeDetailsDto>
{
    public AggregatorAcctBalChangeDetailsDtoCommandValidator()
    {
        RuleFor(x=>x.Id)
            .NotNull()
            .WithMessage("Id cannot be null");
        
        RuleFor(x => x.Reversal)
            .InclusiveBetween(0, 1)
            .WithMessage("reversal must be 0 or 1");
        
        RuleFor(x=>x.TransType)
            .NotNull()
            .WithMessage("TransType cannot be null");
        
        RuleFor(x=>x.IssInstId)
            .Length(4)
            .WithMessage("IssInstId must be 4 characters");
        
        RuleFor(x=>x.AccountId)
            .MinimumLength(1)
            .MaximumLength(32)
            .WithMessage("Account id must be between 1 and 32 characters");
        
        RuleFor(x=>x.Direction)
            .Matches("^[CD]$")
            .WithMessage("Direction is invalid");
    }
}