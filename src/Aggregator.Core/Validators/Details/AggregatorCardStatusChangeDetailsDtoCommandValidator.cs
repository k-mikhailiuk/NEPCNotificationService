using Aggregator.DTOs.CardStatusChange;
using FluentValidation;

namespace Aggregator.Core.Validators.Details;

/// <summary>
/// Валидатор для команды DTO объекта деталей изменения статуса карты.
/// </summary>
/// <remarks>
/// Устанавливает правила валидации для свойств объекта AggregatorCardStatusChangeDetailsDto.
/// </remarks>
public class
    AggregatorCardStatusChangeDetailsDtoCommandValidator : AbstractValidator<AggregatorCardStatusChangeDetailsDto>
{
    /// <summary>
    /// Инициализирует новый экземпляр <see cref="AggregatorCardStatusChangeDetailsDtoCommandValidator"/> и задаёт правила валидации.
    /// </summary>
    public AggregatorCardStatusChangeDetailsDtoCommandValidator()
    {
        RuleFor(x => x.ExpDate)
            .Matches("^([0-9]{2})(0[1-9]|1[0-2])$")
            .WithMessage("Id is invalid");

        RuleFor(x => x.OldStatus)
            .NotEmpty()
            .InclusiveBetween(-99, 99)
            .WithMessage("Old status should not be null and between -99 and 99");

        RuleFor(x => x.NewStatus)
            .NotEmpty()
            .InclusiveBetween(-99, 99)
            .WithMessage("New status should not be null and between -99 and 99");

        RuleFor(x => x.ChangeDate)
            .Matches("^[0-9]{14}$")
            .WithMessage("Change date is invalid");

        RuleFor(x => x.Service)
            .MinimumLength(1)
            .MaximumLength(30)
            .When(x => !string.IsNullOrWhiteSpace(x.Service))
            .WithMessage("Service should have length between 1 and 30 characters");

        RuleFor(x => x.UserName)
            .MinimumLength(1)
            .MaximumLength(30)
            .When(x => !string.IsNullOrWhiteSpace(x.UserName))
            .WithMessage("UserName should have length between 1 and 30 characters");

        RuleFor(x => x.Note)
            .MinimumLength(1)
            .MaximumLength(400)
            .When(x => !string.IsNullOrWhiteSpace(x.Note))
            .WithMessage("Note should have length between 1 and 400 characters");

        RuleFor(x => x.CardIdentifier)
            .NotEmpty()
            .WithMessage("Card identifier should not be empty");
    }
}