using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Mvc.Formatters;
using OAS.Jala.API;
using OAS.Jala.API.SwaggerFilters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    options.ReturnHttpNotAcceptable = true;
    options.OutputFormatters.RemoveType<StringOutputFormatter>();
    options.OutputFormatters.RemoveType<TextOutputFormatter>();
    options.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status500InternalServerError));
    options.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status406NotAcceptable));
}).AddNewtonsoftJson(options =>
{
    options.SerializerSettings.Converters.Add(JsonSubtypesSerialization.SubTypesConverter);
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc(
        name:"OAS-Demo",
        new OpenApiInfo()
        {
            Title = "OAS Api demo",
            Description = "OAS Demo",
            TermsOfService = new Uri("http://tempuri.org/terms"),
            Contact = new OpenApiContact()
            {
                Name = "Jonatas",
                Email = "Jonatas@email.com"
            },
            License = new OpenApiLicense()
            {
                Name = "Apache 2.0",
                Url = new Uri("http://www.apache.org/licenses/LICENSE-2.0.html")
            }
        });
    c.UseAllOfToExtendReferenceSchemas();
    c.UseAllOfForInheritance();
    c.UseOneOfForPolymorphism();
    c.SelectDiscriminatorNameUsing(type =>
    {
        return type.Name switch
        {
            nameof(Person) => "_type",
            _ => null
        };
    });
    c.OperationFilter<WithHeaderSwaggerFilter>();
    c.OperationFilter<AuthSwaggerFilter>();
    c.OperationFilter<FileSwaggerFilter>();
    
    var xmlDocumentPath = Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml");
    c.IncludeXmlComments(xmlDocumentPath);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(configurationAction =>
    {
        configurationAction.SwaggerEndpoint("/swagger/OAS-Demo/swagger.json", "AOS Api");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();