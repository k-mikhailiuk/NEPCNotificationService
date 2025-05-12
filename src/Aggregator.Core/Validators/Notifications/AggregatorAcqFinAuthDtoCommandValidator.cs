using Aggregator.Core.Validators.Details;
using Aggregator.DTOs.AcqFinAuth;

namespace Aggregator.Core.Validators.Notifications;

/// <summary>
/// Валидатор для команды DTO объекта авторизации AcqFinAuth.
/// </summary>
/// <remarks>
/// Устанавливает правила валидации для свойств объекта <see cref="AggregatorAcqFinAuthDto"/>,
/// включая валидацию вложенных объектов Details, MerchantInfo и коллекции Extensions.
/// </remarks>
public class AggregatorAcqFinAuthDtoCommandValidator : BaseAggregatorNotificationDtoCommandValidator<AggregatorAcqFinAuthDto>
{
    /// <summary>
    /// Инициализирует новый экземпляр <see cref="AggregatorAcqFinAuthDtoCommandValidator"/> и задаёт правила валидации.
    /// </summary>
    public AggregatorAcqFinAuthDtoCommandValidator()
    {
        RuleFor(x => x.Details)
            .SetValidator(new AggregatorAcqFinAuthDetailsDtoCommandValidator());

        RuleFor(x => x.MerchantInfo)
            .SetValidator(new AggregatorMerchantInfoDtoCommandValidator());
    }
}