using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using PersonIdentificationDirectory.API.Commons;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace PersonIdentificationDirectory.API.Filters
{
    public class SwaggerLanguageFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            operation.Parameters ??= new List<OpenApiParameter>();

            operation.Parameters.Add(new OpenApiParameter()
            {
                Name = "Accept-Language",
                In = ParameterLocation.Header,
                Schema = new OpenApiSchema { Type = "String" },
                Example = new OpenApiString(LanguageConstants.En)
            });
        }
    }
}
