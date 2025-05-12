using Aggregator.Core.Validators.Details;
using Aggregator.DTOs.CardStatusChange;

namespace Aggregator.Core.Validators.Notifications;

/// <summary>
/// Валидатор для DTO объекта изменения статуса карты (CardStatusChange).
/// </summary>
/// <remarks>
/// Класс устанавливает правила валидации для свойств объекта <see cref="AggregatorCardStatusChangeDto"/>, 
/// включая проверку вложенных объектов Details, CardInfo и коллекции Extensions.
/// </remarks>
public class CardStatusChangeDtoValidator : BaseAggregatorNotificationDtoCommandValidator<AggregatorCardStatusChangeDto>
{
    /// <summary>
    /// Инициализирует новый экземпляр <see cref="CardStatusChangeDtoValidator"/> и задаёт правила валидации.
    /// </summary>
    public CardStatusChangeDtoValidator()
    {
        RuleFor(x => x.Details)
            .SetValidator(new AggregatorCardStatusChangeDetailsDtoCommandValidator());

        RuleFor(x => x.CardInfo)
            .SetValidator(new AggregatorCardInfoDtoCommandValidator());
    }
}