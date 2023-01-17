using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace OAS.Jala.API.SwaggerFilters;

public class AuthSwaggerFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var attribute = context.ApiDescription.CustomAttributes()
            .FirstOrDefault(x => x.GetType() == typeof(NeedAuthorizationAttribute));
        if(attribute == null)
            return;

        if (operation.Parameters == null)
            operation.Parameters = new List<OpenApiParameter>();
        
        operation.Parameters.Add(
            new OpenApiParameter()
            {
                Name = "Auth",
                In = ParameterLocation.Header,
                Description = "Default auth value",
                Required = true,
                Schema = new OpenApiSchema()
                {
                    Type = "string",
                    Default = new OpenApiString("Beartoke")
                }
            });
    }
}