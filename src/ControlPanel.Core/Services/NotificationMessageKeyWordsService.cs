using ControlPanel.Core.DTOs;
using ControlPanel.Core.Services.Contracts;
using ControlPanel.DataAccess.Abstractions;
using ControlPanel.DataAccess.Entites;

namespace ControlPanel.Core.Services;

public class NotificationMessageKeyWordsService : INotificationMessageKeyWordsService
{
    private readonly IUnitOfWork _unitOfWork;

    public NotificationMessageKeyWordsService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<NotificationMessageKeyWord>> GetKeyWordsAsync(CancellationToken cancellationToken)
    {
        var keywords = await _unitOfWork.NotificationMessageKeyWords.GetAllAsync(cancellationToken);
        
        return keywords.ToList();
    }

    public async Task UpdateDescriptionAsync(UpdateNotificationMessageKeyWordsDescriptionDto dto, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.NotificationMessageKeyWords.FindAsync(x=>x.Id == dto.Id, cancellationToken);
        if (entity == null)
            return;

        entity.Description = dto.Description;
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}