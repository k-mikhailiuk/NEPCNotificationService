using Aggregator.Repositories.Abstractions;

namespace Aggregator.App;

public class InboxProcessor : BackgroundService
{
    private readonly ILogger<InboxProcessor> _logger;
    private readonly IServiceProvider _serviceProvider;

    public InboxProcessor(ILogger<InboxProcessor> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var serviceScope = _serviceProvider.CreateScope();
            
            using var unitOfWork = serviceScope.ServiceProvider.GetRequiredService<IUnitOfWork>();

            var messages = await unitOfWork.Inbox.GetUnprocessedMessagesAsync();
            
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            }

            await Task.Delay(1000, stoppingToken);
        }
    }
}