using Aggregator.DataAccess;
using Aggregator.DataAccess.Entities;
using Aggregator.Repositories.Abstractions.Repositories;

namespace Aggregator.Repositories.Repositories;

public class NotificationMessageRepository(AggregatorDbContext context)
    : Repository<NotificationMessage>(context), INotificationMessageRepository;