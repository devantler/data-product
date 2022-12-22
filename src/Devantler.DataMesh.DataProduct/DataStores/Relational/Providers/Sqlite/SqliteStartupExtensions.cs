namespace Devantler.DataMesh.DataProduct.DataStores.Relational.Providers.Sqlite;

public static partial class SqliteStartupExtensions
{
    public static IServiceCollection AddSqlite(this IServiceCollection services)
    {
        GenerateServiceRegistrations(services);
        return services;
    }

    public static WebApplication UseSqlite(this WebApplication app)
    {
        GenerateMiddlewareRegistrations(app);
        return app;
    }

    static partial void GenerateServiceRegistrations(IServiceCollection services);
    static partial void GenerateMiddlewareRegistrations(WebApplication app);
}
