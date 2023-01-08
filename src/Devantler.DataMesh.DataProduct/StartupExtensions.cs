using System.Reflection;
using Devantler.DataMesh.DataProduct.Apis;
using Devantler.DataMesh.DataProduct.DataStores;
using Microsoft.FeatureManagement;

namespace Devantler.DataMesh.DataProduct;

public static class FeaturesStartupExtensions
{
    public static void AddFeatures(this IServiceCollection services, IConfiguration configuration)
    {
        if (configuration == null)
        {
            throw new ArgumentNullException(nameof(configuration));
        }
        _ = services.AddFeatureManagement(configuration.GetSection("Features"));
        _ = services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddApis(configuration);
        services.AddDataStore(configuration);
    }

    public static void UseFeatures(this WebApplication app, IConfiguration configuration)
    {
        app.UseApis(configuration);
        app.UseDataStore(configuration);
    }
}
