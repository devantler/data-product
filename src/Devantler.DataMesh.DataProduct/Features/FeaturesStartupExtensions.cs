using Microsoft.FeatureManagement;
using Devantler.DataMesh.DataProduct.Features.DataStores;
using Devantler.DataMesh.DataProduct.Features.Apis;
using System.Reflection;

namespace Devantler.DataMesh.DataProduct.Features;

public static class FeaturesStartupExtensions
{
    public static void AddFeatures(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddFeatureManagement(configuration.GetSection("Features"));
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddApis(configuration);
        services.AddDataStore(configuration);
    }

    public static void UseFeatures(this WebApplication app, IConfiguration configuration) {
        app.UseApis(configuration);
        app.UseDataStore(configuration);
    }
}
