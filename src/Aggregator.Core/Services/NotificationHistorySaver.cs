using Aggregator.Core.Services.Abstractions;
using Aggregator.DataAccess;
using Aggregator.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Aggregator.Core.Services;

/// <summary>
/// Сервис для сохранения истории отправленных push-уведомлений в базу данных.
/// </summary>
public class NotificationHistorySaver(IServiceProvider serviceProvider) : INotificationHistorySaver
{
    /// <inheritdoc/>
    public async Task SaveAsync(NotificationMessage message, CancellationToken cancellationToken = default)
    {
        using var scope = serviceProvider.CreateScope();

        await using var context = scope.ServiceProvider.GetRequiredService<AggregatorDbContext>();

        var loginId = await GetLoginIdAsync(message.CustomerId, context, cancellationToken);

        if (!loginId.HasValue)
            return;
        await context.Database.ExecuteSqlInterpolatedAsync(
            $"""
                 INSERT INTO PushNotification.SendHistories
                     (CustomerID, CreateDate, SendDate, NotificationText, NotificationTitle, HasMessageRead, StatusID, IsNews, LoginID)
                 VALUES
                     ({message.CustomerId}, {DateTimeOffset.Now}, {DateTimeOffset.Now}, {message.Message}, {message.Title}, {false}, {2}, {false}, {loginId.Value});
             """, cancellationToken);
    }

    /// <summary>
    /// Выполняет асинхронный запрос к базе данных для получения LoginID по CustomerID.
    /// </summary>
    /// <param name="customerId">Идентификатор клиента (CustomerID).</param>
    /// <param name="context">Контекст базы данных.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>LoginID клиента, если найден; иначе <c>null</c>.</returns>
    private static async Task<int?> GetLoginIdAsync(long customerId, AggregatorDbContext context,
        CancellationToken cancellationToken)
        => await context.Database
            .SqlQuery<int?>($"SELECT LoginID AS [Value] FROM PushNotification.Settings WHERE CustomerID = {customerId}")
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
}