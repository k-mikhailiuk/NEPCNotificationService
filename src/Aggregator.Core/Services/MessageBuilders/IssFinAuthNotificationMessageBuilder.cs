using Aggregator.Core.Services.Abstractions;
using Aggregator.DataAccess.Entities;
using Aggregator.DataAccess.Entities.Enum;
using Aggregator.DataAccess.Entities.IssFinAuth;
using Aggregator.Repositories.Abstractions;
using ControlPanel.DataAccess.Entites.Enum;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OptionsConfiguration;

namespace Aggregator.Core.Services.MessageBuilders;

public class IssFinAuthNotificationMessageBuilder : INotificationMessageBuilder<IssFinAuth>
{
    private readonly NotificationMessageOptions _notificationMessageOptions;
    private readonly IServiceProvider _serviceProvider;
    private readonly IKeyWordBuilder<IssFinAuth> _keyWordBuilder;

    public IssFinAuthNotificationMessageBuilder(IOptions<NotificationMessageOptions> notificationMessageOptions,
        IServiceProvider serviceProvider, IKeyWordBuilder<IssFinAuth> keyWordBuilder)
    {
        _serviceProvider = serviceProvider;
        _keyWordBuilder = keyWordBuilder;
        _notificationMessageOptions = notificationMessageOptions.Value;
    }

    public async Task<List<NotificationMessage>> BuildNotificationAsync(List<long> notificationIds,
        CancellationToken cancellationToken)
    {
        var list = new List<NotificationMessage>();

        using var scope = _serviceProvider.CreateScope();

        using var unitOfWork = scope.ServiceProvider.GetService<IUnitOfWork>();

        if (unitOfWork == null)
            throw new ArgumentNullException(nameof(unitOfWork));

        var messages =
            await unitOfWork.IssFinAuth.GetListByIdsRawSqlAsync(
                notificationIds,
                cancellationToken,
                x => x.Details,
                x => x.CardInfo,
                x => x.MerchantInfo,
                x => x.CardInfo.Limits);
        
        foreach (var message in messages)
        {
            foreach (var limit in message.CardInfo.Limits)
            {
                limit.Limit = await unitOfWork.Limit.GetByIdAsync(limit.LimitId, cancellationToken) ?? throw new InvalidOperationException();
            }
            
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
                Message = _keyWordBuilder.BuildKeyWordsAsync(messageText.MessageTextRu, message),
            };

            list.Add(notificationMessage);
        }

        return list;
    }
}