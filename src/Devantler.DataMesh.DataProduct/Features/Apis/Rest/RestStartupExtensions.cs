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
    /// <param name="services"></param>
    /// <param name="options"></param>
    public static IServiceCollection AddRest(this IServiceCollection services, DataProductOptions options)
    {
        _ = services.AddControllers(
                o => o.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer()))
            )
            .AddJsonOptions(
                o => o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles
            );
        _ = services.AddSwaggerGen(swaggerOptions =>
        {
            string[] methodsOrder = new string[7] { "GET", "POST", "PUT", "PATCH", "DELETE", "OPTIONS", "TRACE" };
            swaggerOptions.OrderActionsBy((apiDesc) => $"{apiDesc.ActionDescriptor.RouteValues["controller"]}_{Array.IndexOf(methodsOrder, apiDesc.HttpMethod)}");
            swaggerOptions.SwaggerDoc(
                options.Release,
                new OpenApiInfo
                {
                    Version = options.Release,
                    Title = options.Name,
                    Description = options.Description,
                    TermsOfService = !string.IsNullOrEmpty(options.Owner.Website)
                        ? new Uri(options.Owner.Website)
                        : null,
                    Contact = new OpenApiContact
                    {
                        Name = options.Owner.Name,
                        Email = options.Owner.Email,
                        Url = !string.IsNullOrEmpty(options.Owner?.Website)
                            ? new Uri(options.Owner.Website)
                            : null
                    },
                    License = new OpenApiLicense
                    {
                        Name = options.License.Name,
                        Url = !string.IsNullOrEmpty(options.License.Url)
                            ? new Uri(options.License.Url)
                            : null
                    }
                });
            swaggerOptions.IncludeXmlComments(System.IO.Path.Combine(AppContext.BaseDirectory,
                $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
        });
        _ = services.AddEndpointsApiExplorer();
        return services;
    }

    /// <summary>
    /// Configures the web application to use REST.
    /// </summary>
    /// <param name="app"></param>
    /// <param name="options"></param>
    public static WebApplication UseRest(this WebApplication app, DataProductOptions options)
    {
        _ = app.UseSwagger();
        _ = app.UseSwaggerUI(
            swaggerOptions =>
            {
                swaggerOptions.SwaggerEndpoint($"/swagger/{options.Release}/swagger.json", $"{options.Name} {options.Release}");
                swaggerOptions.RoutePrefix = "swagger";
            });
        _ = app.MapControllers();

        return app;
    }
}
