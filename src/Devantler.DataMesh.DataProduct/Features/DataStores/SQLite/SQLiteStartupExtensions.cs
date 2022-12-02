namespace Devantler.DataMesh.DataProduct.Features.DataStores.SQLite;

public static partial class SQLiteStartupExtensions
{
    public static IServiceCollection AddSQLite(this IServiceCollection services, IConfiguration configuration)
    {
        GenerateServiceRegistrations(services);
        return services;
    }

    public static WebApplication UseSQLite(this WebApplication app, IConfiguration configuration)
    {
        GenerateMiddlewareRegistrations(app);
        return app;
    }

    static partial void GenerateServiceRegistrations(IServiceCollection services);
    static partial void GenerateMiddlewareRegistrations(WebApplication app);
}
