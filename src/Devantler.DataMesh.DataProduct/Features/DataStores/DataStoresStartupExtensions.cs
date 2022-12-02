using Devantler.DataMesh.DataProduct.Extensions;
using Devantler.DataMesh.DataProduct.Features.DataStores.SQLite;

namespace Devantler.DataMesh.DataProduct.Features.DataStores;

public static class DataStoresStartupExtensions
{
    public static void AddDataStore(this IServiceCollection services, IConfiguration configuration)
    {
        if (configuration.IsFeatureEnabled(Constants.SQLITE_FEATURE_FLAG))
            services.AddSQLite(configuration);
    }

    public static void UseDataStore(this WebApplication app, IConfiguration configuration)
    {
        if (configuration.IsFeatureEnabled(Constants.SQLITE_FEATURE_FLAG))
            app.UseSQLite(configuration);
    }
}
