using Aggregator.Core.Commands;
using Aggregator.Repositories.Abstractions;
using MediatR;
using Microsoft.Extensions.Options;
using OptionsConfiguration;

namespace Aggregator.App;

public class InboxProcessor(
    ILogger<InboxProcessor> logger,
    IServiceProvider serviceProvider,
    IOptions<AggregatorOptions> aggregatorOptions)
    : BackgroundService
{
    private readonly AggregatorOptions _aggregatorOptions = aggregatorOptions.Value;

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

    private async Task ProcessBatchAsync(CancellationToken cancelationToken)
    {
        using var scope = serviceProvider.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

        var messages =
            await unitOfWork.Inbox.GetUnprocessedMessagesAsync(_aggregatorOptions.BatchSize, cancelationToken);

        if (messages.Count == 0)
        {
            logger.LogDebug("No unprocessed messages found.");
            return;
        }

        logger.LogInformation("Processing {Count} messages from Inbox.", messages.Count);

        await mediator.Send(new ProcessInboxMessageCommand(messages), cancelationToken);
    }
}