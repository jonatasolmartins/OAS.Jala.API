using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace OAS.Jala.API.SwaggerFilters;

public class WithHeaderSwaggerFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var attribute = context.ApiDescription.CustomAttributes()
            .FirstOrDefault(x => x.GetType() == typeof(ProducesHeaderAttribute));
        if(attribute == null)
            return;
        
        var headerAttribute = attribute as ProducesHeaderAttribute;
        foreach (var response in operation.Responses)
        {
            if (response.Key.StartsWith("2"))
            {
                response.Value.Headers.Add
                    (
                        headerAttribute._name,
                        new OpenApiHeader()
                        {
                            Description = "Response type hearer",
                            Schema = new OpenApiSchema()
                            {
                                Type = "string"
                            }
                        });
            }
        }
    }
}