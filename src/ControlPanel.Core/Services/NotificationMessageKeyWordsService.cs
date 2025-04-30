using ControlPanel.Core.DTOs;
using ControlPanel.Core.Services.Contracts;
using ControlPanel.DataAccess.Abstractions;
using ControlPanel.DataAccess.Entities;

namespace ControlPanel.Core.Services;

/// <inheritdoc/>
public class NotificationMessageKeyWordsService(IControlPanelUnitOfWork controlPanelUnitOfWork) : INotificationMessageKeyWordsService
{
    /// <inheritdoc/>
    public async Task<List<NotificationMessageKeyWord>> GetKeyWordsAsync(CancellationToken cancellationToken)
    {
        var keywords = await controlPanelUnitOfWork.NotificationMessageKeyWords.GetAllAsync(cancellationToken);
        
        return keywords.ToList();
    }

    /// <inheritdoc/>
    public async Task UpdateDescriptionAsync(UpdateNotificationMessageKeyWordsDescriptionDto dto, CancellationToken cancellationToken)
    {
        var entity = await controlPanelUnitOfWork.NotificationMessageKeyWords.FindAsync(x=>x.Id == dto.Id, cancellationToken);
        if (entity == null)
            return;

        entity.Description = dto.Description;
        await controlPanelUnitOfWork.SaveChangesAsync(cancellationToken);
    }
}