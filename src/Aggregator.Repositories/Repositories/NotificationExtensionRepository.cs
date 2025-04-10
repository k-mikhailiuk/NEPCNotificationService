using Aggregator.DataAccess;
using Aggregator.DataAccess.Entities;
using Aggregator.Repositories.Abstractions.Repositories;

namespace Aggregator.Repositories.Repositories;

public class NotificationExtensionRepository(AggregatorDbContext context)
    : Repository<NotificationExtension>(context), INotificationExtensionRepository;