using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace DataIngrestorApi.App.Extensions;

public class ContentTypeHeaderOperationFilter : IOperationFilter
{
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