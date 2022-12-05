using Devantler.DataMesh.DataProduct.Apis;
using Microsoft.FeatureManagement;
using System.Reflection;
using Devantler.DataMesh.DataProduct.DataStores;

namespace Devantler.DataMesh.DataProduct;

public static class FeaturesStartupExtensions
{
    public static void AddFeatures(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddFeatureManagement(configuration.GetSection("Features"));
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddApis(configuration);
        services.AddDataStore(configuration);
    }

    public static void UseFeatures(this WebApplication app, IConfiguration configuration)
    {
        app.UseApis(configuration);
        app.UseDataStore(configuration);
    }
}
