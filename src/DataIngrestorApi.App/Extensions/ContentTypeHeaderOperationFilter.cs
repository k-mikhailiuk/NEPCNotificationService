using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace DataIngrestorApi.App.Extensions;

/// <summary>
/// Фильтр операций для добавления заголовка Content-Type в параметры Swagger.
/// </summary>
public class ContentTypeHeaderOperationFilter : IOperationFilter
{
    /// <summary>
    /// Применяет фильтр для операции, добавляя параметр заголовка Content-Type.
    /// </summary>
    /// <param name="operation">Объект OpenApiOperation, который представляет операцию API.</param>
    /// <param name="context">Контекст фильтра, содержащий дополнительную информацию о операции.</param>
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        operation.Parameters.Add(new OpenApiParameter
        {
            Name = "Content-Type",
            In = ParameterLocation.Header,
            Required = true,
            Schema = new OpenApiSchema { Type = "string" },
            Description = "The content type of the request body"
        });
    }
}