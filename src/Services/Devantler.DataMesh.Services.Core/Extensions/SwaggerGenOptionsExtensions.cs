using System.Reflection;
using Devantler.DataMesh.Services.Core.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Devantler.DataMesh.Services.Core.Extensions;

public static class SwaggerGenOptionsExtensions
{
    public static void AddSwaggerDoc(this SwaggerGenOptions options, string title, string version, string description)
    {
        options.SwaggerDoc(version, new OpenApiInfo
        {
            Version = version,
            Title = title,
            Description = description,
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
        var xmlFileName = $"{Assembly.GetCallingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFileName);
        options.IncludeXmlComments(xmlPath);
    }
}