using Microsoft.OpenApi.Models;

namespace DataIngrestorApi.App.Extensions;

/// <summary>
/// Методы-расширения для коллекции сервисов, связанные с конфигурацией Swagger.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Добавляет в коллекцию сервисов конфигурацию Swagger для генерации документации API.
    /// Настраивает Swagger для создания документа версии "v1" с заданными заголовком и версией, 
    /// а также регистрирует фильтр операций для добавления заголовка Content-Type.
    /// </summary>
    /// <param name="services">Коллекция сервисов, в которую будет добавлена конфигурация Swagger.</param>
    /// <returns>Обновленная коллекция сервисов с добавленной конфигурацией Swagger.</returns>
    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(option =>
        {
            option.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "DataIngrestorApi",
                Version = "v1"
            });
            option.OperationFilter<ContentTypeHeaderOperationFilter>();
        });

        return services;
    }
}