using Devantler.DataMesh.DataProduct.DataStores.Relational.Providers.Sqlite;
using Devantler.DataMesh.DataProduct.Extensions;

namespace Devantler.DataMesh.DataProduct.DataStores.Relational;

public static class RelationalDataStoreStartupExtensions
{
    public static void AddRelationalDataStore(this IServiceCollection services, IConfiguration configuration)
    {
        _ = configuration.GetFeatureValue(Constants.DataStoreProviderFeatureFlag) switch
        {
            Providers.Sqlite.Constants.DataStoreProviderFeatureFlagValue => services.AddSqlite(),
            _ => throw new NotSupportedException("Provided relational DataStore is not supported"),
        };
    }

    public static void UseRelationalDataStore(this WebApplication app, IConfiguration configuration)
    {
        _ = configuration.GetFeatureValue(Constants.DataStoreProviderFeatureFlag) switch
        {
            Providers.Sqlite.Constants.DataStoreProviderFeatureFlagValue => app.UseSqlite(),
            _ => throw new NotSupportedException("Provided relational DataStore is not supported"),
        };
    }
}
