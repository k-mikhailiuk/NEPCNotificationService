using Aggregator.Core.Validators.Details;
using Aggregator.DTOs.TokenStausChange;

namespace Aggregator.Core.Validators.Notifications;

/// <summary>
/// Валидатор для DTO объекта изменения статуса токена.
/// </summary>
/// <remarks>
/// Этот валидатор проверяет свойства объекта <see cref="AggregatorTokenStatusChangeDto"/>, 
/// включая валидацию вложенных объектов Details, CardInfo и коллекции Extensions.
/// </remarks>
public class AggregatorTokenStatusChangeDtoCommandValidator : BaseAggregatorNotificationDtoCommandValidator<AggregatorTokenStatusChangeDto>
{
    /// <summary>
    /// Инициализирует новый экземпляр <see cref="AggregatorTokenStatusChangeDtoCommandValidator"/> и задаёт правила валидации.
    /// </summary>
    public AggregatorTokenStatusChangeDtoCommandValidator()
    {
        RuleFor(x => x.Details)
            .SetValidator(new AggregatorTokenStatusChangeDetailsDtoCommandValidator());

        RuleFor(x => x.CardInfo)
            .SetValidator(new AggregatorCardInfoDtoCommandValidator());
    }
}