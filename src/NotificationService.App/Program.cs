using NotificationService.App;

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
        services.AddHostedService<NotificationProcessor>();
    });

var host = builder.Build();
host.Run();