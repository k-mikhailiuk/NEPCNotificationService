using Aggregator.DTOs;
using FluentValidation;

namespace Aggregator.Core.Validators;

public class AggregatorMoneyDtoCommandValidator : AbstractValidator<AggregatorMoneyDto>
{
    public AggregatorMoneyDtoCommandValidator()
    {
        RuleFor(x=>x.Amount)
            .NotNull()
            .WithMessage("Amount is required.");
        
        RuleFor(x=>x.Currency)
            .Matches(@"^[0-9]{3}$")
            .WithMessage("Currency number is invalid.");
    }
}