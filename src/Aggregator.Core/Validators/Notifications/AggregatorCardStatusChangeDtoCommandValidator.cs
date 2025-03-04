using Aggregator.Core.Validators.Details;
using Aggregator.DTOs.CardStatusChange;
using FluentValidation;

namespace Aggregator.Core.Validators.Notifications;

public class CardStatusChangeDtoValidator : AbstractValidator<AggregatorCardStatusChangeDto>
{
    public CardStatusChangeDtoValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThanOrEqualTo(1)
            .WithMessage("Id must be >= 1");

        RuleFor(x => x.EventId)
            .GreaterThanOrEqualTo(1)
            .WithMessage("EventId must be >= 1");

        RuleFor(x => x.Time)
            .Matches("^[0-9]{14}$")
            .WithMessage("Time must be 14 digits in the format YYYYMMDDHH24MISS");
        
        RuleFor(x => x.Details)
            .SetValidator(new AggregatorCardStatusChangeDetailsDtoCommandValidator());
        
        RuleFor(x => x.CardInfo)
            .SetValidator(new AggregatorCardInfoDtoCommandValidator());
        
        RuleForEach(x=>x.Extensions)
            .SetValidator(new AggregatorExtensionDtoCommandValidator())
            .When(x => x.Extensions != null);
    }
}
