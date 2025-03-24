using Aggregator.Core.Services.Abstractions;
using Aggregator.DataAccess.Entities;
using Aggregator.DataAccess.Entities.AcctBalChange;
using Aggregator.DataAccess.Entities.Enum;
using Aggregator.Repositories.Abstractions;
using Microsoft.Extensions.Options;
using OptionsConfiguration;

namespace Aggregator.Core.Services;

public class AcctBalChangeNotificationMessageBuilder : INotificationMessageBuilder<AcctBalChange>
{
    private readonly NotificationMessageOptions _notificationMessageOptions;
    private readonly IUnitOfWork _unitOfWork;

    public AcctBalChangeNotificationMessageBuilder(IOptions<NotificationMessageOptions> notificationMessageOptions, IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _notificationMessageOptions = notificationMessageOptions.Value;
    }

    public async Task<List<NotificationMessage>> BuildNotificationAsync(List<long> notificationIds,
        CancellationToken cancellationToken)
    {
        var list = notificationIds.Select(notificationId => new NotificationMessage
            { Title = _notificationMessageOptions.Title, Message = "test", Status = NotificationMessageStatus.New }).ToList();
        
        var messages = await _unitOfWork.AcctBalChange.GetListByIdsRawSqlAsync(notificationIds, cancellationToken);

        foreach (var message in messages)
        {
            var notificationMessage = new NotificationMessage
            {
                Title = _notificationMessageOptions.Title,
                Status = NotificationMessageStatus.New,
                Message = await _unitOfWork.NotificationMessageTextDirectories.FindAsync(x=>x.OperationType == message.Details.TransType)
            };
        }
        
        return list;
    }
}