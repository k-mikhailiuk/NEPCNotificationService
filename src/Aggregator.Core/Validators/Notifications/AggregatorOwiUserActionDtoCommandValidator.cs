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
public class AggregatorOwiUserActionDtoCommandValidator : AbstractValidator<AggregatorOwiUserActionDto>
{
    /// <summary>
    /// Инициализирует новый экземпляр <see cref="AggregatorOwiUserActionDtoCommandValidator"/> и задаёт правила валидации.
    /// </summary>
    public AggregatorOwiUserActionDtoCommandValidator()
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
            .SetValidator(new AggregatorOwiUserActionDetailsDtoCommandValidator());

        RuleFor(x => x.CardInfo)
            .SetValidator(new AggregatorCardInfoDtoCommandValidator())
            .When(x => x.CardInfo != null);

        RuleForEach(x => x.Extensions)
            .SetValidator(new AggregatorExtensionDtoCommandValidator())
            .When(x => x.Extensions != null);
    }
}