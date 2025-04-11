using Microsoft.Extensions.DependencyInjection;
using NotificationService.Core.Services;
using NotificationService.Core.Services.Abstractions;

namespace NotificationService.Core;

/// <summary>
/// Методы-расширения для регистрации сервисов, связанных с отправкой уведомлений.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Регистрирует сервисы отправки уведомлений и сохранения истории уведомлений в контейнере зависимостей.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    /// <returns>Обновлённая коллекция сервисов с зарегистрированными зависимостями.</returns>
    public static IServiceCollection AddNotificationMessageServices(this IServiceCollection services)
    {
        services.AddScoped<INotificationMessageSender, NotificationMessageSender>();
        services.AddScoped<INotificationHistorySaver, NotificationHistorySaver>();
        return services;
    }
}