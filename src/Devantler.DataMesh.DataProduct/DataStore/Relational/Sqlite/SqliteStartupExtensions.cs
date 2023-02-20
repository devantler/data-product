using Devantler.DataMesh.DataProduct.Configuration.Options.DataStoreOptions.Relational;
using Microsoft.EntityFrameworkCore;

namespace Devantler.DataMesh.DataProduct.DataStore.Relational.Sqlite;

/// <summary>
/// Extensions for registering the Sqlite data store and configuring the web application to use it.
/// </summary>
public static class SqliteStartupExtensions
{
    /// <summary>
    /// Registers the Sqlite data store to the DI container.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="options"></param>
    public static IServiceCollection AddSqlite(this IServiceCollection services, SqliteDataStoreOptions? options)
    {
        _ = services.AddDbContext<SqliteDbContext>(dbOptions =>
            dbOptions.UseSqlite(options?.ConnectionString)
        );
        return services;
    }

    /// <summary>
    /// Configures the web application to use Sqlite as a data store.
    /// </summary>
    /// <param name="app"></param>
    public static WebApplication UseSqlite(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<SqliteDbContext>();
        _ = context.Database.EnsureCreated();
        return app;
    }
}
