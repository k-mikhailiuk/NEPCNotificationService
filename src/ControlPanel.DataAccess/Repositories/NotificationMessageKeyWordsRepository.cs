using ControlPanel.DataAccess.Abstractions.Repositories;
using ControlPanel.DataAccess.Entites;

namespace ControlPanel.DataAccess.Repositories;

public class NotificationMessageKeyWordsRepository : Repository<NotificationMessageKeyWord>,
    INotificationMessageKeyWordsRepository
{
    public NotificationMessageKeyWordsRepository(ControlPanelDbContext context) : base(context)
    {
    }
}