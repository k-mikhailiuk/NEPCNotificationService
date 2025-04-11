using Aggregator.Core.Extensions.Factories.Abstractions;
using Aggregator.Core.Services.Abstractions;
using Aggregator.DataAccess.Entities.Abstract;
using Aggregator.DataAccess.Entities.AcctBalChange;
using Aggregator.DataAccess.Entities.AcqFinAuth;
using Aggregator.DataAccess.Entities.AcsOtp;
using Aggregator.DataAccess.Entities.CardStatusChange;
using Aggregator.DataAccess.Entities.IssFinAuth;
using Aggregator.DataAccess.Entities.OwiUserAction;
using Aggregator.DataAccess.Entities.PinChange;
using Aggregator.DataAccess.Entities.TokenChangeStatus;
using Aggregator.DataAccess.Entities.Unhold;
using Microsoft.Extensions.DependencyInjection;

namespace Aggregator.Core.Extensions.Factories;

/// <summary>
/// Фабрика построителей сообщений уведомлений.
/// </summary>
/// <remarks>
/// Данный класс реализует интерфейс <see cref="INotificationMessageBuilderFactory"/> и предоставляет
/// возможность создания построителя сообщений уведомлений для заданного типа уведомления.
/// </remarks>
public class NotificationMessageBuilderFactory(IServiceProvider serviceProvider) : INotificationMessageBuilderFactory
{
    /// <summary>
    /// Создаёт построитель сообщений уведомлений для указанного типа уведомления.
    /// </summary>
    /// <param name="notificationType">
    /// Тип уведомления, для которого необходимо создать построитель сообщений.
    /// </param>
    /// <returns>
    /// Построитель сообщений уведомлений, реализующий <see cref="INotificationMessageBuilder{INotification}"/>.
    /// </returns>
    /// <exception cref="NotSupportedException">
    /// Выбрасывается, если тип уведомления не поддерживается.
    /// </exception>
    public INotificationMessageBuilder<INotification> CreateNotificationMessageBuilder(Type notificationType)
    {
        return notificationType switch
        {
            _ when notificationType == typeof(IssFinAuth) => 
                serviceProvider.GetRequiredService<INotificationMessageBuilder<IssFinAuth>>(),
            _ when notificationType == typeof(AcqFinAuth) => 
                serviceProvider.GetRequiredService<INotificationMessageBuilder<AcqFinAuth>>(),
            _ when notificationType == typeof(CardStatusChange) => 
                serviceProvider.GetRequiredService<INotificationMessageBuilder<CardStatusChange>>(),
            _ when notificationType == typeof(PinChange) => 
                serviceProvider.GetRequiredService<INotificationMessageBuilder<PinChange>>(),
            _ when notificationType == typeof(OwiUserAction) => 
                serviceProvider.GetRequiredService<INotificationMessageBuilder<OwiUserAction>>(),
            _ when notificationType == typeof(Unhold) => 
                serviceProvider.GetRequiredService<INotificationMessageBuilder<Unhold>>(),
            _ when notificationType == typeof(AcctBalChange) => 
                serviceProvider.GetRequiredService<INotificationMessageBuilder<AcctBalChange>>(),
            _ when notificationType == typeof(TokenStatusChange) => 
                serviceProvider.GetRequiredService<INotificationMessageBuilder<TokenStatusChange>>(),
            _ when notificationType == typeof(AcsOtp) => 
                serviceProvider.GetRequiredService<INotificationMessageBuilder<AcsOtp>>(),
           
            _ => throw new NotSupportedException($"Тип уведомления {notificationType.Name} не поддерживается.")
        };
    }
}