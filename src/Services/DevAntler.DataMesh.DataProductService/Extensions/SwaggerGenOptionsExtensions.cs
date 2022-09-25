using System.Reflection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace DevAntler.DataMesh.DataProductService.Extensions;

public static class SwaggerGenOptionsExtensions
{
    public static void AddSwaggerDoc(this SwaggerGenOptions options)
    {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "Data Product API",
            Description = "An ASP.NET Core Web API for managing Data Products",
            Contact = new OpenApiContact
            {
                Name = "Nikolai Emil Damm",
                Url = new Uri("https://github.com/devantler")
            },
            License = new OpenApiLicense
            {
                Name = "License - MIT",
                Url = new Uri("https://choosealicense.com/licenses/mit/")
            }
        });
    }

    public static void IncludeXmlComments(this SwaggerGenOptions options)
    {
        var xmlFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFileName);
        options.IncludeXmlComments(xmlPath);
    }
}