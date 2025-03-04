using Aggregator.DTOs;
using FluentValidation;

namespace Aggregator.Core.Validators;

public class AggregatorCntLimitDtoCommandValidator : AbstractValidator<AggregatorCntLimitDto>
{
    public AggregatorCntLimitDtoCommandValidator()
    {
        RuleFor(x=>x.Id).GreaterThan(1).WithMessage("Id should be greater than or equal 1");
        
        RuleFor(x=>x.CycleType)
            .MinimumLength(1)
            .MaximumLength(30)
            .When(x=>!string.IsNullOrEmpty(x.CycleType))
            .WithMessage("CycleType should be between 1 and 30 characters");
        
        RuleFor(x=>x.CycleLength)
            .GreaterThanOrEqualTo(0)
            .When(x=>x.CycleLength is not null)
            .WithMessage("CycleType should be greater than or equal 0");
        
        RuleFor(x=>x.EndTime)
            .Matches("^[0-9]{14}$")
            .When(x=>!string.IsNullOrEmpty(x.EndTime))
            .WithMessage("EndTime is not valid");
        
        RuleFor(x=>x.TrsValue)
            .GreaterThanOrEqualTo(0)
            .WithMessage("TrsValue should be greater than or equal 0");
        
        RuleFor(x=>x.UsedValue)
            .GreaterThanOrEqualTo(0)
            .WithMessage("UsedValue should be greater than or equal 0");
    }
}