using System.Data;
using Aggregator.Core.Services.Abstractions;
using Aggregator.DataAccess;
using Aggregator.DataAccess.Abstractions;
using Aggregator.DataAccess.Entities;
using Aggregator.DataAccess.Entities.AcqFinAuth;
using Aggregator.DataAccess.Entities.Enum;
using Common.Enums;
using ControlPanel.DataAccess.Entities.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OptionsConfiguration;

namespace Aggregator.Core.Services.MessageBuilders;

/// <summary>
/// Построитель уведомлений для операций AcqFinAuth.
/// </summary>
/// <remarks>
/// Класс реализует интерфейс INotificationMessageBuilder для формирования уведомлений.
/// </remarks>
public class AcqFinAuthNotificationMessageBuilder(
    IOptions<NotificationMessageOptions> notificationMessageOptions,
    IServiceProvider serviceProvider,
    IKeyWordBuilder<AcqFinAuth> keyWordBuilder)
    : INotificationMessageBuilder<AcqFinAuth>
{
    private readonly NotificationMessageOptions _notificationMessageOptions = notificationMessageOptions.Value;

    /// <summary>
    /// Асинхронно формирует список уведомлений по идентификаторам.
    /// </summary>
    /// <param name="notificationIds">Список идентификаторов уведомлений.</param>
    /// <param name="cancellationToken">Токен для отмены операции.</param>
    /// <returns>Список сформированных уведомлений.</returns>
    /// <exception cref="ArgumentNullException">Выбрасывается, если unitOfWork или context равны null.</exception>
    public async Task<List<NotificationMessage>> BuildNotificationAsync(List<long> notificationIds,
        CancellationToken cancellationToken)
    {
        var list = new List<NotificationMessage>();

        using var scope = serviceProvider.CreateScope();

        using var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

        await using var context = scope.ServiceProvider.GetRequiredService<AggregatorDbContext>();
        
        if (unitOfWork == null)
            throw new ArgumentNullException(nameof(unitOfWork));

        if (context == null)
            throw new ArgumentNullException(nameof(context));

        var messages =
            await unitOfWork.AcqFinAuth.GetByIdsWithIncludesAsync(notificationIds,
                cancellationToken,
                x => x.Details,
                x => x.MerchantInfo);

        foreach (var message in messages)
        {
            var messageText = await unitOfWork.NotificationMessageTextDirectories.FindAsync(
                x => x.NotificationType == NotificationMessageType.AcqFinAuth &&
                     (int)x.OperationType! == message.Details.TransType, cancellationToken);

            if (messageText == null)
                continue;

            if (!messageText.IsNeedSend)
                continue;

            var customerId = await GetCustomerIdAsync(message.MerchantInfo.TerminalId, context, cancellationToken);

            if (customerId == null)
                continue;

            var languageSelector = scope.ServiceProvider.GetRequiredService<ILanguageSelector>();
            
            var languageId = await languageSelector.GetLanguageIdAsync(customerId.Value, context, cancellationToken);

            var language = Language.Russian;
            
            if(languageId != null)
                language = (Language)languageId;
            
            if (language == Language.Undefined)
                continue;
            
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

    /// <summary>
    /// Получает идентификатор клиента по терминалу.
    /// </summary>
    /// <param name="terminalId">Идентификатор терминала.</param>
    /// <param name="context">Контекст базы данных.</param>
    /// <param name="cancellationToken">Токен для отмены операции.</param>
    /// <returns>Идентификатор клиента или null, если не найден.</returns>
    private async Task<long?> GetCustomerIdAsync(string terminalId, AggregatorDbContext context,
        CancellationToken cancellationToken)
    {
        var connection = context.Database.GetDbConnection();

        if (connection.State != ConnectionState.Open)
            await connection.OpenAsync(cancellationToken);

        await using var command = connection.CreateCommand();

        command.CommandText = @"
            SELECT a.CustomerId
            FROM Cards.Offices o
        JOIN Accounts a
            ON a.AccountNo = o.AccountNoIncome
        WHERE o.DeviceCode = @terminalId";

        var parameter = command.CreateParameter();
        parameter.ParameterName = "@terminalId";
        parameter.Value = terminalId;
        command.Parameters.Add(parameter);

        var result = await command.ExecuteScalarAsync(cancellationToken);

        if (result == null || result == DBNull.Value)
            return null;

        return Convert.ToInt64(result);
    }
}