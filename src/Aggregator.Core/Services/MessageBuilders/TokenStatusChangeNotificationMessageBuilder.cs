using System.Data;
using Aggregator.Core.Services.Abstractions;
using Aggregator.DataAccess;
using Aggregator.DataAccess.Entities;
using Aggregator.DataAccess.Entities.Enum;
using Aggregator.DataAccess.Entities.TokenChangeStatus;
using Aggregator.Repositories.Abstractions;
using ControlPanel.DataAccess.Entites.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OptionsConfiguration;

namespace Aggregator.Core.Services.MessageBuilders;

public class TokenStatusChangeNotificationMessageBuilder : INotificationMessageBuilder<TokenStatusChange>
{
    private readonly NotificationMessageOptions _notificationMessageOptions;
    private readonly IServiceProvider _serviceProvider;
    private readonly IKeyWordBuilder<TokenStatusChange> _keyWordBuilder;

    public TokenStatusChangeNotificationMessageBuilder(IOptions<NotificationMessageOptions> notificationMessageOptions,
        IServiceProvider serviceProvider, IKeyWordBuilder<TokenStatusChange> keyWordBuilder)
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

        using var context = scope.ServiceProvider.GetRequiredService<AggregatorDbContext>();

        if (unitOfWork == null)
            throw new ArgumentNullException(nameof(unitOfWork));

        if (context == null)
            throw new ArgumentNullException(nameof(context));

        var messages =
            await unitOfWork.TokenStatusChange.GetListByIdsRawSqlAsync(notificationIds, cancellationToken,
                x => x.Details);

        foreach (var message in messages)
        {
            var messageText = await unitOfWork.NotificationMessageTextDirectories.FindAsync(
                x => x.NotificationType == NotificationMessageType.TokenStatusChange, cancellationToken);

            if (messageText == null)
                throw new NullReferenceException();

            if (!messageText.IsNeedSend)
                return list;

            var customerId = await GetCustomerId(message.Details.CardIdentifier.CardIdentifierValue, context,
                cancellationToken);

            if (customerId == null)
                continue;

            var notificationMessage = new NotificationMessage
            {
                Title = _notificationMessageOptions.Title,
                Status = NotificationMessageStatus.New,
                Message = _keyWordBuilder.BuildKeyWordsAsync(messageText.MessageTextRu, message),
                CustomerId = customerId.Value,
            };

            list.Add(notificationMessage);
        }

        return list;
    }

    private async Task<long?> GetCustomerId(string accountId, AggregatorDbContext context,
        CancellationToken cancellationToken)
    {
        var connection = context.Database.GetDbConnection();

        if (connection.State != ConnectionState.Open)
            await connection.OpenAsync(cancellationToken);

        using var command = connection.CreateCommand();

        command.CommandText = @"
        SELECT accounts.CustomerID
        FROM dbo.Accounts accounts 
        WHERE AccountNo = SUBSTRING(CAST(@accountId AS VARCHAR(50)), 4, LEN(CAST(@accountId AS VARCHAR(50))) - 6)";

        var parameter = command.CreateParameter();
        parameter.ParameterName = "@accountId";
        parameter.Value = accountId;
        command.Parameters.Add(parameter);

        var result = await command.ExecuteScalarAsync(cancellationToken);

        if (result == null || result == DBNull.Value)
            return null;

        return Convert.ToInt64(result);
    }
}