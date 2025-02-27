using Aggregator.DataAccess;
using Aggregator.DataAccess.Entities;
using Aggregator.Repositories.Abstractions.Repositories;

namespace Aggregator.Repositories.Repositories;

public class NotificationExtensionRepository : Repository<NotificationExtension>, INotificationExtensionRepository
{
    public NotificationExtensionRepository(AggregatorDbContext context) : base(context)
    {
    }
}