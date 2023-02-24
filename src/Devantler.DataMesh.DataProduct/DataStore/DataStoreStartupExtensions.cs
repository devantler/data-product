using Devantler.DataMesh.DataProduct.Configuration.Options.DataStoreOptions;

namespace Devantler.DataMesh.DataProduct.DataStore;

/// <summary>
/// Extensions to registers a data store to the DI container and configure the web application to use it.
/// </summary>
public static partial class DataStoreStartupExtensions
{
    /// <summary>
    /// Registers a data store to the DI container.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="options"></param>
    /// <exception cref="NotImplementedException">Thrown when a data store is not implemented.</exception>
    public static IServiceCollection AddDataStore(this IServiceCollection services, IDataStoreOptions options)
    {
        services.AddGeneratedServiceRegistrations(options);
        _ = options.Type switch
        {
            DataStoreType.Relational => _ = services.AddDatabaseDeveloperPageExceptionFilter(),
            DataStoreType.DocumentBased => throw new NotSupportedException("Document based data stores are not supported yet."),
            DataStoreType.GraphBased => throw new NotSupportedException("Graph based data stores are not supported yet."),
            _ => throw new NotSupportedException($"The data store type {options.Type} is not supported."),
        };
        return services;
    }

    /// <summary>
    /// Configures the web application to use a data store.
    /// </summary>
    /// <param name="app"></param>
    /// <param name="options"></param>
    /// <exception cref="NotImplementedException">Thrown when a data store is not implemented.</exception>
    public static WebApplication UseDataStore(this WebApplication app, IDataStoreOptions options)
    {
        if (!app.Environment.IsDevelopment())
        {
            _ = app.UseExceptionHandler("/Error");
            _ = app.UseHsts();
        }
        else
        {
            _ = app.UseDeveloperExceptionPage();
        }
        switch (options.Type)
        {
            case DataStoreType.Relational:
                if (app.Environment.IsDevelopment())
                    _ = app.UseMigrationsEndPoint();
                break;
            case DataStoreType.DocumentBased:
                throw new NotSupportedException("Document based data stores are not supported yet.");
            case DataStoreType.GraphBased:
                throw new NotSupportedException("Graph based data stores are not supported yet.");
            default:
                throw new NotSupportedException($"The data store type {options.Type} is not supported.");
        }
        app.UseGeneratedServiceRegistrations(options);

        return app;
    }

    static partial void AddGeneratedServiceRegistrations(this IServiceCollection services, IDataStoreOptions options);

    static partial void UseGeneratedServiceRegistrations(this WebApplication app, IDataStoreOptions options);
}
