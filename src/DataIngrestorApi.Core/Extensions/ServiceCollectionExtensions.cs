using System.Text.Json;
using System.Text.Json.Serialization;
using DataIngrestorApi.Core.MessageProcessor.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace DataIngrestorApi.Core.Extensions;

/// <summary>
/// Методы-расширения для коллекции сервисов, связанные с конфигурацией процессора сообщений и параметров сериализации JSON.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Регистрирует реализацию <see cref="IMessageProcessor"/> в контейнере зависимостей.
    /// Использует скоуп для создания экземпляра <see cref="MessageProcessor.MessageProcessor"/>.
    /// </summary>
    /// <param name="services">Коллекция сервисов, в которую регистрируется IMessageProcessor.</param>
    /// <returns>Обновленная коллекция сервисов.</returns>
    public static IServiceCollection AddMessageProcessor(this IServiceCollection services)
        => services.AddScoped<IMessageProcessor, MessageProcessor.MessageProcessor>();

    /// <summary>
    /// Регистрирует параметры сериализации JSON в виде синглтона в коллекции сервисов.
    /// Настраивает <see cref="JsonSerializerOptions"/> так, чтобы при сериализации игнорировались свойства со значением <c>null</c>.
    /// </summary>
    /// <param name="services">Коллекция сервисов, в которую добавляются параметры сериализации JSON.</param>
    /// <returns>Обновленная коллекция сервисов.</returns>
    public static IServiceCollection AddJsonSerializeOptions(this IServiceCollection services)
    {
        var jsonOptions = new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };
        
        services.AddSingleton(jsonOptions);
        
        return services;
    }
}