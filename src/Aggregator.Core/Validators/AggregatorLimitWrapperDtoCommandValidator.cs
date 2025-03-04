using Aggregator.DTOs;
using FluentValidation;

namespace Aggregator.Core.Validators;

public class AggregatorLimitWrapperDtoCommandValidator : AbstractValidator<AggregatorLimitWrapperDto>
{
    public AggregatorLimitWrapperDtoCommandValidator()
    {
        RuleFor(x=>x.AmtLimit)
            .SetValidator(new AggregatorAmtLimitDtoCommandValidator())
            .When(x=>x.AmtLimit is not null);
        
        RuleFor(x=>x.CntLimit)
            .SetValidator(new AggregatorCntLimitDtoCommandValidator())
            .When(x=>x.CntLimit is not null);
    }
}