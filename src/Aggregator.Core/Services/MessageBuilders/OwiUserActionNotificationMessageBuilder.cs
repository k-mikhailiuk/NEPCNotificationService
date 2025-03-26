using Aggregator.Core.Services.Abstractions;
using Aggregator.DataAccess.Entities;
using Aggregator.DataAccess.Entities.Enum;
using Aggregator.DataAccess.Entities.OwiUserAction;
using Aggregator.Repositories.Abstractions;
using ControlPanel.DataAccess.Entites.Enum;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OptionsConfiguration;

namespace Aggregator.Core.Services.MessageBuilders;

public class OwiUserActionNotificationMessageBuilder : INotificationMessageBuilder<OwiUserAction>
{
    private readonly NotificationMessageOptions _notificationMessageOptions;
    private readonly IServiceProvider _serviceProvider;
    private readonly IKeyWordBuilder<OwiUserAction> _keyWordBuilder;

    public OwiUserActionNotificationMessageBuilder(IOptions<NotificationMessageOptions> notificationMessageOptions,
        IServiceProvider serviceProvider, IKeyWordBuilder<OwiUserAction> keyWordBuilder)
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
            await unitOfWork.OwiUserAction.GetListByIdsRawSqlAsync(notificationIds,
                cancellationToken, 
                x => x.Details,
                x=>x.CardInfo);

        var messageText = await unitOfWork.NotificationMessageTextDirectories.FindAsync(
            x => x.NotificationType == NotificationMessageType.OwiUserAction, cancellationToken);

        if (messageText == null)
            throw new NullReferenceException();

        if (!messageText.IsNeedSend)
            return list;

        list.AddRange(from message in messages
            where messageText.IsNeedSend
            select new NotificationMessage
            {
                Title = _notificationMessageOptions.Title, Status = NotificationMessageStatus.New,
                Message = _keyWordBuilder.BuildKeyWordsAsync(messageText.MessageTextRu, message)
            });

        return list;
    }
}