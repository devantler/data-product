using Devantler.DataMesh.DataProduct.Configuration.Options;
using Devantler.DataMesh.DataProduct.Configuration.Options.Services.DataStore;

namespace Devantler.DataMesh.DataProduct.Features.DataStore;

/// <summary>
/// Extensions to registers a data store to the DI container and configure the web application to use it.
/// </summary>
public static partial class DataStoreStartupExtensions
{
    /// <summary>
    /// Registers a data store to the DI container.
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="options"></param>
    /// <exception cref="NotImplementedException">Thrown when a data store is not implemented.</exception>
    public static WebApplicationBuilder AddDataStore(this WebApplicationBuilder builder, DataProductOptions options)
    {
        builder.Services.AddGeneratedServiceRegistrations(options.Services.DataStore);
        _ = options.Services.DataStore.Type switch
        {
            DataStoreType.SQL => _ = builder.Services.AddDatabaseDeveloperPageExceptionFilter(),
            DataStoreType.NoSQL => throw new NotSupportedException("Document based data stores are not supported yet."),
            DataStoreType.Graph => throw new NotSupportedException("Graph based data stores are not supported yet."),
            _ => throw new NotSupportedException($"The data store type {options.Services.DataStore} is not supported."),
        };
        return builder;
    }

    /// <summary>
    /// Configures the web application to use a data store.
    /// </summary>
    /// <param name="app"></param>
    /// <param name="options"></param>
    /// <exception cref="NotImplementedException">Thrown when a data store is not implemented.</exception>
    public static WebApplication UseDataStore(this WebApplication app, DataProductOptions options)
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
        switch (options.Services.DataStore.Type)
        {
            case DataStoreType.SQL:
                if (app.Environment.IsDevelopment())
                    _ = app.UseMigrationsEndPoint();
                break;
            case DataStoreType.NoSQL:
                throw new NotSupportedException("Document based data stores are not supported yet.");
            case DataStoreType.Graph:
                throw new NotSupportedException("Graph based data stores are not supported yet.");
            default:
                throw new NotSupportedException($"The data store type {options.Services.DataStore.Type} is not supported.");
        }
        app.UseGeneratedServiceRegistrations(options.Services.DataStore);

        return app;
    }

    static partial void AddGeneratedServiceRegistrations(this IServiceCollection services, IDataStoreOptions options);

    static partial void UseGeneratedServiceRegistrations(this WebApplication app, IDataStoreOptions options);
}
