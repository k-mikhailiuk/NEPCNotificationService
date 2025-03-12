using Microsoft.Extensions.DependencyInjection;
using NotificationService.Core.Services;
using NotificationService.Core.Services.Abstractions;

namespace NotificationService.Core;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMessageSender(this IServiceCollection services)
    {
        services.AddScoped<INotificationSender, NotificationSender>();
        return services;
    }
}