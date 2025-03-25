using Aggregator.Core.Services.Abstractions;
using Aggregator.DataAccess.Entities;
using Aggregator.DataAccess.Entities.Enum;
using Aggregator.DataAccess.Entities.Unhold;
using Aggregator.Repositories.Abstractions;
using ControlPanel.DataAccess.Entites.Enum;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OptionsConfiguration;

namespace Aggregator.Core.Services;

public class UnholdNotificationMessageBuilder : INotificationMessageBuilder<Unhold>
{
    private readonly NotificationMessageOptions _notificationMessageOptions;
    private readonly IServiceProvider _serviceProvider;

    public UnholdNotificationMessageBuilder(IOptions<NotificationMessageOptions> notificationMessageOptions, IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        _notificationMessageOptions = notificationMessageOptions.Value;
    }

    public async Task<List<NotificationMessage>> BuildNotificationAsync(List<long> notificationIds,
        CancellationToken cancellationToken)
    {
        var list = new List<NotificationMessage>();
        
        using var scope = _serviceProvider.CreateScope();
        
        using var unitOfWork = scope.ServiceProvider.GetService<IUnitOfWork>();
        
        if(unitOfWork == null)
            throw new ArgumentNullException(nameof(unitOfWork));

        var messages = await unitOfWork.Unhold.GetListByIdsRawSqlAsync(notificationIds, cancellationToken, x=>x.Details);

        foreach (var message in messages)
        {
            var messageText = await unitOfWork.NotificationMessageTextDirectories.FindAsync(
                x => x.NotificationType == NotificationMessageType.IssFinAuth &&
                     (int)x.OperationType! == message.Details.TransType, cancellationToken);

            if (messageText == null)
                continue;

            if (!messageText.IsNeedSend)
                continue;

            var notificationMessage = new NotificationMessage
            {
                Title = _notificationMessageOptions.Title,
                Status = NotificationMessageStatus.New,
                Message = messageText.MessageTextRu != null ? messageText.MessageTextRu : null,
            };

            list.Add(notificationMessage);
        }

        return list;
    }
}