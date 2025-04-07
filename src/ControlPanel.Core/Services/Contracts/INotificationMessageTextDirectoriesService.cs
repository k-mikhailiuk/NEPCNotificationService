using ControlPanel.Core.DTOs;
using ControlPanel.DataAccess.Entites;
using ControlPanel.DataAccess.Entities;

namespace ControlPanel.Core.Services.Contracts;

public interface INotificationMessageTextDirectoriesService
{
    Task<List<NotificationMessageTextDirectory>> GetNotificationsTextAsync(CancellationToken cancellationToken);
    
    Task UpdateMessageTextsAsync(UpdateNotificationMessageDirectoriesTextDto dto, CancellationToken cancellationToken);
}