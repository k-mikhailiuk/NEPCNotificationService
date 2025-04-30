using Aggregator.Core.Services;
using Aggregator.DataAccess.Abstractions;
using Microsoft.Extensions.Options;
using OptionsConfiguration;

namespace Aggregator.App;

/// <summary>
/// Processes incoming inbox messages in batches.
/// </summary>
/// <remarks>
/// This background service periodically checks for unprocessed messages from the inbox,
/// logs the activity, and sends them for processing using MediatR.
/// </remarks>
public class InboxProcessor(
    ILogger<InboxProcessor> logger,
    IServiceProvider serviceProvider,
    IOptions<AggregatorOptions> aggregatorOptions)
    : BackgroundService
{
    private readonly AggregatorOptions _aggregatorOptions = aggregatorOptions.Value;

    /// <summary>
    /// Executes the background service.
    /// </summary>
    /// <param name="cancelationToken">A token that can be used to signal cancellation.</param>
    /// <returns>A task that represents the long-running operation.</returns>
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
    /// Processes a batch of unprocessed inbox messages.
    /// </summary>
    /// <param name="cancellationToken">A token that can be used to signal cancellation.</param>
    /// <returns>A task representing the asynchronous batch processing operation.</returns>
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