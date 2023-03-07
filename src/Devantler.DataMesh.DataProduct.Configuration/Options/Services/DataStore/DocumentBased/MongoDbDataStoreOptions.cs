namespace Devantler.DataMesh.DataProduct.Configuration.Options.Services.DataStore.DocumentBased;

/// <summary>
/// Options to configure a MongoDb data store.
/// </summary>
public class MongoDbDataStoreOptions : IDataStoreOptions
{
    /// <inheritdoc/>
    public DataStoreType Type { get; set; } = DataStoreType.DocumentBased;

    /// <inheritdoc/>
    public string Provider { get; set; } = DocumentBasedDataStoreProvider.LiteDb;

    /// <inheritdoc/>
    public string ConnectionString { get; set; } = string.Empty;
}
