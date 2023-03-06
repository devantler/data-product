namespace Devantler.DataMesh.DataProduct.Configuration.Options.ServiceOptions.DataStoreOptions.DocumentBased;

/// <summary>
/// Options to configure a MongoDb data store.
/// </summary>
public class MongoDbDataStoreOptions : DocumentBasedDataStoreOptionsBase
{
    /// <inheritdoc/>
    public override string Provider { get; set; } = DocumentBasedDataStoreProvider.MongoDb;

    /// <inheritdoc/>
    public override string ConnectionString { get; set; } = string.Empty;
}
