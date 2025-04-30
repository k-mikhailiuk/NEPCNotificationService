using Aggregator.Core.Validators.Details;
using Aggregator.DTOs.IssFinAuth;
using FluentValidation;

namespace Aggregator.Core.Validators.Notifications;

/// <summary>
/// Валидатор для DTO объекта авторизации IssFinAuth.
/// </summary>
/// <remarks>
/// Класс устанавливает правила валидации для свойств объекта <see cref="AggregatorIssFinAuthDto"/>,
/// включая проверку вложенных объектов Details, CardInfo, AccountInfo, MerchantInfo и коллекции Extensions.
/// </remarks>
public class AggregatorIssFinAuthDtoCommandValidator : AbstractValidator<AggregatorIssFinAuthDto>
{
    /// <summary>
    /// Инициализирует новый экземпляр <see cref="AggregatorIssFinAuthDtoCommandValidator"/> и задаёт правила валидации.
    /// </summary>
    public AggregatorIssFinAuthDtoCommandValidator()
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
            .SetValidator(new AggregatorIssFinAuthDetailsDtoCommandValidator());

        RuleFor(x => x.CardInfo)
            .SetValidator(new AggregatorCardInfoDtoCommandValidator())
            .When(x => x.CardInfo != null);

        RuleForEach(x => x.AccountsInfo)
            .SetValidator(new AggregatorAccountInfoDtoCommandValidator());

        RuleFor(x => x.MerchantInfo)
            .SetValidator(new AggregatorMerchantInfoDtoCommandValidator());

        RuleForEach(x => x.Extensions)
            .SetValidator(new AggregatorExtensionDtoCommandValidator())
            .When(x => x.Extensions != null);
    }
}