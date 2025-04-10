using Aggregator.Core.Validators.Details;
using Aggregator.DTOs.Unhold;
using FluentValidation;

namespace Aggregator.Core.Validators.Notifications;

/// <summary>
/// Валидатор для DTO объекта разблокировки (Unhold).
/// </summary>
/// <remarks>
/// Класс устанавливает правила валидации для свойств объекта <see cref="AggregatorUnholdDto"/>,
/// включая проверку вложенных объектов Details, MerchantInfo, CardInfo и коллекции Extensions.
/// </remarks>
public class AggregatorUnholdDtoCommandValidator : AbstractValidator<AggregatorUnholdDto>
{
    /// <summary>
    /// Инициализирует новый экземпляр <see cref="AggregatorUnholdDtoCommandValidator"/> и задаёт правила валидации.
    /// </summary>
    public AggregatorUnholdDtoCommandValidator()
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
            .SetValidator(new AggregatorUnholdDetailsDtoCommandValidator());

        RuleFor(x => x.MerchantInfo)
            .SetValidator(new AggregatorMerchantInfoDtoCommandValidator());

        RuleFor(x => x.CardInfo)
            .SetValidator(new AggregatorCardInfoDtoCommandValidator());

        RuleForEach(x => x.Extensions)
            .SetValidator(new AggregatorExtensionDtoCommandValidator())
            .When(x => x.Extensions != null);
    }
}