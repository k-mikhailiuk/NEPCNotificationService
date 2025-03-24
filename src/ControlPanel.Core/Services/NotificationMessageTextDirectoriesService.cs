using ControlPanel.Core.DTOs;
using ControlPanel.Core.Services.Contracts;
using ControlPanel.DataAccess.Abstractions;
using ControlPanel.DataAccess.Entites;

namespace ControlPanel.Core.Services;

public class NotificationMessageTextDirectoriesService : INotificationMessageTextDirectoriesService
{
    private readonly IUnitOfWork _unitOfWork;

    public NotificationMessageTextDirectoriesService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<List<NotificationMessageTextDirectory>> GetNotificationsTextAsync(CancellationToken cancellationToken)
    {
        var keywords = await _unitOfWork.NotificationMessageTextDirectories.GetAllAsync(cancellationToken);
        
        return keywords.ToList();
    }

    public async Task UpdateMessageTextsAsync(UpdateNotificationMessageDirectoriesTextDto dto, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.NotificationMessageTextDirectories.FindAsync(x=>x.Id == dto.Id, cancellationToken);
        if (entity == null)
            return;

        entity.MessageTextRu = dto.MessageTextRu;
        entity.MessageTextEn = dto.MessageTextEn;
        entity.MessageTextKg = dto.MessageTextKg;
        entity.IsNeedSend = dto.IsNeedSend;
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}