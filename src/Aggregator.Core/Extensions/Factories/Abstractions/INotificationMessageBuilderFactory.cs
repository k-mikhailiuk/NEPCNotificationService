using Aggregator.Core.Services.Abstractions;
using Aggregator.DataAccess.Entities.Abstract;

namespace Aggregator.Core.Extensions.Factories.Abstractions;

/// <summary>
/// Фабрика построителей сообщений уведомлений.
/// </summary>
/// <remarks>
/// Интерфейс предназначен для создания экземпляров построителей сообщений уведомлений,
/// соответствующих определённому типу уведомления.
/// </remarks>
public interface INotificationMessageBuilderFactory
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
    INotificationMessageBuilder<INotification> CreateNotificationMessageBuilder(Type notificationType);
}