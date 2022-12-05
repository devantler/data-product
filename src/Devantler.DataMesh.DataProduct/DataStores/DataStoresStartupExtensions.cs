using Devantler.DataMesh.DataProduct.DataStores.Relational;
using Devantler.DataMesh.DataProduct.Extensions;

namespace Devantler.DataMesh.DataProduct.DataStores;

public static class DataStoresStartupExtensions
{
    public static void AddDataStore(this IServiceCollection services, IConfiguration configuration)
    {
        switch (configuration.GetFeatureValue(Constants.DATASTORE_TYPE_FEATURE_FLAG))
        {
            case Relational.Constants.DATASTORE_TYPE_FEATURE_FLAG_VALUE:
                services.AddRelationalDataStore(configuration);
                break;
            default:
                throw new NotSupportedException("DataStore not supported");
        }
    }

    public static void UseDataStore(this WebApplication app, IConfiguration configuration)
    {
        switch (configuration.GetFeatureValue(Constants.DATASTORE_TYPE_FEATURE_FLAG))
        {
            case Relational.Constants.DATASTORE_TYPE_FEATURE_FLAG_VALUE:
                app.UseRelationalDataStore(configuration);
                break;
            default:
                throw new NotSupportedException("DataStore not supported");
        }
    }
}
