using Devantler.DataMesh.DataProduct.DataStore.Relational.Providers.Sqlite;
using Devantler.DataMesh.DataProduct.Extensions;

namespace Devantler.DataMesh.DataProduct.DataStore.Relational;

/// <summary>
/// Extensions to register a relation data store and configuring the web application to use it.
/// </summary>
public static class RelationalDataStoreStartupExtensions
{
    /// <summary>
    /// Registers a relational data store to the DI container.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <exception cref="NotSupportedException">Thrown when the relational data store is not supported.</exception>
    public static void AddRelationalDataStore(this IServiceCollection services, IConfiguration configuration)
    {
        _ = configuration.GetFeatureValue(Constants.DataStoreProviderFeatureFlag) switch
        {
            Providers.Sqlite.Constants.DataStoreProviderFeatureFlagValue => services.AddSqlite(),
            _ => throw new NotSupportedException("Provided relational DataStore is not supported"),
        };
    }

    /// <summary>
    /// Configures the web application to use a relation data store.
    /// </summary>
    /// <param name="app"></param>
    /// <param name="configuration"></param>
    /// <exception cref="NotSupportedException">Thrown when the relational data store is not supported.</exception>
    public static void UseRelationalDataStore(this WebApplication app, IConfiguration configuration)
    {
        _ = configuration.GetFeatureValue(Constants.DataStoreProviderFeatureFlag) switch
        {
            Providers.Sqlite.Constants.DataStoreProviderFeatureFlagValue => app.UseSqlite(),
            _ => throw new NotSupportedException("Provided relational DataStore is not supported"),
        };
    }
}
