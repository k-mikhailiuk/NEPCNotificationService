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
public class AggregatorIssFinAuthDtoCommandValidator : BaseAggregatorNotificationDtoCommandValidator<AggregatorIssFinAuthDto>
{
    /// <summary>
    /// Инициализирует новый экземпляр <see cref="AggregatorIssFinAuthDtoCommandValidator"/> и задаёт правила валидации.
    /// </summary>
    public AggregatorIssFinAuthDtoCommandValidator()
    {
        RuleFor(x => x.Details)
            .SetValidator(new AggregatorIssFinAuthDetailsDtoCommandValidator());

        RuleFor(x => x.CardInfo)
            .SetValidator(new AggregatorCardInfoDtoCommandValidator())
            .When(x => x.CardInfo != null);

        RuleForEach(x => x.AccountsInfo)
            .SetValidator(new AggregatorAccountInfoDtoCommandValidator());

        RuleFor(x => x.MerchantInfo)
            .SetValidator(new AggregatorMerchantInfoDtoCommandValidator());
    }
}