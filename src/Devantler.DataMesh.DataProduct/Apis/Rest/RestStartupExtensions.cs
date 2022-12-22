using System.Reflection;
using Devantler.DataMesh.DataProduct.Extensions;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Devantler.DataMesh.DataProduct.Apis.Rest;

public static partial class RestStartupExtensions
{
    public static void AddRestApi(this IServiceCollection services)
    {
        services.AddControllers(options =>
            options.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer())));
        services.AddSwaggerGen(options =>
        {
            GenerateSwaggerDoc(options);
            options.IncludeXmlComments(System.IO.Path.Combine(AppContext.BaseDirectory,
                $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
        });
        services.AddEndpointsApiExplorer();
    }

    public static void UseRestApi(this WebApplication app, IConfiguration configuration)
    {
        if (configuration.IsFeatureEnabled(Constants.AUTHORISATION_FEATURE_FLAG))
            app.UseAuthorization();

        app.MapControllers();
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    static partial void GenerateSwaggerDoc(SwaggerGenOptions options);
}
