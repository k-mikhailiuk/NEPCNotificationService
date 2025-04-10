using ControlPanel.DataAccess.Abstractions.Repositories;
using ControlPanel.DataAccess.Entities;

namespace ControlPanel.DataAccess.Repositories;

public class NotificationMessageTextDirectoriesRepository(ControlPanelDbContext context)
    : Repository<NotificationMessageTextDirectory>(context),
        INotificationMessageTextDirectoriesRepository;