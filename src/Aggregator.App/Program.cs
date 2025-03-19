using Aggregator.App;

var builder = Host.CreateDefaultBuilder(args)
    .UseWindowsService()
    .ConfigureAppConfiguration(config =>
    {
        config.Sources.Clear();
        config.AddJsonFile(Path.Combine(AppContext.BaseDirectory, "appsettings.json"), optional: false,
            reloadOnChange: true);
    })
    .ConfigureServices((ctx, services) =>
    {
        services.RegisterServices(ctx.Configuration);
        services.AddHostedService<InboxProcessor>();
    });

var host = builder.Build();
host.Run();