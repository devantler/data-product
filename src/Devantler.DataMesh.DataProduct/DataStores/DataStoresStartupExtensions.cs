using Devantler.DataMesh.DataProduct.DataStores.Relational;
using Devantler.DataMesh.DataProduct.Extensions;

namespace Devantler.DataMesh.DataProduct.DataStores;

/// <summary>
/// Extensions to registers a data store to the DI container and configure the web application to use it.
/// </summary>
public static class DataStoresStartupExtensions
{
    /// <summary>
    /// Registers a data store to the DI container.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    public static void AddDataStore(this IServiceCollection services, IConfiguration configuration)
    {
        switch (configuration.GetFeatureValue(Constants.DataStoreTypeFeatureFlag))
        {
            case Relational.Constants.DataStoreTypeFeatureFlagValue:
                services.AddRelationalDataStore(configuration);
                break;
            default:
                throw new NotSupportedException("DataStore not supported");
        }
    }

    /// <summary>
    /// Configures the web application to use a data store.
    /// </summary>
    /// <param name="app"></param>
    /// <param name="configuration"></param>
    public static void UseDataStore(this WebApplication app, IConfiguration configuration)
    {
        switch (configuration.GetFeatureValue(Constants.DataStoreTypeFeatureFlag))
        {
            case Relational.Constants.DataStoreTypeFeatureFlagValue:
                app.UseRelationalDataStore(configuration);
                break;
            default:
                throw new NotSupportedException("DataStore not supported");
        }
    }
}
