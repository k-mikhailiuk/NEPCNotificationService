using ControlPanel.Core.DTOs;
using ControlPanel.DataAccess.Entites;

namespace ControlPanel.Core.Services.Contracts;

public interface INotificationMessageTextDirectoriesService
{
    Task<List<NotificationMessageTextDirectory>> GetNotificationsTextAsync(CancellationToken cancellationToken);
    
    Task UpdateMessageTextsAsync(UpdateNotificationMessageDirectoriesTextDto dto, CancellationToken cancellationToken);
}