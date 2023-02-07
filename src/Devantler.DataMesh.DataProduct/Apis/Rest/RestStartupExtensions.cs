using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Devantler.DataMesh.DataProduct.Apis.Rest;

/// <summary>
/// Extensions for registering REST to the DI container and configure the web application to use it.
/// </summary>
public static partial class RestStartupExtensions
{
    /// <summary>
    /// Registers REST to the DI container.
    /// </summary>
    /// <param name="services"></param>
    public static void AddRestApi(this IServiceCollection services)
    {
        _ = services.AddControllers(options =>
                options.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer())));
        _ = services.AddSwaggerGen(options =>
        {
            GenerateSwaggerDoc(options);
            options.IncludeXmlComments(System.IO.Path.Combine(AppContext.BaseDirectory,
                $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
        });
        _ = services.AddEndpointsApiExplorer();
    }

    /// <summary>
    /// Configures the web application to use REST.
    /// </summary>
    /// <param name="app"></param>
    /// <param name="configuration"></param>
    public static void UseRestApi(this WebApplication app, IConfiguration configuration)
    {
        if (configuration.IsFeatureEnabled(Constants.AuthorisationFeatureFlag))
            _ = app.UseAuthorization();

        _ = app.MapControllers();
        _ = app.UseSwagger().UseSwaggerUI();
    }

    static partial void GenerateSwaggerDoc(SwaggerGenOptions options);
}
