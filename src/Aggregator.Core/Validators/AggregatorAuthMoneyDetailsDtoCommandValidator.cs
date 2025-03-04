using Aggregator.DTOs;
using FluentValidation;

namespace Aggregator.Core.Validators;

public class AggregatorAuthMoneyDetailsDtoCommandValidator : AbstractValidator<AggregatorAuthMoneyDetailsDto>
{
    public AggregatorAuthMoneyDetailsDtoCommandValidator()
    {
        RuleFor(x => x.ExceedLimitMoney)
            .SetValidator(new AggregatorMoneyDtoCommandValidator());
        
        RuleFor(x=>x.OwnFundsMoney).
            SetValidator(new AggregatorMoneyDtoCommandValidator());
    }   
}