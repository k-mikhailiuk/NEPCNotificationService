using Aggregator.DTOs;
using FluentValidation;

namespace Aggregator.Core.Validators;

public class AggregatorExtensionDtoCommandValidator : AbstractValidator<AggregatorExtensionDto>
{
    public AggregatorExtensionDtoCommandValidator()
    {
        RuleFor(x=>x.ExtensionId)
            .MinimumLength(1)
            .MaximumLength(50)
            .WithMessage("Id must be between 1 and 50 characters");
        
        RuleFor(x=>x.Critical)
            .InclusiveBetween(0,1)
            .WithMessage("Id must be in [0..1]");

        RuleForEach(x => x.Parameters)
            .SetValidator(new AggregatorExtensionParametersDtoCommandValidator())
            .When(x => x.Parameters != null);
    }
}