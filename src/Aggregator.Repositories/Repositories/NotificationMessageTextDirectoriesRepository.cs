using Aggregator.DataAccess;
using Aggregator.Repositories.Abstractions.Repositories;
using ControlPanel.DataAccess.Entites;

namespace Aggregator.Repositories.Repositories;

public class NotificationMessageTextDirectoriesRepository : Repository<NotificationMessageTextDirectory>,
    INotificationMessageTextDirectoriesRepository
{
    public NotificationMessageTextDirectoriesRepository(AggregatorDbContext context) : base(context)
    {
    }
}