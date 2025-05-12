using Aggregator.Core.Validators.Details;
using Aggregator.DTOs.AcctBalChange;
using FluentValidation;

namespace Aggregator.Core.Validators.Notifications;

/// <summary>
/// Валидатор для команды DTO объекта изменения баланса счета (AcctBalChange).
/// </summary>
/// <remarks>
/// Устанавливает правила валидации для свойств объекта <see cref="AggregatorAcctBalChangeDto"/>,
/// включая валидацию вложенных объектов Details, CardInfo, AccountInfo и Extensions.
/// </remarks>
public class AggregatorAcctBalChangeDtoCommandValidator
    : BaseAggregatorNotificationDtoCommandValidator<AggregatorAcctBalChangeDto>
{
    /// <summary>
    /// Инициализирует новый экземпляр <see cref="AggregatorAcctBalChangeDtoCommandValidator"/> и задаёт правила валидации.
    /// </summary>
    public AggregatorAcctBalChangeDtoCommandValidator()
    {
        RuleFor(x => x.Details)
            .SetValidator(new AggregatorAcctBalChangeDetailsDtoCommandValidator());

        RuleFor(x => x.CardInfo)
            .SetValidator(new AggregatorCardInfoDtoCommandValidator())
            .When(x => x.CardInfo != null);

        RuleForEach(x => x.AccountsInfo)
            .SetValidator(new AggregatorAccountInfoDtoCommandValidator());
    }
}