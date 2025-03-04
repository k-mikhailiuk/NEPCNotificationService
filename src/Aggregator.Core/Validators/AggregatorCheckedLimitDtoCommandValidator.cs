using Aggregator.DTOs;
using FluentValidation;

namespace Aggregator.Core.Validators;

public class AggregatorCheckedLimitDtoCommandValidator : AbstractValidator<AggregatorCheckedLimitDto>
{
    public AggregatorCheckedLimitDtoCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(1)
            .WithMessage("Id must be greater than 1.");
        
        RuleFor(x=>x.Type)
            .IsInEnum()
            .WithMessage("Type must be a valid integer.");
        
        RuleFor(x=>x.Exceeded)
            .NotNull()
            .WithMessage("Exceeded value must be provided.");
    }
}