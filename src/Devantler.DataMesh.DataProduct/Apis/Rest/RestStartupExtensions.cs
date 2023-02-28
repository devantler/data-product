using System.Reflection;
using System.Text.Json.Serialization;
using Devantler.DataMesh.DataProduct.Configuration.Options;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.OpenApi.Models;

namespace Devantler.DataMesh.DataProduct.Apis.Rest;

/// <summary>
/// Extensions for registering REST to the DI container and configure the web application to use it.
/// </summary>
public static class RestStartupExtensions
{
    /// <summary>
    /// Registers REST to the DI container.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="dataProductOptions"></param>
    public static void AddRestApi(this IServiceCollection services, DataProductOptions dataProductOptions)
    {
        _ = services
                .AddControllers(
                    options => options.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer())))
                .AddJsonOptions(
                    options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
        _ = services.AddSwaggerGen(swaggerOptions =>
        {
            swaggerOptions.SwaggerDoc(
                "v1",
                new OpenApiInfo
                {
                    Version = dataProductOptions.Version,
                    Title = dataProductOptions.Name,
                    Description = dataProductOptions.Description,
                    TermsOfService = !string.IsNullOrEmpty(dataProductOptions.Owner?.Website)
                            ? new Uri(dataProductOptions.Owner.Website)
                            : null,
                    Contact = new OpenApiContact
                    {
                        Name = dataProductOptions.Owner?.Name,
                        Email = dataProductOptions.Owner?.Email,
                        Url = !string.IsNullOrEmpty(dataProductOptions.Owner?.Website)
                            ? new Uri(dataProductOptions.Owner.Website)
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
        _ = services.AddEndpointsApiExplorer();
    }

    /// <summary>
    /// Configures the web application to use REST.
    /// </summary>
    /// <param name="app"></param>
    /// <param name="featureFlags"></param>
    public static void UseRestApi(this WebApplication app, FeatureFlagsOptions featureFlags)
    {
        if (featureFlags.EnableAuthorisation)
            _ = app.UseAuthorization();

        _ = app.MapControllers();
        _ = app.UseSwagger().UseSwaggerUI();
    }
}
