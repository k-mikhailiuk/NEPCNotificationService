using ControlPanel.DataAccess.Abstractions.Repositories;
using ControlPanel.DataAccess.Entites;

namespace ControlPanel.DataAccess.Repositories;

public class NotificationMessageTextDirectoriesRepository : Repository<NotificationMessageTextDirectory>,
    INotificationMessageTextDirectoriesRepository
{
    public NotificationMessageTextDirectoriesRepository(ControlPanelDbContext context) : base(context)
    {
    }
}