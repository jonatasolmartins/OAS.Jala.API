using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace OAS.Jala.API.SwaggerFilters;

public class FileSwaggerFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if(context.MethodInfo.Name != "CreatePerson")
            return;

        operation.RequestBody = new OpenApiRequestBody()
        {
            Description = "upload a file",
            Content = new Dictionary<string, OpenApiMediaType>()
            {
                {
                    "multipart/form-data", new OpenApiMediaType()
                    {
                        Schema = new OpenApiSchema()
                        {
                            Type = "object",
                            Properties = new Dictionary<string, OpenApiSchema>()
                            {
                                {
                                    "file", new OpenApiSchema()
                                    {
                                        Type = "string",
                                        Format = "binary"
                                    }
                                }
                            }
                        }
                    }
                }
            }
        };
    }
}