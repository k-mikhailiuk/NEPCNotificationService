using DataIngrestorApi.App.Extensions;
using DataIngrestorApi.DataAccess;
using DataIngrestorApi.Core.Extensions;
using OptionsConfiguration;

namespace DataIngrestorApi.App;

/// <summary>
/// Конфигурация DI (Dependency Injection) для Ingress API.
/// </summary>
public static class DIConfigure
{
    /// <summary>
    /// Регистрирует пользовательские сервисы в контейнере зависимостей.
    /// Добавляются опции сериализации JSON, Swagger, контроллеры с кастомным форматтером, 
    /// а также сервисы для обработки сообщений, подключения к базе данных и контекст API.
    /// </summary>
    /// <param name="services">Коллекция сервисов, в которую будут добавлены зависимости.</param>
    /// <param name="configuration">Конфигурация приложения, содержащая необходимые настройки.</param>
    public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddJsonSerializeOptions();
        services.AddSwagger();
        services.AddControllers(options =>
        {
            options.InputFormatters.Add(new CustomTextPlainInputFormatter());
        });
        services.AddEndpointsApiExplorer();
        services.AddMessageProcessor();
        services.AddDatabaseConnectionString(configuration);
        services.AddIngressApiDbContext(configuration);
    }
}