using Aggregator.Core.Validators.Details;
using Aggregator.DTOs.OwiUserAction;
using FluentValidation;

namespace Aggregator.Core.Validators.Notifications;

/// <summary>
/// Валидатор для DTO объекта действий пользователя (OwiUserAction).
/// </summary>
/// <remarks>
/// Класс устанавливает правила валидации для свойств объекта <see cref="AggregatorOwiUserActionDto"/>,
/// включая проверку вложенных объектов Details, CardInfo и коллекции Extensions.
/// </remarks>
public class AggregatorOwiUserActionDtoCommandValidator : BaseAggregatorNotificationDtoCommandValidator<AggregatorOwiUserActionDto>
{
    /// <summary>
    /// Инициализирует новый экземпляр <see cref="AggregatorOwiUserActionDtoCommandValidator"/> и задаёт правила валидации.
    /// </summary>
    public AggregatorOwiUserActionDtoCommandValidator()
    {
        RuleFor(x => x.Details)
            .SetValidator(new AggregatorOwiUserActionDetailsDtoCommandValidator());

        RuleFor(x => x.CardInfo)
            .SetValidator(new AggregatorCardInfoDtoCommandValidator())
            .When(x => x.CardInfo != null);
    }
}