namespace Devantler.DataMesh.DataProduct.DataStore.Relational.Providers.Sqlite;

/// <summary>
/// Extensions for registering the SQLite data store and configuring the web application to use it.
/// </summary>
public static partial class SqliteStartupExtensions
{
    /// <summary>
    /// Registers the SQLite data store to the DI container.
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddSqlite(this IServiceCollection services)
    {
        GenerateServiceRegistrations(services);
        return services;
    }

    /// <summary>
    /// Configures the web application to use SQLite as a data store.
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static WebApplication UseSqlite(this WebApplication app)
    {
        GenerateMiddlewareRegistrations(app);
        return app;
    }

    static partial void GenerateServiceRegistrations(IServiceCollection services);
    static partial void GenerateMiddlewareRegistrations(WebApplication app);
}
