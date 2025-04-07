using ControlPanel.Core.DTOs;
using ControlPanel.DataAccess.Entites;
using ControlPanel.DataAccess.Entities;

namespace ControlPanel.Core.Services.Contracts;

public interface INotificationMessageKeyWordsService
{
    Task<List<NotificationMessageKeyWord>> GetKeyWordsAsync(CancellationToken cancellationToken);
    
    Task UpdateDescriptionAsync(UpdateNotificationMessageKeyWordsDescriptionDto dto, CancellationToken cancellationToken);
}