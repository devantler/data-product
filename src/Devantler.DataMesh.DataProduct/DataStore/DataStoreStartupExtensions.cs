using Devantler.DataMesh.DataProduct.Configuration.Options.DataStoreOptions;
using Devantler.DataMesh.DataProduct.Configuration.Options.DataStoreOptions.Relational;
using Devantler.DataMesh.DataProduct.DataStore.Relational;
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
    /// <param name="options"></param>
    /// <exception cref="NotImplementedException">Thrown when a data store is not implemented.</exception>
    public static IServiceCollection AddDataStore(this IServiceCollection services, IDataStoreOptions options)
    {
        return options.Type switch
        {
            DataStoreType.Relational => services.AddRelationalDataStore(options as RelationalDataStoreOptionsBase),
            _ => throw new NotImplementedException(
                $"The relational DataStore type {options.Type} is not implemented yet."),
        };
    }

    /// <summary>
    /// Configures the web application to use a data store.
    /// </summary>
    /// <param name="app"></param>
    /// <param name="options"></param>
    /// <exception cref="NotImplementedException">Thrown when a data store is not implemented.</exception>
    public static WebApplication UseDataStore(this WebApplication app, IDataStoreOptions options)
    {
        return options.Type switch
        {
            DataStoreType.Relational => app.UseRelationalDataStore(options as RelationalDataStoreOptionsBase),
            _ => throw new NotImplementedException(
                $"The relational DataStore type {options.Type} is not implemented yet."),
        };
    }
}
