using Aggregator.Core.Services.Abstractions;
using Aggregator.DataAccess.Entities;
using Aggregator.DataAccess.Entities.Enum;
using Aggregator.DataAccess.Entities.TokenChangeStatus;
using Microsoft.Extensions.Options;
using OptionsConfiguration;

namespace Aggregator.Core.Services;

public class TokenStatusChangeNotificationMessageBuilder : INotificationMessageBuilder<TokenStatusChange>
{
    private readonly NotificationMessageOptions _notificationMessageOptions;

    public TokenStatusChangeNotificationMessageBuilder(IOptions<NotificationMessageOptions> notificationMessageOptions)
    {
        _notificationMessageOptions = notificationMessageOptions.Value;
    }

    public async Task<List<NotificationMessage>> BuildNotificationAsync(List<long> notificationIds,
        CancellationToken cancellationToken)
    {
        var list = notificationIds.Select(notificationId => new NotificationMessage
                { Title = _notificationMessageOptions.Title, Message = "test", Status = NotificationMessageStatus.New })
            .ToList();

        return list;
    }
}