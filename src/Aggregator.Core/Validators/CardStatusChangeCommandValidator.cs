using Aggregator.DTOs.CardStatusChange;
using FluentValidation;

namespace Aggregator.Core.Validators;

public class CardStatusChangeDtoValidator : AbstractValidator<AggregatorCardStatusChangeDto>
{
    public CardStatusChangeDtoValidator()
    {
        
    }
}
