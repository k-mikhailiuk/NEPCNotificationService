using Aggregator.DataAccess;
using Aggregator.DataAccess.Entities;
using Aggregator.Repositories.Abstractions;

namespace Aggregator.Repositories.Repositories;

public class NotificationMessageRepository : Repository<NotificationMessage>, INotificationMessageRepository
{
    public NotificationMessageRepository(AggregatorDbContext context) : base(context)
    {
    }
}