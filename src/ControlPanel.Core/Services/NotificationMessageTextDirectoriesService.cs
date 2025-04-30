using ControlPanel.Core.DTOs;
using ControlPanel.Core.Services.Contracts;
using ControlPanel.DataAccess.Abstractions;
using ControlPanel.DataAccess.Entities;

namespace ControlPanel.Core.Services;

/// <inheritdoc/>
public class NotificationMessageTextDirectoriesService(IControlPanelUnitOfWork controlPanelUnitOfWork)
    : INotificationMessageTextDirectoriesService
{
    /// <inheritdoc/>
    public async Task<List<NotificationMessageTextDirectory>> GetNotificationsTextAsync(CancellationToken cancellationToken)
    {
        var keywords = await controlPanelUnitOfWork.NotificationMessageTextDirectories.GetAllAsync(cancellationToken);
        
        return keywords.ToList();
    }

    /// <inheritdoc/>
    public async Task UpdateMessageTextsAsync(UpdateNotificationMessageDirectoriesTextDto dto, CancellationToken cancellationToken)
    {
        var entity = await controlPanelUnitOfWork.NotificationMessageTextDirectories.FindAsync(x=>x.Id == dto.Id, cancellationToken);
        if (entity == null)
            return;

        entity.MessageTextRu = dto.MessageTextRu;
        entity.MessageTextEn = dto.MessageTextEn;
        entity.MessageTextKg = dto.MessageTextKg;
        entity.IsNeedSend = dto.IsNeedSend;
        
        await controlPanelUnitOfWork.SaveChangesAsync(cancellationToken);
    }
}