using Aggregator.DTOs;
using FluentValidation;

namespace Aggregator.Core.Validators;

public class AggregatorWalletProviderDtoCommandValidator : AbstractValidator<AggregatorWalletProviderDto>
{
    public AggregatorWalletProviderDtoCommandValidator()
    {
        RuleFor(x=>x.PaymentSystem)
            .Matches("^(VISA|MC|MIR)$")
            .WithMessage("Payment system is not supported");
        
        RuleFor(x=>x.PaymentSystemId)
            .MinimumLength(1)
            .MaximumLength(11)
            .WithMessage("Payment system id should be between 1 and 11 characters length");
    }
}