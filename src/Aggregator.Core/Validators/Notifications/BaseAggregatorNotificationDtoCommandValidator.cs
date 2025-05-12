using Aggregator.DTOs.Abstractions;
using FluentValidation;

namespace Aggregator.Core.Validators.Notifications;

public class BaseAggregatorNotificationDtoCommandValidator<TDto> : AbstractValidator<TDto> 
    where TDto : NotificationAggregatorBaseDto
{
    public BaseAggregatorNotificationDtoCommandValidator()
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
        
        RuleForEach(x => x.Extensions)
            .SetValidator(new AggregatorExtensionDtoCommandValidator())
            .When(x => x.Extensions != null);
    }
}