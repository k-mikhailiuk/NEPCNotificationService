using Aggregator.DTOs;
using FluentValidation;

namespace Aggregator.Core.Validators;

/// <summary>
/// Валидатор для команды DTO объекта AggregatorWalletProvider.
/// </summary>
/// <remarks>
/// Устанавливает правила валидации для свойств объекта AggregatorWalletProviderDto.
/// </remarks>
public class AggregatorWalletProviderDtoCommandValidator : AbstractValidator<AggregatorWalletProviderDto>
{
    /// <summary>
    /// Инициализирует новый экземпляр <see cref="AggregatorWalletProviderDtoCommandValidator"/> и задаёт правила валидации.
    /// </summary>
    public AggregatorWalletProviderDtoCommandValidator()
    {
        RuleFor(x => x.PaymentSystem)
            .Matches("^(VISA|MC|MIR)$")
            .WithMessage("Payment system is not supported");

        RuleFor(x => x.PaymentSystemId)
            .Length(1, 11)
            .WithMessage("Payment system id should be between 1 and 11 characters length");
    }
}