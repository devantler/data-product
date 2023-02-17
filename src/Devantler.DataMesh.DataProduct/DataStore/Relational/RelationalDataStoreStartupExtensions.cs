using Devantler.DataMesh.DataProduct.Configuration.Options.DataStoreOptions.Relational;
using Devantler.DataMesh.DataProduct.DataStore.Relational.Sqlite;

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
    /// <param name="options"></param>
    /// <exception cref="NotImplementedException">Thrown when the relational data store is not implemented yet.</exception>
    public static IServiceCollection AddRelationalDataStore(this IServiceCollection services,
        RelationalDataStoreOptionsBase? options)
    {
        _ = options?.Provider switch
        {
            RelationalDataStoreProvider.SQLite => services.AddSqlite(options as SqliteDataStoreOptions),
            _ => throw new NotImplementedException(
                $"The {options?.Provider} relational DataStore is not implemented yet."),
        };
        _ = services.AddDatabaseDeveloperPageExceptionFilter();
        return services;
    }

    /// <summary>
    /// Configures the web application to use a relation data store.
    /// </summary>
    /// <param name="app"></param>
    /// <param name="options"></param>
    /// <exception cref="NotImplementedException">Thrown when the relational data store is not implemented yet.</exception>
    public static WebApplication UseRelationalDataStore(this WebApplication app,
        RelationalDataStoreOptionsBase? options)
    {
        if (!app.Environment.IsDevelopment())
        {
            _ = app.UseExceptionHandler("/Error");
            _ = app.UseHsts();
        }
        else
        {
            _ = app.UseDeveloperExceptionPage();
            _ = app.UseMigrationsEndPoint();
        }

        _ = options?.Provider switch
        {
            RelationalDataStoreProvider.SQLite => app.UseSqlite(),
            _ => throw new NotImplementedException(
                $"The {options?.Provider} relational DataStore is not implemented yet."),
        };
        return app;
    }
}
