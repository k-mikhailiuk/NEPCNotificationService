using Aggregator.DTOs;
using FluentValidation;

namespace Aggregator.Core.Validators;

public class AggregatorTransactionDtoCommandValidator : AbstractValidator<AggregatorTransactionDto>
{
    public AggregatorTransactionDtoCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("Id must be greater than 0");

        RuleFor(x => x.FeTrans)
            .Length(7)
            .Matches("TPTP[0-9]{3}")
            .WithMessage("FeTrans is not valid");

        RuleFor(x => x.TranMoney)
            .SetValidator(new AggregatorMoneyDtoCommandValidator())
            .When(x => x.TranMoney != null);

        RuleFor(x => x.Direction)
            .Must(dir => dir is 'C' or 'D')
            .WithMessage("Direction is not valid");

        RuleFor(x => x.MerchantInfo)
            .SetValidator(new AggregatorMerchantInfoDtoCommandValidator())
            .When(x => x.MerchantInfo != null);
        
        RuleFor(x=>x.CorrespondingAccount)
            .Length(4)
            .When(x=> !string.IsNullOrEmpty(x.CorrespondingAccount))
            .WithMessage("Corresponding account is not valid");
    }
}