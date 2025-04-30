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
public class AggregatorAcctBalChangeDtoCommandValidator : AbstractValidator<AggregatorAcctBalChangeDto>
{
    /// <summary>
    /// Инициализирует новый экземпляр <see cref="AggregatorAcctBalChangeDtoCommandValidator"/> и задаёт правила валидации.
    /// </summary>
    public AggregatorAcctBalChangeDtoCommandValidator()
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
            .SetValidator(new AggregatorAcctBalChangeDetailsDtoCommandValidator());

        RuleFor(x => x.CardInfo)
            .SetValidator(new AggregatorCardInfoDtoCommandValidator())
            .When(x => x.CardInfo != null);

        RuleForEach(x => x.AccountsInfo)
            .SetValidator(new AggregatorAccountInfoDtoCommandValidator());

        RuleForEach(x => x.Extensions)
            .SetValidator(new AggregatorExtensionDtoCommandValidator())
            .When(x => x.Extensions != null);
    }
}