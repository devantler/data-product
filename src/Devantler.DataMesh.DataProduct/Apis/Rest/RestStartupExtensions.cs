using Devantler.DataMesh.DataProduct.Extensions;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Devantler.DataMesh.DataProduct.Apis.Rest;

public static class RESTStartupExtensions
{
    public static void AddRestApi(this IServiceCollection services)
    {
        services.AddControllers(options => options.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer())));
        services.AddSwaggerGen();
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
}
