using ControlPanel.Core.Services;
using ControlPanel.Core.Services.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace ControlPanel.Core;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddTransient<INotificationMessageKeyWordsService, NotificationMessageKeyWordsService>();
        services.AddTransient<INotificationMessageTextDirectoriesService, NotificationMessageTextDirectoriesService>();
        services.AddTransient<ICurrenciesService, CurrenciesService>();
        services.AddTransient<ILimitIdDescriptionDirectoriesService, LimitIdDescriptionDirectoriesService>();

        return services;
    }
}