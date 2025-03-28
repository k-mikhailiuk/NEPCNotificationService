using Aggregator.DataAccess.Entities;
using NotificationService.DataAccess.Abstractions;

namespace NotificationService.DataAccess;

public class NotificationMessagesRepository : Repository<NotificationMessage>, INotificationMessagesRepository
{
    public NotificationMessagesRepository(NotificationServiceDbContext context) : base(context)
    {
    }
}