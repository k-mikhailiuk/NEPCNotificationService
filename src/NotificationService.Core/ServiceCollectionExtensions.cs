using Microsoft.Extensions.DependencyInjection;
using NotificationService.Core.Services;
using NotificationService.Core.Services.Abstractions;

namespace NotificationService.Core;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddNotificationMessageServices(this IServiceCollection services)
    {
        services.AddScoped<INotificationMessageSender, NotificationMessageSender>();
        services.AddScoped<INotificationHistorySaver, NotificationHistorySaver>();
        return services;
    }
}