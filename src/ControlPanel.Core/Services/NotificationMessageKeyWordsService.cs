using ControlPanel.Core.DTOs;
using ControlPanel.Core.Services.Contracts;
using ControlPanel.DataAccess.Abstractions;
using ControlPanel.DataAccess.Entities;

namespace ControlPanel.Core.Services;

public class NotificationMessageKeyWordsService(IUnitOfWork unitOfWork) : INotificationMessageKeyWordsService
{
    public async Task<List<NotificationMessageKeyWord>> GetKeyWordsAsync(CancellationToken cancellationToken)
    {
        var keywords = await unitOfWork.NotificationMessageKeyWords.GetAllAsync(cancellationToken);
        
        return keywords.ToList();
    }

    public async Task UpdateDescriptionAsync(UpdateNotificationMessageKeyWordsDescriptionDto dto, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.NotificationMessageKeyWords.FindAsync(x=>x.Id == dto.Id, cancellationToken);
        if (entity == null)
            return;

        entity.Description = dto.Description;
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}