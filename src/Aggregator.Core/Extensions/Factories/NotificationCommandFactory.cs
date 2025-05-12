using Aggregator.Core.Commands;
using Aggregator.Core.Extensions.Factories.Abstractions;
using Aggregator.DTOs.Abstractions;
using Aggregator.DTOs.AcctBalChange;
using Aggregator.DTOs.AcqFinAuth;
using Aggregator.DTOs.AcsOtp;
using Aggregator.DTOs.CardStatusChange;
using Aggregator.DTOs.IssFinAuth;
using Aggregator.DTOs.OwiUserAction;
using Aggregator.DTOs.PinChange;
using Aggregator.DTOs.TokenStausChange;
using Aggregator.DTOs.Unhold;
using MediatR;

namespace Aggregator.Core.Extensions.Factories;

/// <summary>
/// Фабрика команд уведомлений.
/// </summary>
/// <remarks>
/// Данный класс реализует интерфейс <see cref="INotificationCommandFactory"/> и предоставляет
/// возможность создания команды уведомлений на основе типа переданного списка уведомлений.
/// </remarks>
public class NotificationCommandFactory : INotificationCommandFactory
{
    /// <summary>
    /// Создаёт команду уведомлений на основе переданного списка уведомлений.
    /// </summary>
    /// <param name="notifications">
    /// Список уведомлений, для которых необходимо создать команду. Все уведомления должны быть одного типа.
    /// </param>
    /// <returns>
    /// Команда, реализующая <see cref="IRequest{TResponse}"/>, которая при выполнении возвращает список идентификаторов.
    /// </returns>
    /// <exception cref="NotSupportedException">
    /// Выбрасывается, если тип уведомления не поддерживается.
    /// </exception>
    public IRequest<List<long>> CreateCommand(List<NotificationAggregatorBaseDto> notifications)
    {
        if (notifications == null || notifications.Count == 0)
            throw new ArgumentException("Notifications list must contain at least one element.", nameof(notifications));

        var first = notifications.First();
        return first switch
        {
            AggregatorIssFinAuthDto _ => Cast<AggregatorIssFinAuthDto>(),
            AggregatorAcqFinAuthDto _ => Cast<AggregatorAcqFinAuthDto>(),
            AggregatorCardStatusChangeDto _ => Cast<AggregatorCardStatusChangeDto>(),
            AggregatorPinChangeDto _ => Cast<AggregatorPinChangeDto>(),
            AggregatorUnholdDto _ => Cast<AggregatorUnholdDto>(),
            AggregatorOwiUserActionDto _ => Cast<AggregatorOwiUserActionDto>(),
            AggregatorAcctBalChangeDto _ => Cast<AggregatorAcctBalChangeDto>(),
            AggregatorTokenStatusChangeDto _ => Cast<AggregatorTokenStatusChangeDto>(),
            AggregatorOtpDto _ => Cast<AggregatorOtpDto>(),
            _ => throw new NotSupportedException($"Тип уведомления {first.GetType().Name} не поддерживается.")
        };

        ProcessNotificationCommand<T> Cast<T>() where T : NotificationAggregatorBaseDto =>
            new(notifications.Cast<T>().ToList());
    }
}