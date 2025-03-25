using Aggregator.DataAccess;
using Aggregator.Repositories.Abstractions.Repositories;
using ControlPanel.DataAccess.Entites;

namespace Aggregator.Repositories.Repositories;

public class NotificationMessageKeyWordsRepository  : Repository<NotificationMessageKeyWord>,
    INotificationMessageKeyWordsRepository
{
    public NotificationMessageKeyWordsRepository(AggregatorDbContext context) : base(context)
    {
    }
}