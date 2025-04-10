using ControlPanel.DataAccess.Abstractions.Repositories;
using ControlPanel.DataAccess.Entities;

namespace ControlPanel.DataAccess.Repositories;

public class NotificationMessageKeyWordsRepository(ControlPanelDbContext context)
    : Repository<NotificationMessageKeyWord>(context),
        INotificationMessageKeyWordsRepository;