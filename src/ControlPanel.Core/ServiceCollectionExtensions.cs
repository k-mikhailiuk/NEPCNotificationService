using ControlPanel.Core.Services;
using ControlPanel.Core.Services.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace ControlPanel.Core;

/// <summary>
/// Расширения для регистрации сервисов.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Регистрирует сервисы приложения.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    /// <returns>Обновлённая коллекция сервисов.</returns>
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddTransient<INotificationMessageKeyWordsService, NotificationMessageKeyWordsService>();
        services.AddTransient<INotificationMessageTextDirectoriesService, NotificationMessageTextDirectoriesService>();
        services.AddTransient<ICurrenciesService, CurrenciesService>();
        services.AddTransient<ILimitIdDescriptionDirectoriesService, LimitIdDescriptionDirectoriesService>();

        return services;
    }
}