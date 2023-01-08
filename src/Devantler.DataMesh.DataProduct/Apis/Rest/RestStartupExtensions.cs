using System.Reflection;
using Devantler.DataMesh.DataProduct.Extensions;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Devantler.DataMesh.DataProduct.Apis.Rest;

public static partial class RestStartupExtensions
{
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

    public static void UseRestApi(this WebApplication app, IConfiguration configuration)
    {
        if (configuration.IsFeatureEnabled(Constants.AuthorisationFeatureFlag))
            _ = app.UseAuthorization();

        _ = app.MapControllers();
        _ = app.UseSwagger().UseSwaggerUI();
    }

    static partial void GenerateSwaggerDoc(SwaggerGenOptions options);
}
