using Aggregator.Core.Validators.Details;
using Aggregator.DTOs.PinChange;

namespace Aggregator.Core.Validators.Notifications;

/// <summary>
/// Валидатор для DTO объекта изменения PIN-кода.
/// </summary>
/// <remarks>
/// Этот валидатор проверяет свойства объекта <see cref="AggregatorPinChangeDto"/>, включая валидацию вложенных объектов Details, CardInfo и коллекции Extensions.
/// </remarks>
public class AggregatorPinChangeDtoCommandValidator : BaseAggregatorNotificationDtoCommandValidator<AggregatorPinChangeDto>
{
    /// <summary>
    /// Инициализирует новый экземпляр <see cref="AggregatorPinChangeDtoCommandValidator"/> и задаёт правила валидации.
    /// </summary>
    public AggregatorPinChangeDtoCommandValidator()
    {
        RuleFor(x => x.Details)
            .SetValidator(new AggregatorPinChangeDetailsDtoCommandValidator());

        RuleFor(x => x.CardInfo)
            .SetValidator(new AggregatorCardInfoDtoCommandValidator());
    }
}