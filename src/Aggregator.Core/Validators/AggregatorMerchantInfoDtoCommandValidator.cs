using Aggregator.DTOs;
using FluentValidation;

namespace Aggregator.Core.Validators;

/// <summary>
/// Валидатор для команды DTO объекта AggregatorMerchantInfo.
/// </summary>
/// <remarks>
/// Устанавливает правила валидации для свойств объекта AggregatorMerchantInfoDto.
/// </remarks>
public class AggregatorMerchantInfoDtoCommandValidator : AbstractValidator<AggregatorMerchantInfoDto>
{
    /// <summary>
    /// Инициализирует новый экземпляр <see cref="AggregatorMerchantInfoDtoCommandValidator"/> и задаёт правила валидации.
    /// </summary>
    public AggregatorMerchantInfoDtoCommandValidator()
    {
        RuleFor(x => x.Id)
            .Length(1, 15)
            .When(x => !string.IsNullOrWhiteSpace(x.Id))
            .WithMessage("Id must be between 1 and 15 characters");

        RuleFor(x => x.MCC)
            .Matches("^[0-9]{4}$")
            .WithMessage("MCC is not valid");

        RuleFor(x => x.TerminalId)
            .Length(1, 8)
            .When(x => !string.IsNullOrWhiteSpace(x.TerminalId))
            .WithMessage("TerminalId must be between 1 and 8 characters");

        RuleFor(x => x.Aid)
            .Length(1, 11)
            .When(x => !string.IsNullOrWhiteSpace(x.Aid))
            .WithMessage("Aid must be between 1 and 11 characters");

        RuleFor(x => x.Name)
            .Length(1, 25)
            .When(x => !string.IsNullOrWhiteSpace(x.Name))
            .WithMessage("Name must be between 1 and 25 characters");

        RuleFor(x => x.Street)
            .Length(1, 31)
            .When(x => !string.IsNullOrWhiteSpace(x.Street))
            .WithMessage("Street must be between 1 and 31 characters");

        RuleFor(x => x.City)
            .Length(1, 31)
            .When(x => !string.IsNullOrWhiteSpace(x.City))
            .WithMessage("City must be between 1 and 31 characters");

        RuleFor(x => x.Country)
            .Matches("^[0-9]{3}$")
            .When(x => !string.IsNullOrWhiteSpace(x.Country))
            .WithMessage("Country is not valid");

        RuleFor(x => x.ZipCode)
            .Length(1, 10)
            .When(x => !string.IsNullOrWhiteSpace(x.ZipCode))
            .WithMessage("ZipCode must be between 1 and 10 characters");
    }
}