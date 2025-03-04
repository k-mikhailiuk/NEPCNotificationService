using Aggregator.DTOs;
using FluentValidation;

namespace Aggregator.Core.Validators;

public class AggregatorExtensionParametersDtoCommandValidator : AbstractValidator<AggregatorExtensionParametersDto>
{
    public AggregatorExtensionParametersDtoCommandValidator()
    {
        RuleFor(x => x.Name)
            .MinimumLength(1)
            .MaximumLength(256)
            .WithMessage("Name must be between 1 and 256 characters.");
        
        RuleFor(x=>x.Value)
            .MinimumLength(1)
            .WithMessage("Value must have at least 1 character.");
    }
}