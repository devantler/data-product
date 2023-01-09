using System.Reflection;
using Devantler.DataMesh.DataProduct.Apis;
using Devantler.DataMesh.DataProduct.Configuration;
using Devantler.DataMesh.DataProduct.DataStore;
using Microsoft.FeatureManagement;

namespace Devantler.DataMesh.DataProduct;

/// <summary>
/// Extensions for registering features and configuring the web application to use them.
/// </summary>
public static class FeaturesStartupExtensions
{
    /// <summary>
    /// Registers features to the DI container.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    public static void AddFeatures(this IServiceCollection services, IConfiguration configuration)
    {
        if (configuration == null)
        {
            throw new ArgumentNullException(nameof(configuration));
        }
        _ = services.AddFeatureManagement(configuration.GetSection(FeaturesOptions.Key));
        _ = services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddApis(configuration);
        services.AddDataStore(configuration);
    }

    /// <summary>
    /// Configures the web application to use enabled features.
    /// </summary>
    /// <param name="app"></param>
    /// <param name="configuration"></param>
    public static void UseFeatures(this WebApplication app, IConfiguration configuration)
    {
        app.UseApis(configuration);
        app.UseDataStore(configuration);
    }
}
