namespace Devantler.DataMesh.DataProduct.Configuration.Options.Services.DataStore.NoSQL;

/// <summary>
/// Options to configure a MongoDb data store.
/// </summary>
public class MongoDbDataStoreOptions : IDataStoreOptions
{
    /// <inheritdoc/>
    public DataStoreType Type { get; set; } = DataStoreType.NoSQL;

    /// <inheritdoc/>
    public string Provider { get; set; } = NoSQLDataStoreProvider.LiteDb;

    /// <inheritdoc/>
    public string ConnectionString { get; set; } = string.Empty;
}
