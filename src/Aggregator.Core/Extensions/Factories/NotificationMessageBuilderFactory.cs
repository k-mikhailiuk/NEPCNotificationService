using Aggregator.Core.Extensions.Factories.Abstractions;
using Aggregator.Core.Services.Abstractions;
using Aggregator.DataAccess.Entities.Abstract;
using Aggregator.DataAccess.Entities.AcctBalChange;
using Aggregator.DataAccess.Entities.AcqFinAuth;
using Aggregator.DataAccess.Entities.CardStatusChange;
using Aggregator.DataAccess.Entities.IssFinAuth;
using Aggregator.DataAccess.Entities.OwiUserAction;
using Aggregator.DataAccess.Entities.PinChange;
using Aggregator.DataAccess.Entities.TokenChangeStatus;
using Aggregator.DataAccess.Entities.Unhold;
using Microsoft.Extensions.DependencyInjection;

namespace Aggregator.Core.Extensions.Factories;

public class NotificationMessageBuilderFactory : INotificationMessageBuilderFactory
{
    private readonly IServiceProvider _serviceProvider;

    public NotificationMessageBuilderFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public INotificationMessageBuilder<INotification> CreateNotificationMessageBuilder(Type notificationType)
    {
        return notificationType switch
        {
            _ when notificationType == typeof(IssFinAuth) => 
                _serviceProvider.GetRequiredService<INotificationMessageBuilder<IssFinAuth>>(),
            _ when notificationType == typeof(AcqFinAuth) => 
                _serviceProvider.GetRequiredService<INotificationMessageBuilder<AcqFinAuth>>(),
            _ when notificationType == typeof(CardStatusChange) => 
                _serviceProvider.GetRequiredService<INotificationMessageBuilder<CardStatusChange>>(),
            _ when notificationType == typeof(PinChange) => 
                _serviceProvider.GetRequiredService<INotificationMessageBuilder<PinChange>>(),
            _ when notificationType == typeof(OwiUserAction) => 
                _serviceProvider.GetRequiredService<INotificationMessageBuilder<OwiUserAction>>(),
            _ when notificationType == typeof(Unhold) => 
                _serviceProvider.GetRequiredService<INotificationMessageBuilder<Unhold>>(),
            _ when notificationType == typeof(AcctBalChange) => 
                _serviceProvider.GetRequiredService<INotificationMessageBuilder<AcctBalChange>>(),
            _ when notificationType == typeof(TokenStatusChange) => 
                _serviceProvider.GetRequiredService<INotificationMessageBuilder<TokenStatusChange>>(),
           
            _ => throw new NotSupportedException($"Тип уведомления {notificationType.Name} не поддерживается.")
        };
    }
}