using Aggregator.Core.Services.Abstractions;
using Aggregator.DataAccess;
using Aggregator.DataAccess.Abstractions;
using Aggregator.DataAccess.Entities;
using Aggregator.DataAccess.Entities.Enum;
using Aggregator.DataAccess.Entities.TokenChangeStatus;
using Common.Enums;
using ControlPanel.DataAccess.Entities.Enum;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OptionsConfiguration;

namespace Aggregator.Core.Services.MessageBuilders;

/// <summary>
/// Построитель уведомлений для операций изменения статуса токена.
/// </summary>
/// <remarks>
/// Реализует интерфейс INotificationMessageBuilder для формирования уведомлений по операциям TokenStatusChange.
/// </remarks>
public class TokenStatusChangeNotificationMessageBuilder(
    IOptions<NotificationMessageOptions> notificationMessageOptions,
    IServiceProvider serviceProvider,
    IKeyWordBuilder<TokenStatusChange> keyWordBuilder,
    ICustomerIdSelector customerIdSelector)
    : INotificationMessageBuilder<TokenStatusChange>
{
    private readonly NotificationMessageOptions _notificationMessageOptions = notificationMessageOptions.Value;

    /// <summary>
    /// Асинхронно формирует список уведомлений по заданным идентификаторам.
    /// </summary>
    /// <param name="notificationIds">Список идентификаторов уведомлений.</param>
    /// <param name="cancellationToken">Токен для отмены операции.</param>
    /// <returns>Список сформированных уведомлений.</returns>
    /// <exception cref="ArgumentNullException">Выбрасывается, если unitOfWork или context равны null.</exception>
    /// <exception cref="NullReferenceException">Выбрасывается, если текст уведомления не найден.</exception>
    public async Task<List<NotificationMessage>> BuildNotificationAsync(List<long> notificationIds,
        CancellationToken cancellationToken)
    {
        var list = new List<NotificationMessage>();

        using var scope = serviceProvider.CreateScope();

        using var unitOfWork = scope.ServiceProvider.GetService<IUnitOfWork>();

        await using var context = scope.ServiceProvider.GetRequiredService<AggregatorDbContext>();
        
        var messages =
            await unitOfWork.TokenStatusChange.GetByIdsWithIncludesAsync(notificationIds, cancellationToken,
                x => x.Details);

        foreach (var message in messages)
        {
            var messageText = await unitOfWork.NotificationMessageTextDirectories.FindAsync(
                x => x.NotificationType == NotificationMessageType.TokenStatusChange, cancellationToken);

            if (messageText == null)
                throw new NullReferenceException();

            if (!messageText.IsNeedSend)
                return list;

            var customerId = await customerIdSelector.GetCustomerIdAsync(message.Details.CardIdentifier.CardIdentifierValue, context,
                cancellationToken);

            if (customerId == null)
                continue;
            
            var languageSelector = scope.ServiceProvider.GetRequiredService<ILanguageSelector>();
            
            var languageId = await languageSelector.GetLanguageIdAsync(customerId.Value, context, cancellationToken);
            
            var language = languageId.HasValue ? (Language)languageId.Value : Language.Russian;
            
            var localizeMessage = language switch
            {
                Language.Russian => messageText.MessageTextRu,
                Language.English => messageText.MessageTextEn,
                Language.Kyrgyz => messageText.MessageTextKg,
                _ => messageText.MessageTextRu
            };

            var notificationMessage = new NotificationMessage
            {
                Title = _notificationMessageOptions.Title,
                Status = NotificationMessageStatus.New,
                Message = await keyWordBuilder.BuildKeyWordsAsync(localizeMessage, message, language),
                CustomerId = customerId.Value,
            };

            list.Add(notificationMessage);
        }

        return list;
    }
}