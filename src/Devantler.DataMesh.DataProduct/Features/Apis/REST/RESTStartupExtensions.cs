using Devantler.DataMesh.DataProduct.Extensions;

namespace Devantler.DataMesh.DataProduct.Features.Apis.REST;

public static class RESTStartupExtensions
{
    public static void AddRESTApi(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.AddSwaggerGen();
        services.AddEndpointsApiExplorer();
    }

    public static void UseRESTApi(this WebApplication app, IConfiguration configuration)
    {
        if (configuration.IsFeatureEnabled(Constants.AUTHORISATION_FEATURE_FLAG))
            app.UseAuthorization();
        
        app.MapControllers();
        app.UseSwagger();
        app.UseSwaggerUI();
    }
}
