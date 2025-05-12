using Aggregator.Core.Services;
using Aggregator.DataAccess.Abstractions;
using Microsoft.Extensions.Options;
using OptionsConfiguration;

namespace Aggregator.App;

/// <summary>
/// Обрабатывает входящие сообщения из очереди пакетами.
/// </summary>
/// <remarks>
/// Этот фоновой сервис периодически проверяет наличие необработанных сообщений во входящей очереди,
/// записывает информацию в лог и отправляет их на обработку через MediatR.
/// </remarks>
public class InboxProcessor(
    ILogger<InboxProcessor> logger,
    IServiceProvider serviceProvider,
    IOptions<AggregatorOptions> aggregatorOptions)
    : BackgroundService
{
    private readonly AggregatorOptions _aggregatorOptions = aggregatorOptions.Value;

    /// <summary>
    /// Выполняет работу фонового сервиса.
    /// </summary>
    /// <param name="cancelationToken">Токен для отмены операции.</param>
    /// <returns>Задача, представляющая непрерывную фоновую обработку.</returns>
    protected override async Task ExecuteAsync(CancellationToken cancelationToken)
    {
        logger.LogInformation("InboxProcessor started with batch size: {BatchSize}", _aggregatorOptions.BatchSize);

        while (!cancelationToken.IsCancellationRequested)
        {
            try
            {
                await ProcessBatchAsync(cancelationToken);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred while processing Inbox messages.");
            }

            await Task.Delay(_aggregatorOptions.IntervalInSeconds, cancelationToken);
        }
    }

    /// <summary>
    /// Обрабатывает очередной пакет необработанных входящих сообщений.
    /// </summary>
    /// <param name="cancellationToken">Токен для отмены операции.</param>
    /// <returns>Задача, представляющая асинхронную обработку пакета.</returns>
    private async Task ProcessBatchAsync(CancellationToken cancellationToken)
    {
        using var scope = serviceProvider.CreateScope();
        var unitOfWork = scope.ServiceProvider.GetRequiredService<IAggregatorUnitOfWork>();

        var messages =
            await unitOfWork.Inbox.GetUnprocessedMessagesAsync(_aggregatorOptions.BatchSize, cancellationToken);

        if (messages.Count == 0)
        {
            logger.LogDebug("No unprocessed messages found.");
            return;
        }

        logger.LogInformation("Processing {Count} messages from Inbox.", messages.Count);

        var inboxHandler = scope.ServiceProvider.GetRequiredService<IInboxHandler>();
        await inboxHandler.HandleAsync(messages, cancellationToken);
    }
}