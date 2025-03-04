using Aggregator.DTOs;
using FluentValidation;

namespace Aggregator.Core.Validators;

public class AggregatorAuthorizationDtoCommandValidator : AbstractValidator<AggregatorAuthorizationDto>
{
    public AggregatorAuthorizationDtoCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotNull()
            .WithMessage("Id cannot be null");
        
        RuleFor(x=>x.Reversal)
            .InclusiveBetween(0, 1)
            .WithMessage("Reversal must be 0 or 1");
    }
}