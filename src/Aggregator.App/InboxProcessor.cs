using Aggregator.Core.Commands;
using Aggregator.Repositories.Abstractions;
using MediatR;
using Microsoft.Extensions.Options;
using OptionsConfiguration;

namespace Aggregator.App;

public class InboxProcessor : BackgroundService
{
    private readonly ILogger<InboxProcessor> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly AggregatorOptions _aggregatorOptions;

    public InboxProcessor(ILogger<InboxProcessor> logger, IServiceProvider serviceProvider,
        IOptions<AggregatorOptions> aggregatorOptions)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _aggregatorOptions = aggregatorOptions.Value;
    }

    protected override async Task ExecuteAsync(CancellationToken cancelationToken)
    {
        _logger.LogInformation("InboxProcessor started with batch size: {BatchSize}", _aggregatorOptions.BatchSize);

        while (!cancelationToken.IsCancellationRequested)
        {
            try
            {
                await ProcessBatchAsync(cancelationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while processing Inbox messages.");
            }

            await Task.Delay(_aggregatorOptions.IntervalInSeconds, cancelationToken);
        }
    }

    private async Task ProcessBatchAsync(CancellationToken cancelationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

        var messages =
            await unitOfWork.Inbox.GetUnprocessedMessagesAsync(_aggregatorOptions.BatchSize, cancelationToken);

        if (messages.Count == 0)
        {
            _logger.LogDebug("No unprocessed messages found.");
            return;
        }

        _logger.LogInformation("Processing {Count} messages from Inbox.", messages.Count);

        await mediator.Send(new ProcessInboxMessageCommand(messages), cancelationToken);
    }
}