using Aggregator.DataAccess;
using Aggregator.Repositories.Abstractions.Repositories;
using ControlPanel.DataAccess.Entites;
using ControlPanel.DataAccess.Entities;

namespace Aggregator.Repositories.Repositories;

public class NotificationMessageTextDirectoriesRepository : Repository<NotificationMessageTextDirectory>,
    INotificationMessageTextDirectoriesRepository
{
    public NotificationMessageTextDirectoriesRepository(AggregatorDbContext context) : base(context)
    {
    }
}