using Devantler.DataMesh.DataProduct.DataStore.Relational;
using Devantler.DataMesh.DataProduct.Extensions;

namespace Devantler.DataMesh.DataProduct.DataStore;

/// <summary>
/// Extensions to registers a data store to the DI container and configure the web application to use it.
/// </summary>
public static class DataStoreStartupExtensions
{
    /// <summary>
    /// Registers a data store to the DI container.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <exception cref="NotSupportedException">Thrown when the data store is not supported.</exception>
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
    /// <exception cref="NotSupportedException">Thrown when the data store is not supported.</exception>
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
