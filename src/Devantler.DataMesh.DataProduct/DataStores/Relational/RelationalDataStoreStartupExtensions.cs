using Devantler.DataMesh.DataProduct.DataStores.Relational.Providers.Sqlite;
using Devantler.DataMesh.DataProduct.Extensions;

namespace Devantler.DataMesh.DataProduct.DataStores.Relational;

public static class RelationalDataStoreStartupExtensions
{
    public static void AddRelationalDataStore(this IServiceCollection services, IConfiguration configuration)
    {
        switch (configuration.GetFeatureValue(Constants.DATASTORE_PROVIDER_FEATURE_FLAG))
        {
            case Providers.Sqlite.Constants.DATASTORE_PROVIDER_FEATURE_FLAG_VALUE:
                services.AddSqlite();
                break;
            default:
                throw new NotSupportedException("Provided relational DataStore is not supported");
        }
    }

    public static void UseRelationalDataStore(this WebApplication app, IConfiguration configuration)
    {
        switch (configuration.GetFeatureValue(Constants.DATASTORE_PROVIDER_FEATURE_FLAG))
        {
            case Providers.Sqlite.Constants.DATASTORE_PROVIDER_FEATURE_FLAG_VALUE:
                app.UseSqlite();
                break;
            default:
                throw new NotSupportedException("Provided relational DataStore is not supported");
        }
    }
}
