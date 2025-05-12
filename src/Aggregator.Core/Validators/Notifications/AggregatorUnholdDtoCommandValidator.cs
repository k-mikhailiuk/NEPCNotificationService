using Aggregator.Core.Validators.Details;
using Aggregator.DTOs.Unhold;

namespace Aggregator.Core.Validators.Notifications;

/// <summary>
/// Валидатор для DTO объекта разблокировки (Unhold).
/// </summary>
/// <remarks>
/// Класс устанавливает правила валидации для свойств объекта <see cref="AggregatorUnholdDto"/>,
/// включая проверку вложенных объектов Details, MerchantInfo, CardInfo и коллекции Extensions.
/// </remarks>
public class AggregatorUnholdDtoCommandValidator : BaseAggregatorNotificationDtoCommandValidator<AggregatorUnholdDto>
{
    /// <summary>
    /// Инициализирует новый экземпляр <see cref="AggregatorUnholdDtoCommandValidator"/> и задаёт правила валидации.
    /// </summary>
    public AggregatorUnholdDtoCommandValidator()
    {
        RuleFor(x => x.Details)
            .SetValidator(new AggregatorUnholdDetailsDtoCommandValidator());

        RuleFor(x => x.MerchantInfo)
            .SetValidator(new AggregatorMerchantInfoDtoCommandValidator());

        RuleFor(x => x.CardInfo)
            .SetValidator(new AggregatorCardInfoDtoCommandValidator());
    }
}