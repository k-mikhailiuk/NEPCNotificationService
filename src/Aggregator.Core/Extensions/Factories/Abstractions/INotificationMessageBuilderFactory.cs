using Aggregator.Core.Services.Abstractions;
using Aggregator.DataAccess.Entities.Abstract;

namespace Aggregator.Core.Extensions.Factories.Abstractions;

public interface INotificationMessageBuilderFactory
{
    INotificationMessageBuilder<INotification> CreateNotificationMessageBuilder(Type notificationType);
}