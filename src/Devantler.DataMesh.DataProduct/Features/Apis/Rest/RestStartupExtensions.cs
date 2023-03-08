using System.Reflection;
using System.Text.Json.Serialization;
using Devantler.DataMesh.DataProduct.Configuration.Options;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.OpenApi.Models;

namespace Devantler.DataMesh.DataProduct.Features.Apis.Rest;

/// <summary>
/// Extensions for registering REST to the DI container and configure the web application to use it.
/// </summary>
public static class RestStartupExtensions
{
    /// <summary>
    /// Registers REST to the DI container.
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="options"></param>
    public static WebApplicationBuilder AddRest(this WebApplicationBuilder builder, DataProductOptions options)
    {
        _ = builder.Services
                .AddControllers(
                    options => options.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer())))
                .AddJsonOptions(
                    options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
        _ = builder.Services.AddSwaggerGen(swaggerOptions =>
        {
            swaggerOptions.SwaggerDoc(
                "v1",
                new OpenApiInfo
                {
                    Version = options.Version,
                    Title = options.Name,
                    Description = options.Description,
                    TermsOfService = !string.IsNullOrEmpty(options.Owner?.Website)
                            ? new Uri(options.Owner.Website)
                            : null,
                    Contact = new OpenApiContact
                    {
                        Name = options.Owner?.Name,
                        Email = options.Owner?.Email,
                        Url = !string.IsNullOrEmpty(options.Owner?.Website)
                            ? new Uri(options.Owner.Website)
                            : null
                    },
                    License = new OpenApiLicense
                    {
                        Name = "The MIT License",
                        Url = new Uri("https://opensource.org/license/mit/")
                    }
                });
            swaggerOptions.IncludeXmlComments(System.IO.Path.Combine(AppContext.BaseDirectory,
                $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
        });
        _ = builder.Services.AddEndpointsApiExplorer();
        return builder;
    }

    /// <summary>
    /// Configures the web application to use REST.
    /// </summary>
    /// <param name="app"></param>
    /// <param name="options"></param>
    public static WebApplication UseRest(this WebApplication app, DataProductOptions options)
    {
        _ = app.MapControllers();
        _ = app.UseSwagger().UseSwaggerUI();
        return app;
    }
}
