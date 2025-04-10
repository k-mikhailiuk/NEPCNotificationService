using Aggregator.DataAccess;
using Aggregator.Repositories.Abstractions.Repositories;
using ControlPanel.DataAccess.Entities;

namespace Aggregator.Repositories.Repositories;

public class NotificationMessageTextDirectoriesRepository(AggregatorDbContext context)
    : Repository<NotificationMessageTextDirectory>(context),
        INotificationMessageTextDirectoriesRepository;