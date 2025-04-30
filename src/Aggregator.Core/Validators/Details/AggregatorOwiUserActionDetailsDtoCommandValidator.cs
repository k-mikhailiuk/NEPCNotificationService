using Aggregator.DTOs.OwiUserAction;
using FluentValidation;

namespace Aggregator.Core.Validators.Details;

/// <summary>
/// Валидатор для команды DTO объекта деталей действий пользователя (OwiUserAction).
/// </summary>
/// <remarks>
/// Устанавливает правила валидации для свойств объекта <see cref="AggregatorOwiUserActionDetailsDto"/>.
/// </remarks>
public class AggregatorOwiUserActionDetailsDtoCommandValidator : AbstractValidator<AggregatorOwiUserActionDetailsDto>
{
    /// <summary>
    /// Инициализирует новый экземпляр <see cref="AggregatorOwiUserActionDetailsDtoCommandValidator"/> и задаёт правила валидации.
    /// </summary>
    public AggregatorOwiUserActionDetailsDtoCommandValidator()
    {
        RuleFor(x => x.TransactionTime)
            .Matches("^[0-9]{14}$")
            .WithMessage("Invalid transaction time.");

        RuleFor(x => x.Login)
            .Length(1, 30)
            .WithMessage("Login must be between 1 and 30 characters.");

        RuleFor(x => x.Action)
            .Length(1, 30)
            .WithMessage("Action must be between 1 and 30 characters.");
    }
}