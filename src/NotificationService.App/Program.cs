using NotificationService.App;

var builder = Host.CreateApplicationBuilder(args);

builder.Configuration.Sources.Clear();
builder.Configuration.AddJsonFile(Path.Combine(AppContext.BaseDirectory, "appsettings.json"), optional: false, reloadOnChange: true);

var services = builder.Services;
services.RegisterServices(builder.Configuration);

builder.Services.AddHostedService<NotificationProcessor>();

var host = builder.Build();
host.Run();