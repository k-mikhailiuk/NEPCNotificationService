using Aggregator.DTOs.CardStatusChange;
using FluentValidation;

namespace Aggregator.Core.Validators;

public class CardStatusChangeDtoValidator : AbstractValidator<AggregatorCardStatusChangeDto>
{
    public CardStatusChangeDtoValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id cannot be empty");
        RuleFor(x=>x.Id).GreaterThanOrEqualTo(1).WithMessage("Must be greater than or equal to 1");
    }
}
