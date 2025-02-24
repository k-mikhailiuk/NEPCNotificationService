using Microsoft.OpenApi.Models;

namespace DataIngrestorApi.App.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(option =>
        {
            option.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "DataIngrestorApi",
                Version = "v1"
            });
        });

        return services;
    }
}