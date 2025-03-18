using Aggregator.Core.Commands;
using Aggregator.Core.Extensions.Factories.Abstractions;
using Aggregator.DTOs.AcctBalChange;
using Aggregator.DTOs.AcqFinAuth;
using Aggregator.DTOs.CardStatusChange;
using Aggregator.DTOs.IssFinAuth;
using Aggregator.DTOs.OwiUserAction;
using Aggregator.DTOs.PinChange;
using Aggregator.DTOs.TokenStausChange;
using Aggregator.DTOs.Unhold;
using MediatR;

namespace Aggregator.Core.Extensions.Factories;

public class NotificationCommandFactory : INotificationCommandFactory
{
    public IRequest<List<long>> CreateCommand(List<object> notifications)
    {
        var notificationType = notifications.First().GetType();
        
        return notificationType switch
        {
            _ when notificationType == typeof(AggregatorIssFinAuthDto) => 
                new ProcessNotificationCommand<AggregatorIssFinAuthDto>(
                    notifications.Cast<AggregatorIssFinAuthDto>().ToList()),
            _ when notificationType == typeof(AggregatorAcqFinAuthDto) => 
                new ProcessNotificationCommand<AggregatorAcqFinAuthDto>(
                    notifications.Cast<AggregatorAcqFinAuthDto>().ToList()),
            _ when notificationType == typeof(AggregatorCardStatusChangeDto) => 
                new ProcessNotificationCommand<AggregatorCardStatusChangeDto>(
                    notifications.Cast<AggregatorCardStatusChangeDto>().ToList()),
            _ when notificationType == typeof(AggregatorPinChangeDto) => 
                new ProcessNotificationCommand<AggregatorPinChangeDto>(
                    notifications.Cast<AggregatorPinChangeDto>().ToList()),
            _ when notificationType == typeof(AggregatorUnholdDto) => 
                new ProcessNotificationCommand<AggregatorUnholdDto>(
                    notifications.Cast<AggregatorUnholdDto>().ToList()),
            _ when notificationType == typeof(AggregatorOwiUserActionDto) => 
                new ProcessNotificationCommand<AggregatorOwiUserActionDto>(
                    notifications.Cast<AggregatorOwiUserActionDto>().ToList()),
            _ when notificationType == typeof(AggregatorAcctBalChangeDto) => 
                new ProcessNotificationCommand<AggregatorAcctBalChangeDto>(
                    notifications.Cast<AggregatorAcctBalChangeDto>().ToList()),
            _ when notificationType == typeof(AggregatorTokenStatusChangeDto) => 
                new ProcessNotificationCommand<AggregatorTokenStatusChangeDto>(
                    notifications.Cast<AggregatorTokenStatusChangeDto>().ToList()),
            _ => throw new NotSupportedException($"Тип уведомления {notificationType.Name} не поддерживается.")
        };
    }
}