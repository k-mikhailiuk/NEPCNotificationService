using Aggregator.DTOs;
using FluentValidation;

namespace Aggregator.Core.Validators;

public class AggregatorAccountInfoDtoCommandValidator : AbstractValidator<AggregatorAccountInfoDto>
{
    public AggregatorAccountInfoDtoCommandValidator()
    {
        RuleFor(x => x.Id)
            .MinimumLength(1)
            .MaximumLength(32)
            .WithMessage("Id must be between 1 and 32 characters.");

        RuleFor(x => x.Type)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Type must be greater than or equal to 0.");

        RuleFor(x => x.AvailableBalance).SetValidator(new AggregatorMoneyDtoCommandValidator());

        RuleFor(x => x.ExceedLimit)
            .SetValidator(new AggregatorMoneyDtoCommandValidator())
            .When(x => x.ExceedLimit != null);

        RuleForEach(x => x.Limits)
            .SetValidator(new AggregatorLimitWrapperDtoCommandValidator())
            .When(x => x.Limits != null);
    }
}