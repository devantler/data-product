namespace Devantler.DataMesh.DataProduct.Configuration.Options.ServiceOptions.DataStoreOptions.DocumentBased;

/// <summary>
/// Options to configure a document based data store.
/// </summary>
public abstract class DocumentBasedDataStoreOptionsBase : DataStoreOptionsBase
{
    /// <inheritdoc/>
    public override DataStoreType Type { get; set; } = DataStoreType.DocumentBased;
}
