using Aggregator.DTOs.OwiUserAction;
using FluentValidation;

namespace Aggregator.Core.Validators.Details;

public class AggregatorOwiUserActionDetailsDtoCommandValidator : AbstractValidator<AggregatorOwiUserActionDetailsDto>
{
    public AggregatorOwiUserActionDetailsDtoCommandValidator()
    {
        RuleFor(x => x.TransactionTime)
            .Matches("^[0-9]{14}$")
            .WithMessage("Invalid transaction time.");
        
        RuleFor(x=>x.Login)
            .MinimumLength(1)
            .MaximumLength(30)
            .WithMessage("Login must be between 1 and 30 characters.");
        
        RuleFor(x=>x.Action)
            .MinimumLength(1)
            .MaximumLength(30)
            .WithMessage("Action must be between 1 and 30 characters.");
    }
}