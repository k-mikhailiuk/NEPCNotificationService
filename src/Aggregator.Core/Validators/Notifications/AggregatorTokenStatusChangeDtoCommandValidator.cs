using Aggregator.Core.Validators.Details;
using Aggregator.DTOs.TokenStausChange;
using FluentValidation;

namespace Aggregator.Core.Validators.Notifications;

/// <summary>
/// Валидатор для DTO объекта изменения статуса токена.
/// </summary>
/// <remarks>
/// Этот валидатор проверяет свойства объекта <see cref="AggregatorTokenStatusChangeDto"/>, 
/// включая валидацию вложенных объектов Details, CardInfo и коллекции Extensions.
/// </remarks>
public class AggregatorTokenStatusChangeDtoCommandValidator : AbstractValidator<AggregatorTokenStatusChangeDto>
{
    /// <summary>
    /// Инициализирует новый экземпляр <see cref="AggregatorTokenStatusChangeDtoCommandValidator"/> и задаёт правила валидации.
    /// </summary>
    public AggregatorTokenStatusChangeDtoCommandValidator()
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
            .SetValidator(new AggregatorTokenStatusChangeDetailsDtoCommandValidator());

        RuleFor(x => x.CardInfo)
            .SetValidator(new AggregatorCardInfoDtoCommandValidator());

        RuleForEach(x => x.Extensions)
            .SetValidator(new AggregatorExtensionDtoCommandValidator())
            .When(x => x.Extensions != null);
    }
}